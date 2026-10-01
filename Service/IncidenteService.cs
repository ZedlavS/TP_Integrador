using System;
using System.Collections.Generic;
using System.Linq;
using TuProyecto.Dominio;

namespace TuProyecto.Servicios
{
    public class IncidenteService
    {
        private readonly List<Incidente> _incidentes = new();
        private readonly EquipoService _equipoService;
        private readonly HeroeService _heroeService;

        public IncidenteService(EquipoService equipoService, HeroeService heroeService)
        {
            _equipoService = equipoService;
            _heroeService = heroeService;
        }

        public IEnumerable<Incidente> ObtenerTodos() => _incidentes;

        public Incidente ObtenerPorId(int id)
        {
            return _incidentes.FirstOrDefault(i => i.Id == id) 
                ?? throw new KeyNotFoundException($"El incidente con ID {id} no existe.");
        }

        public Incidente CrearIncidente(Incidente nuevoIncidente)
        {
            nuevoIncidente.Id = _incidentes.Count + 1;
            nuevoIncidente.EstadoActual = EstadoIncidente.NoHecho;
            _incidentes.Add(nuevoIncidente);
            return nuevoIncidente;
        }

        public void IniciarIncidente(int id)
        {
            var incidente = ObtenerPorId(id);

            if (incidente.EstadoActual != EstadoIncidente.NoHecho)
            {
                throw new InvalidOperationException($"No se puede iniciar el incidente. Estado actual: {incidente.EstadoActual}.");
            }

            incidente.IniciarIncidente();
        }

        // REGLAS DE NEGOCIO PRINCIPALES: Asignación y resolución de incidentes
        public void AsignarYResolverIncidente(int incidenteId, List<int> equiposIds)
        {
            var incidente = ObtenerPorId(incidenteId);

            if (incidente.EstadoActual == EstadoIncidente.Hecho)
            {
                throw new InvalidOperationException("El incidente ya fue resuelto previamente.");
            }

            // 1. REGLA: Verificar si requiere asignación de múltiples equipos (Emergencia General)
            bool esEmergencia = incidente.EsEmergenciaGeneral();
            if (esEmergencia && equiposIds.Count < 2)
            {
                throw new InvalidOperationException($"El incidente es una Emergencia General (Dificultad {incidente.NivelDificultad}) y requiere la asignación de al menos 2 equipos.");
            }

            var equipos = equiposIds.Select(id => _equipoService.ObtenerPorId(id)).ToList();

            // 2. REGLA: Validar que todos los equipos estén aptos para participar
            foreach (var equipo in equipos)
            {
                if (!equipo.PuedeParticiparEnMision())
                {
                    throw new InvalidOperationException($"El equipo '{equipo.Nombre}' no está disponible o no tiene el mínimo de 2 integrantes disponibles.");
                }
            }

            // 3. REGLA: Verificar los poderes requeridos por el incidente
            if (!string.IsNullOrEmpty(incidente.TipoPoder))
            {
                bool tienePoderRequerido = equipos
                    .SelectMany(e => e.Heroes)
                    .SelectMany(h => h.Poderes)
                    .Any(p => p.Nombre.Equals(incidente.TipoPoder, StringComparison.OrdinalIgnoreCase));

                if (!tienePoderRequerido)
                {
                    throw new InvalidOperationException($"Ninguno de los héroes en los equipos asignados posee el poder requerido: '{incidente.TipoPoder}'.");
                }
            }

            // 4. REGLA: Comparar la capacidad combinada de los equipos contra la dificultad del incidente
            int capacidadTotalEquipos = equipos.Sum(e => e.Capacidad());
            if (capacidadTotalEquipos < incidente.NivelDificultad)
            {
                throw new InvalidOperationException($"La capacidad total combinada ({capacidadTotalEquipos}) es insuficiente para resolver la dificultad del incidente ({incidente.NivelDificultad}).");
            }

            // 5. REGLA: Cambiar estados tras resolver
            incidente.IniciarIncidente();
            incidente.FinalizarIncidente();

            // Liberar/actualizar estados de los héroes participantes
            foreach (var heroe in equipos.SelectMany(e => e.Heroes))
            {
                heroe.CantidadExitos++;
                _heroeService.FinalizarMision(heroe.Id); // Pasa a inactivo o cansado
            }
        }

        public void FinalizarIncidente(int id)
        {
            var incidente = ObtenerPorId(id);

            if (incidente.EstadoActual != EstadoIncidente.EnCurso)
            {
                throw new InvalidOperationException("Solo se pueden finalizar incidentes que estén EnCurso.");
            }

            incidente.FinalizarIncidente();
        }
    }
}
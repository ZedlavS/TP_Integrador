using System;
using System.Collections.Generic;
using System.Linq;
using TuProyecto.Dominio;

namespace TuProyecto.Servicios
{
    public class EquipoService
    {
        private readonly List<Equipo> _equipos = new();
        private readonly HeroeService _heroeService;

        public EquipoService(HeroeService heroeService)
        {
            _heroeService = heroeService;
        }

        public IEnumerable<Equipo> ObtenerTodos() => _equipos;

        public Equipo ObtenerPorId(int id)
        {
            return _equipos.FirstOrDefault(e => e.Id == id) 
                ?? throw new KeyNotFoundException($"El equipo con ID {id} no existe.");
        }

        public Equipo CrearEquipo(Equipo nuevoEquipo)
        {
          
            if (nuevoEquipo.Heroes != null && (nuevoEquipo.Heroes.Count < 2 || nuevoEquipo.Heroes.Count > 5))
            {
                throw new InvalidOperationException("Un equipo debe crearse con entre 2 y 5 integrantes.");
            }

            nuevoEquipo.Id = _equipos.Count + 1;
            _equipos.Add(nuevoEquipo);
            return nuevoEquipo;
        }

        public int ObtenerCapacidadEquipo(int id)
        {
            var equipo = ObtenerPorId(id);
            
           
            return equipo.Capacidad();
        }

        public void AgregarHeroeAEquipo(int equipoId, int heroeId)
        {
            var equipo = ObtenerPorId(equipoId);

            
            if (equipo.Heroes.Count >= 5)
            {
                throw new InvalidOperationException("El equipo ya alcanzó el límite máximo de 5 integrantes.");
            }

           
            if (equipo.Heroes.Any(h => h.Id == heroeId))
            {
                throw new InvalidOperationException("El héroe ya pertenece a este equipo.");
            }

        
            _heroeService.AsignarAMision(heroeId);

           
            var heroe = _heroeService.ObtenerPorId(heroeId);
            equipo.Heroes.Add(heroe);
    }
}
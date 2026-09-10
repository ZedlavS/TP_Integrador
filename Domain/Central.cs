using System;
using System.Collections.Generic;
using System.Linq;

namespace TuProyecto.Dominio
{
    public class Central
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        
        // Listas generales registradas en la Central
        public List<Heroe> HeroesRegistrados { get; set; } = new List<Heroe>();
        public List<Equipo> EquiposRegistrados { get; set; } = new List<Equipo>();
        public List<Incidente> IncidentesRegistrados { get; set; } = new List<Incidente>();

        public Central() { }

        public Central(string nombre)
        {
            Nombre = nombre;
        }

        /// <summary>
        /// Recibe un nuevo incidente y asigna el o los equipos óptimos según zona, poder y capacidad.
        /// </summary>
        public bool ProcesarIncidente(Incidente incidente, Zona zonaIncidente)
        {
            if (incidente == null || zonaIncidente == null) return false;

            IncidentesRegistrados.Add(incidente);

            // 1. Filtrar solo equipos que están en condiciones de asumir misiones
            var equiposElegibles = EquiposRegistrados
                .Where(e => e != null && e.PuedeParticiparEnMision())
                .ToList();

            if (!equiposElegibles.Any())
            {
                return false; 
            }

            // 2. Evaluar y ordenar equipos protegiendo contra posibles valores nulos:
            // - Prioridad a los que cubren la zona del incidente
            // - Compatibilidad de tipo de poder requerido
            // - Mayor capacidad total
            var equiposOrdenados = equiposElegibles
                .OrderByDescending(e => e.Zonas != null && e.Zonas.Any(z => z != null && string.Equals(z.Nombre, zonaIncidente.Nombre, StringComparison.OrdinalIgnoreCase)))
                .ThenByDescending(e => e.Heroes != null && e.Heroes.Any(h => h != null && h.Poderes != null && h.Poderes.Any(p => p != null && string.Equals(p.Tipo, incidente.TipoPoder, StringComparison.OrdinalIgnoreCase))))
                .ThenByDescending(e => e.Capacidad())
                .ToList();

            // 3. Regla de negocio: Emergencia General requiere 2 equipos
            int cantidadEquiposRequeridos = incidente.EsEmergenciaGeneral() ? 2 : 1;

            if (equiposOrdenados.Count < cantidadEquiposRequeridos)
            {
                return false;
            }

            // 4. Asignar la misión
            var equiposAsignados = equiposOrdenados.Take(cantidadEquiposRequeridos).ToList();
            
            foreach (var equipo in equiposAsignados)
            {
                equipo.EstadoActual = EstadoEquipo.EnMision;
            }

            incidente.IniciarIncidente();
            return true;
        }

        /// <summary>
        /// Devuelve los héroes de la central que están disponibles para ser asignados.
        /// </summary>
        public List<Heroe> ObtenerHeroesDisponibles()
        {
            return HeroesRegistrados
                .Where(h => h != null && h.Estado == EstadoHeroe.Disponible)
                .ToList();
        }

        /// <summary>
        /// Devuelve los equipos de la central que están listos para salir a misión.
        /// </summary>
        public List<Equipo> ObtenerEquiposDisponibles()
        {
            return EquiposRegistrados
                .Where(e => e != null && e.PuedeParticiparEnMision())
                .ToList();
        }
    }
}

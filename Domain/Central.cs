using System;
using System.Collections.Generic;
using System.Linq;
namespace TuProyecto.Dominio{
    public class Central
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<Equipo> EquiposRegistrados { get; set; } = new List<Equipo>();
    public List<Incidente> IncidentesRegistrados { get; set; } = new List<Incidente>();

    public Central() { }

    public Central(string nombre)
    {
        Nombre = nombre;
    }

    /// <summary>
    /// Recibe un nuevo incidente y busca asignarle el o los equipos óptimos.
    /// </summary>
    public bool ProcesarIncidente(Incidente incidente, Zona zonaIncidente)
    {
        IncidentesRegistrados.Add(incidente);

        // 1. Filtrar solo equipos que están en condiciones de asumir misiones
        var equiposElegibles = EquiposRegistrados
            .Where(e => e.PuedeParticiparEnMision())
            .ToList();

        if (!equiposElegibles.Any())
        {
            // No hay equipos disponibles
            return false; 
        }

        // 2. Evaluar y ordenar equipos según criterios:
        // - Prioridad a los que están en la zona del incidente
        // - Compatibilidad de poderes (si algún héroe del equipo posee el TipoPoder requerido)
        // - Mayor capacidad total
        var equiposOrdenados = equiposElegibles
            .OrderByDescending(e => e.Zonas != null && e.Zonas.Any(z => z.Nombre == zonaIncidente.Nombre))
            .ThenByDescending(e => e.Heroes.Any(h => h.Poderes.Any(p => p.Tipo.Equals(incidente.TipoPoder, StringComparison.OrdinalIgnoreCase))))
            .ThenByDescending(e => e.Capacidad())
            .ToList();

        // 3. Regla de negocio: Emergencia General requiere 2 equipos
        int cantidadEquiposRequeridos = incidente.EsEmergenciaGeneral() ? 2 : 1;

        if (equiposOrdenados.Count < cantidadEquiposRequeridos)
        {
            // No hay suficientes equipos para cubrir una emergencia general
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
}

}

using Microsoft.AspNetCore.Mvc;
using TuProyecto.Dominio;

[ApiController]
[Route("api/[controller]")]
public class CentralController : ControllerBase
{
    [HttpPost("despachar")]
    public IActionResult DespacharIncidente([FromQuery] int incidenteId, [FromQuery] int zonaId)
    {
        // TODO: 
        // 1. Cargar la Central, el Incidente con id `incidenteId` y la Zona con id `zonaId`.
        // 2. bool asignado = central.ProcesarIncidente(incidente, zona);
        
        bool exito = true; // Simulación

        if (!exito)
        {
            return BadRequest(new { Mensaje = "No se encontraron equipos disponibles o suficientes para atender este incidente." });
        }

        return Ok(new { Mensaje = "Incidente despachado y asignado a equipo(s) con éxito." });
    }

    [HttpGet("estado-global")]
    public IActionResult GetEstadoGlobal()
    {
        // TODO: Retornar resumen de la central (incidentes en curso, equipos en misión, etc.)
        return Ok(new { Estado = "Operativo", IncidentesActivos = 0 });
    }
}

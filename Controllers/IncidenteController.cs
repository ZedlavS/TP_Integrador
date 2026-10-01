using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TuProyecto.Dominio;
using TuProyecto.Servicios;

namespace TuProyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidenteController : ControllerBase
    {
        private readonly IncidenteService _incidenteService;

        public IncidenteController(IncidenteService incidenteService)
        {
            _incidenteService = incidenteService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Incidente>> GetAll()
        {
            return Ok(_incidenteService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<Incidente> GetById(int id)
        {
            try
            {
                var incidente = _incidenteService.ObtenerPorId(id);
                return Ok(incidente);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<Incidente> RegistrarIncidente([FromBody] Incidente nuevoIncidente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creado = _incidenteService.CrearIncidente(nuevoIncidente);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
        }

        [HttpPatch("{id}/iniciar")]
        public IActionResult Iniciar(int id)
        {
            try
            {
                _incidenteService.IniciarIncidente(id);
                return Ok(new { mensaje = $"Incidente {id} iniciado (EnCurso)." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("{id}/resolver")]
        public IActionResult Resolver(int id, [FromBody] List<int> equiposIds)
        {
            try
            {
                _incidenteService.AsignarYResolverIncidente(id, equiposIds);
                return Ok(new { mensaje = $"Incidente {id} fue asignado y resuelto con éxito." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPatch("{id}/finalizar")]
        public IActionResult Finalizar(int id)
        {
            try
            {
                _incidenteService.FinalizarIncidente(id);
                return Ok(new { mensaje = $"Incidente {id} marcado como Hecho." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
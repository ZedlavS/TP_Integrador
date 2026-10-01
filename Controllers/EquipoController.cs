using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TuProyecto.Dominio;
using TuProyecto.Servicios;

namespace TuProyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipoController : ControllerBase
    {
        private readonly EquipoService _equipoService;

        public EquipoController(EquipoService equipoService)
        {
            _equipoService = equipoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Equipo>> GetAll()
        {
            return Ok(_equipoService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<Equipo> GetById(int id)
        {
            try
            {
                var equipo = _equipoService.ObtenerPorId(id);
                return Ok(equipo);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<Equipo> Create([FromBody] Equipo nuevoEquipo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var equipoCreado = _equipoService.CrearEquipo(nuevoEquipo);
                return CreatedAtAction(nameof(GetById), new { id = equipoCreado.Id }, equipoCreado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{id}/capacidad")]
        public ActionResult<int> GetCapacidad(int id)
        {
            try
            {
                int capacidad = _equipoService.ObtenerCapacidadEquipo(id);
                return Ok(new { equipoId = id, capacidad });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpPost("{id}/agregar-heroe/{heroeId}")]
        public IActionResult AgregarHeroe(int id, int heroeId)
        {
            try
            {
                _equipoService.AgregarHeroeAEquipo(id, heroeId);
                return Ok(new { mensaje = $"Héroe {heroeId} agregado al equipo {id} con éxito." });
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
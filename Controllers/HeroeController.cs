using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TuProyecto.Dominio;
using TuProyecto.Servicios;

namespace TuProyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroeController : ControllerBase
    {
        private readonly HeroeService _heroeService;

        public HeroeController(HeroeService heroeService)
        {
            _heroeService = heroeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Heroe>> GetAll()
        {
            return Ok(_heroeService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<Heroe> GetById(int id)
        {
            try
            {
                var heroe = _heroeService.ObtenerPorId(id);
                return Ok(heroe);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<Heroe> Create([FromBody] Heroe nuevoHeroe)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var heroeCreado = _heroeService.CrearHeroe(nuevoHeroe);
            return CreatedAtAction(nameof(GetById), new { id = heroeCreado.Id }, heroeCreado);
        }

        [HttpPost("{id}/recuperar")]
        public IActionResult Recuperar(int id)
        {
            try
            {
                _heroeService.RecuperarHeroe(id);
                return Ok(new { mensaje = $"El héroe con ID {id} se recuperó exitosamente y se encuentra disponible." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }
    }
}
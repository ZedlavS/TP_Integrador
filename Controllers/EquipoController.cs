using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TuProyecto.Dominio;

[ApiController]
[Route("api/[controller]")]
public class EquipoController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Equipo>> GetAll()
    {
        return Ok(new List<Equipo>());
    }

    [HttpGet("{id}")]
    public ActionResult<Equipo> GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public ActionResult<Equipo> Create([FromBody] Equipo nuevoEquipo)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return CreatedAtAction(nameof(GetById), new { id = nuevoEquipo.Id }, nuevoEquipo);
    }

    [HttpGet("{id}/capacidad")]
    public ActionResult<int> GetCapacidad(int id)
    {
        // TODO: Buscar equipo y retornar equipo.Capacidad();
        return Ok(0);
    }

    [HttpPost("{id}/agregar-heroe/{heroeId}")]
    public IActionResult AgregarHeroe(int id, int heroeId)
    {
        // TODO: Vincular héroe al equipo respetando la restricción de MAX_INTEGRANTES
        return Ok(new { Mensaje = $"Héroe {heroeId} agregado al equipo {id}." });
    }
}

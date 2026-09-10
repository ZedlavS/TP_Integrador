using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TuProyecto.Dominio;


[ApiController]
[Route("api/[controller]")]
public class IncidenteController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Incidente>> GetAll()
    {
        return Ok(new List<Incidente>());
    }

    [HttpGet("{id}")]
    public ActionResult<Incidente> GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public ActionResult<Incidente> RegistrarIncidente([FromBody] Incidente nuevoIncidente)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // TODO: Persistir el nuevo incidente
        return CreatedAtAction(nameof(GetById), new { id = nuevoIncidente.Id }, nuevoIncidente);
    }

    [HttpPatch("{id}/iniciar")]
    public IActionResult Iniciar(int id)
    {
        // TODO: Obtener incidente y ejecutar incidente.IniciarIncidente();
        return Ok(new { Mensaje = $"Incidente {id} iniciado (EnCurso)." });
    }

    [HttpPatch("{id}/finalizar")]
    public IActionResult Finalizar(int id)
    {
        // TODO: Obtener incidente y ejecutar incidente.FinalizarIncidente();
        return Ok(new { Mensaje = $"Incidente {id} marcado como Hecho." });
    }
}

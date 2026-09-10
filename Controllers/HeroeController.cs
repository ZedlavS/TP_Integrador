using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TuProyecto.Dominio;

[ApiController]
[Route("api/[controller]")]
public class HeroeController : ControllerBase
{
    // NOTA: Cuando conectes la base de datos, inyectarás un DbContext o un Servicio aquí.
    
    [HttpGet]
    public ActionResult<IEnumerable<Heroe>> GetAll()
    {
        return Ok(new List<Heroe>());
    }

    [HttpGet("{id}")]
    public ActionResult<Heroe> GetById(int id)
    {
        // TODO: Buscar héroe por Id
        return Ok();
    }

    [HttpPost]
    public ActionResult<Heroe> Create([FromBody] Heroe nuevoHeroe)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // TODO: Guardar en repositorio/base de datos
        return CreatedAtAction(nameof(GetById), new { id = nuevoHeroe.Id }, nuevoHeroe);
    }

    [HttpPatch("{id}/recibir-dano")]
    public IActionResult RecibirDano(int id)
    {
        // TODO: Obtener héroe y ejecutar heroe.RecibirDaño();
        return Ok(new { Mensaje = $"El héroe con ID {id} ha sido marcado como Herido." });
    }

    [HttpPatch("{id}/recuperar")]
    public IActionResult Recuperar(int id)
    {
        // TODO: Obtener héroe y ejecutar heroe.Recuperar();
        return Ok(new { Mensaje = $"El héroe con ID {id} ha recuperado su estado Disponible." });
    }
}

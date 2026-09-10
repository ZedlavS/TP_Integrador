using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TuProyecto.Dominio;


[ApiController]
[Route("api/[controller]")]
public class PoderController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Poder>> GetAll()
    {
        return Ok(new List<Poder>());
    }

    [HttpPost]
    public ActionResult<Poder> Create([FromBody] Poder nuevoPoder)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(nuevoPoder);
    }
}

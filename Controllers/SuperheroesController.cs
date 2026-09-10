using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TP_Integrador.Domain; // Asegúrate de ajustar este namespace al de tu proyecto

namespace TP_Integrador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroesController : ControllerBase
    {
        // Lista temporal en memoria para simular la base de datos
        private static List<Heroe> _heroes = new List<Heroe>();
        private static int _nextId = 1;

        // 1. OBTENER TODOS LOS HÉROES
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            return Ok(_heroes);
        }

        // 2. CREAR UN NUEVO HÉROE
        [HttpPost]
        public IActionResult Crear([FromBody] Heroe nuevoHeroe)
        {
            if (string.IsNullOrWhiteSpace(nuevoHeroe.Nombre))
            {
                return BadRequest("El nombre del héroe es obligatorio.");
            }

            // Asignamos un ID único simulado (agrega 'public int Id { get; set; }' a tu modelo Heroe)
            // nuevoHeroe.Id = _nextId++;
            _heroes.Add(nuevoHeroe);

            return Ok(new { mensaje = "Héroe creado con éxito", heroe = nuevoHeroe });
        }

        // 3. CONSULTAR ESTADÍSTICAS (Aprovechamos tus métodos CalcularEstrellas y Efectividad)
        [HttpGet("{nombre}/estadisticas")]
        public IActionResult ObtenerEstadisticas(string nombre)
        {
            var heroe = _heroes.FirstOrDefault(h => h.Nombre.ToLower() == nombre.ToLower());
            if (heroe == null)
            {
                return NotFound($"No se encontró al héroe: {nombre}");
            }

            return Ok(new
            {
                Nombre = heroe.Nombre,
                Estrellas = heroe.CalcularEstrellas(),
                PorcentajeEfectividad = heroe.efectividad(),
                EstadoActual = heroe.Estado.ToString()
            });
        }

        // 4. ACCIÓN: HERIR HÉROE (Llama a RecibirDaño)
        [HttpPost("{nombre}/recibir-danio")]
        public IActionResult RecibirDanio(string nombre)
        {
            var heroe = _heroes.FirstOrDefault(h => h.Nombre.ToLower() == nombre.ToLower());
            if (heroe == null) return NotFound("Héroe no encontrado.");

            heroe.RecibirDaño();
            return Ok(new { mensaje = $"{heroe.Nombre} sufrió daño.", nuevoEstado = heroe.Estado.ToString() });
        }

        // 5. ACCIÓN: RECUPERAR HÉROE (Llama a Recuperar)
        [HttpPost("{nombre}/recuperar")]
        public IActionResult Recuperar(string nombre)
        {
            var heroe = _heroes.FirstOrDefault(h => h.Nombre.ToLower() == nombre.ToLower());
            if (heroe == null) return NotFound("Héroe no encontrado.");

            heroe.Recuperar();
            return Ok(new { mensaje = $"{heroe.Nombre} ha intentado recuperarse.", nuevoEstado = heroe.Estado.ToString() });
        }
    }
}
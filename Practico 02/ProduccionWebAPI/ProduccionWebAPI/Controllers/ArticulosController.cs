using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProduccionBack.Servicios;
using ProduccionBack.Modelos;

namespace ProduccionWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ArticulosController : ControllerBase
    {
        private ServicioArticulos servicioArticulos = new ServicioArticulos();

        [HttpGet("obtener")]
        public IActionResult Get()
        {
            List<Articulo>? articulos = servicioArticulos.ObtenerTodos();

            if (articulos is null) return StatusCode(500, "Error interno. Intente nuevamente.");

            else if (articulos.Count == 0) return NotFound();

            else return Ok(articulos);
        }

        [HttpGet("obtener/{idArticulo}")]
        public IActionResult Get(int idArticulo)
        {
            if (idArticulo <= 0) return BadRequest("No se permiten numeros de identificacion sin valor.");

            Articulo? articulo = servicioArticulos.ObtenerPorId(idArticulo);

            if (articulo is null) return StatusCode(500, "Error interno. Intente nuevamente.");

            else if (articulo.Nombre is null) return NotFound();

            else return Ok(articulo);
        }

        [HttpPost("crear")]
        public IActionResult Post(Articulo articulo)
        {
            if (articulo.Nombre == "" || articulo.PrecioUnitario <= 0) return BadRequest("Nombre y valor obligatorios.");
            
            bool? resultado = servicioArticulos.Crear(articulo);
            
            if (!resultado.HasValue) return StatusCode(500, "Error interno. Intente nuevamente.");

            else return Ok("Articulo creado correctamente.");
        }

        [HttpPut("modificar")]
        public IActionResult Put(Articulo articulo)
        {
            if (articulo.IdArticulo <= 0 || articulo.Nombre == "" || articulo.PrecioUnitario <= 0) return BadRequest("Id, nombre y valor obligatorios.");

            bool? resultado = servicioArticulos.Modificar(articulo);

            if (resultado is null) return StatusCode(500, "Error interno. Intente nuevamente.");

            else if (resultado == false) return NotFound();

            else return Ok("Articulo modificado correctamente.");
        }

        [HttpDelete("eliminar/{idArticulo}")]
        public IActionResult Delete(int idArticulo)
        {
            if (idArticulo <= 0) return BadRequest("No se permiten numeros de identificacion sin valor.");

            bool? resultado = servicioArticulos.Eliminar(idArticulo);

            if (resultado is null) return StatusCode(500, "Error interno. Intente nuevamente.");

            else if (resultado == false) return NotFound();

            else return Ok("Articulo eliminado correctamente.");
        }
    }
}

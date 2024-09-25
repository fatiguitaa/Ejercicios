using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProduccionBack.Modelos;
using ProduccionBack.Servicios;

namespace ProduccionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {

        private ServicioFactura servicioFactura = new ServicioFactura();

        [HttpGet("obtener")]
        public IActionResult Get()
        {
            List<Factura>? facturas = servicioFactura.ObtenerTodas();

            if (facturas is null) return StatusCode(500, "Error interno. Intente nuevamente.");

            else if (facturas.Count == 0) return NotFound();

            else return Ok(facturas);
        }

        [HttpGet("obtener/{idFactura}")]
        public IActionResult Get(int idFactura)
        {
            if (idFactura <= 0) return BadRequest("No se permiten numeros de identificacion sin valor.");

            Factura? factura = servicioFactura.ObtenerPorId(idFactura);

            if (factura is null) return StatusCode(500, "Error interno. Intente nuevamente.");

            else if (factura.IdFactura == 0) return NotFound();

            else return Ok(factura);
        }

        [HttpPost("crear")]
        public IActionResult Post(Factura factura)
        {
            if (factura.IdFormaPago <= 0) return BadRequest("Forma de pago obligatoria.");
            else if (factura.Fecha.Year < 1900) return BadRequest("Fecha demasiado antigua.");

            bool? resultado = servicioFactura.Crear(factura);

            if (!resultado.HasValue) return StatusCode(500, "Error interno. Intente nuevamente.");

            else return Ok("Articulo creado correctamente.");
        }

        [HttpPut("modificar")]
        public IActionResult Put(Factura factura)
        {
            if (factura.IdFactura <= 0 || factura.IdFormaPago <= 0) return BadRequest("Id y forma de pago obligatorios.");
            else if (factura.Fecha.Year < 1900) return BadRequest("Fecha demasiado antigua.");

            var resultado = servicioFactura.Modificar(factura);

            if (resultado is null) return StatusCode(500, "Error interno. Intente nuevamente.");

            else if (resultado == false) return NotFound();

            else return Ok("Articulo modificado correctamente.");
        }

        [HttpDelete("eliminar/{idFactura}")]
        public IActionResult Delete(int idFactura)
        {
            if (idFactura <= 0) return BadRequest("No se permiten numeros de identificacion sin valor.");

            bool? resultado = servicioFactura.Eliminar(idFactura);

            if (resultado is null) return StatusCode(500, "Error interno. Intente nuevamente.");

            else if (resultado == false) return NotFound();

            else return Ok(resultado);
        }
    }
}

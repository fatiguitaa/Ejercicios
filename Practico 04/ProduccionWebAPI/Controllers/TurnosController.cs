using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Produccion_Back.Datos.Modelos;
using Produccion_Back.Datos.Repositorios;

namespace ProduccionWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private TurnosRepository repository;

        public TurnosController(TurnosRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            try
            {
                var turnos = repository.ObtenerTodos();

                if (turnos.Count == 0) return NotFound();
                else return Ok(turnos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
        }

        [HttpGet()]
        public IActionResult Get([FromQuery] int id)
        {
            try
            {
                var turno = repository.ObtenerPorId(id);

                if (turno is null) return NotFound();
                else return Ok(turno);
            }
            catch
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Turno turno)
        {
            try
            {
                repository.Crear(turno);
                return Ok();
            }
            catch
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
        }

        [HttpPut()]
        public IActionResult Put([FromBody] Turno turno)
        {
            try
            {
                repository.Modificar(turno);
                return Ok();
            }
            catch
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
        }

        [HttpDelete("/eliminar")]
        public IActionResult Delete([FromQuery] int id)
        {
            try
            {
                repository.Eliminar(id);
                return Ok();
            }
            catch
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }

        }
    }
}

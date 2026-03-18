using inventory_api.Dtos;
using inventory_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inventory_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CintaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CintaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearCinta([FromBody] DtoCinta dto)
        {
            try
            {
                var existe = await _context.Cinta
                    .AnyAsync(c => c.Codigo == dto.Codigo);

                if (existe)
                    return BadRequest(new { mensaje = "Ya existe una cinta con ese código." });

                var cinta = new Cinta
                {
                    Codigo = dto.Codigo,
                    Descripcion = dto.Descripcion,
                    Contenido = dto.Contenido,
                    Fecha_Respaldo = dto.Fecha_Respaldo.HasValue && dto.Fecha_Respaldo != DateTime.MinValue ? dto.Fecha_Respaldo : null,
                    Ubicacion = dto.Ubicacion,
                    Estado = dto.Estado,
                    Fecha_Creacion = DateTime.UtcNow,
                    Activo = true
                };

                _context.Cinta.Add(cinta);
                await _context.SaveChangesAsync();

                return Ok(cinta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear la cinta", error = ex.Message });
            }
        }

        [HttpGet("cintas")]
        public async Task<ActionResult<IEnumerable<Cinta>>> GetCintas()
        {
            var models = await _context.Cinta
                .Where(p => p.Activo)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("cintas/{id}")]
        public async Task<ActionResult<IEnumerable<Cinta>>> GetCintasId(int id)
        {
            var model = await _context.Cinta
                .Where(p => p.Activo && p.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return Ok(model);
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DesactivarEquipo(int id)
        {
            var model = await _context.Cinta
                                           .FirstOrDefaultAsync(p => p.Id == id);

            if (model == null)
                return NotFound("Cinta no encontrado.");

            model.Activo = false;

            _context.Entry(model).Property(x => x.Fecha_Creacion).IsModified = false;
            _context.Entry(model).Property(x => x.Fecha_Respaldo).IsModified = false;

            await _context.SaveChangesAsync();

            return Ok("Cinta desactivada correctamente.");
        }

    }
}

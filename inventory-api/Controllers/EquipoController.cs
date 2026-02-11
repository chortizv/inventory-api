using inventory_api.Dtos;
using inventory_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inventory_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EquipoController(AppDbContext context)
        {
            _context = context;
        }

        //EQUIPOS
        [HttpGet("equipos")]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetEquipos()
        {
            var models = await _context.Equipo
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }
        
        //MARCA
        [HttpGet("marcas")]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas()
        {
            var models = await _context.Marca
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("marca/{id}")]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcaId(int id)
        {
            var models = await _context.Marca
                .Where(p => p.Activo && p.Id_marca == id)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpPost("crearMarca")]
        public async Task<IActionResult> CrearMarca([FromBody] DtoMarca dto)
        {
            var model = new Marca
            {
                Descripcion = dto.Descripcion,
                Activo = true
            };

            _context.Marca.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("eliminarMarca/{id}")]
        public async Task<IActionResult> DesactivarMarca(int id)
        {
            var model = await _context.Marca
                                           .FirstOrDefaultAsync(p => p.Id_marca == id);

            if (model == null)
                return NotFound("Marca no encontrada.");

            model.Activo = false;

            _context.Marca.Update(model);
            await _context.SaveChangesAsync();

            return Ok("Marca desactivada correctamente.");
        }

        //MODELO
        [HttpGet("modelos")]
        public async Task<ActionResult<IEnumerable<Modelo>>> GetModelos()
        {
            var models = await _context.Modelo
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("modelo/{id}")]
        public async Task<ActionResult<IEnumerable<Modelo>>> GetModeloId(int id)
        {
            var models = await _context.Modelo
                .Where(p => p.Activo && p.Id_modelo == id)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpPost("crearModelo")]
        public async Task<IActionResult> CrearModelo([FromBody] DtoModelo dto)
        {
            var model = new Modelo
            {
                Descripcion = dto.Descripcion,
                Id_marca = dto.Id_marca,
                Activo = true
            };

            _context.Modelo.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("eliminarModelo/{id}")]
        public async Task<IActionResult> DesactivarModelo(int id)
        {
            var model = await _context.Modelo
                                           .FirstOrDefaultAsync(p => p.Id_modelo == id);

            if (model == null)
                return NotFound("Modelo no encontrada.");

            model.Activo = false;

            _context.Modelo.Update(model);
            await _context.SaveChangesAsync();

            return Ok("Modelo desactivada correctamente.");
        }

        //TIPO_MODELO
        [HttpGet("tipo-modelos")]
        public async Task<ActionResult<IEnumerable<Tipo_modelo>>> GetTipoModelos()
        {
            var models = await _context.Tipo_modelo
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("tipo-modelo/{id}")]
        public async Task<ActionResult<IEnumerable<Tipo_modelo>>> GetTipoModeloId(int id)
        {
            var models = await _context.Tipo_modelo
                .Where(p => p.Activo && p.Id_tipomodelo == id)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpPost("crearTipoModelo")]
        public async Task<IActionResult> crearTipoModelo([FromBody] DtoTipoModelo dto)
        {
            var model = new Tipo_modelo
            {
                Descripcion = dto.Descripcion,
                Activo = true
            };

            _context.Tipo_modelo.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("eliminarTipoModelo/{id}")]
        public async Task<IActionResult> DesactivarTipoModelo(int id)
        {
            var model = await _context.Tipo_modelo
                                           .FirstOrDefaultAsync(p => p.Id_tipomodelo == id);

            if (model == null)
                return NotFound("Tipo modelo no encontrado.");

            model.Activo = false;

            _context.Tipo_modelo.Update(model);
            await _context.SaveChangesAsync();

            return Ok("Tipo modelo desactivado correctamente.");
        }

        //ESTADO
        [HttpGet("estados")]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstados()
        {
            var models = await _context.Estado
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("estado/{id}")]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstadoId(int id)
        {
            var models = await _context.Estado
                .Where(p => p.Activo && p.Id_estado == id)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpPost("crearEstado")]
        public async Task<IActionResult> crearEstado([FromBody] DtoEstado dto)
        {
            var model = new Estado
            {
                Descripcion = dto.Descripcion,
                Activo = true
            };

            _context.Estado.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("eliminarEstado/{id}")]
        public async Task<IActionResult> DesactivarEstado(int id)
        {
            var model = await _context.Estado
                                           .FirstOrDefaultAsync(p => p.Id_estado== id);

            if (model == null)
                return NotFound("Estado no encontrado.");

            model.Activo = false;

            _context.Estado.Update(model);
            await _context.SaveChangesAsync();

            return Ok("Estado desactivado correctamente.");
        }

        //CONTRATO
        [HttpGet("contratos")]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratos()
        {
            var models = await _context.Contrato
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("contrato/{id}")]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratoId(int id)
        {
            var models = await _context.Contrato
                .Where(p => p.Activo && p.Id_contrato == id)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpPost("crearContrato")]
        public async Task<IActionResult> crearContrato([FromBody] DtoContrato dto)
        {
            var model = new Contrato
            {
                Nomcontrato = dto.Nomcontrato,
                Activo = true
            };

            _context.Contrato.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("eliminarContrato/{id}")]
        public async Task<IActionResult> DesactivarContrato(int id)
        {
            var model = await _context.Contrato
                                           .FirstOrDefaultAsync(p => p.Id_contrato == id);

            if (model == null)
                return NotFound("Contrato no encontrado.");

            model.Activo = false;

            _context.Contrato.Update(model);
            await _context.SaveChangesAsync();

            return Ok("Contrato desactivado correctamente.");
        }

    }
}

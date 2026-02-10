using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using inventory_api.Models;

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

        //MODELO
        [HttpGet("modelos")]
        public async Task<ActionResult<IEnumerable<Modelo>>> GetModelos()
        {
            var models = await _context.Modelo
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
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

        //ESTADO
        [HttpGet("estados")]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstados()
        {
            var models = await _context.Estado
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
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
    }
}

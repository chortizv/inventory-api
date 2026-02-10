using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using inventory_api.Models;

namespace inventory_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FuncionarioController(AppDbContext context)
        {
            _context = context;
        }

        //FUNCIONARIOS
        [HttpGet("funcionarios")]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            var models = await _context.Funcionario
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        //ASIGNACION EQUIPOS
        [HttpGet("asignaciones")]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetAsignaciones()
        {
            var models = await _context.Asignacion_Equipo
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        //PRIORIDAD
        [HttpGet("prioridades")]
        public async Task<ActionResult<IEnumerable<Prioridad>>> GetPrioridades()
        {
            var models = await _context.Prioridad
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        //DEPARTAMENTO
        [HttpGet("departamentos")]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamentos()
        {
            var models = await _context.Departamento
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }
        //SUBDEPARTAMENTO
        [HttpGet("subdepartamentos")]
        public async Task<ActionResult<IEnumerable<Subdepartamento>>> GetSubdepartamentos()
        {
            var models = await _context.Subdepartamento
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }
        //SECCION
        [HttpGet("secciones")]
        public async Task<ActionResult<IEnumerable<Seccion>>> GetSecciones()
        {
            var models = await _context.Seccion
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }
    }
}

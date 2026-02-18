using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using inventory_api.Models;
using inventory_api.Dtos;

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
                .Where(p => p.Activo)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("funcionario/{id}")]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarioId(int id)
        {
            var models = await _context.Funcionario
                .Where(p => p.Activo && p.Id_funcionario == id)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpPost("crearFuncionario")]
        public async Task<IActionResult> CrearPrioridad([FromBody] DtoFuncionario dto)
        {
            var model = new Funcionario
            {
                Pnombre = dto.Pnombre,
                Snombre = dto.Snombre,
                Appaterno = dto.Appaterno,
                Apmaterno = string.IsNullOrWhiteSpace(dto.Apmaterno) ? "" : dto.Apmaterno,
                Correo = dto.Correo,
                Anexo = dto.Anexo > 0 ? 0 : dto.Anexo,
                Cargo = dto.Cargo,
                Teletrabajo = dto.Teletrabajo,
                Notebook = dto.Notebook,
                Validado = dto.Validado,
                Id_seccion = dto.Id_seccion,
                Id_prioridad = dto.Id_prioridad,
                Activo = true
            };

            _context.Funcionario.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("eliminarFuncionario/{id}")]
        public async Task<IActionResult> DesactivarFuncionario(int id)
        {
            var model = await _context.Funcionario
                                           .FirstOrDefaultAsync(p => p.Id_funcionario == id);

            if (model == null)
                return NotFound("Funcionario no encontrado.");

            model.Activo = false;

            _context.Funcionario.Update(model);
            await _context.SaveChangesAsync();

            return Ok("Funcionario desactivado correctamente.");
        }

        //ASIGNACION EQUIPOS
        [HttpGet("asignaciones")]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetAsignaciones()
        {
            var models = await _context.Asignacion_Equipo
                .Where(p => p.Activo)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("asignacion/{id}")]
        public async Task<IActionResult> ObtenerAsignacion(int id)
        {
            var asignacion = await _context.Asignacion_Equipo
                .FirstOrDefaultAsync(a => a.Id_asignacion == id);

            if (asignacion == null)
                return NotFound();

            return Ok(asignacion);
        }

        [HttpPost("crearAsignacion")]
        public async Task<IActionResult> CrearAsignacion([FromForm] DtoAsignacionEquipo dto)
        {
            if (dto.Archivo == null || dto.Archivo.Length == 0)
                return BadRequest("Debe adjuntar un archivo.");

            // Ruta física
            var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "asignacion_equipo");

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            // Nombre único
            var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(dto.Archivo.FileName);

            var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await dto.Archivo.CopyToAsync(stream);
            }

            var url = $"/uploads/asignacion_equipo/{nombreArchivo}";

            // Crear entidad
            var asignacion = new Asignacion_equipo
            {
                Id_funcionario = dto.Id_funcionario,
                Serie = dto.Serie,
                Fecha_inicio = dto.Fecha_inicio,
                Fecha_fin = dto.Fecha_fin,
                Estado = dto.Estado,
                Observacion = dto.Observacion,
                Activo = true,
                Url_archivo = url
            };

            _context.Asignacion_Equipo.Add(asignacion);
            await _context.SaveChangesAsync();

            return Ok(asignacion);
        }

        [HttpGet("historial/{idFuncionario}")]
        public async Task<IActionResult> ObtenerHistorialPorFuncionario(int idFuncionario)
        {
            var historial = await _context.Asignacion_Equipo
                .Where(a => a.Id_funcionario == idFuncionario)
                .OrderByDescending(a => a.Fecha_inicio) // opcional pero recomendado
                .AsNoTracking()
                .ToListAsync();

            if (!historial.Any())
                return NotFound("El funcionario no tiene asignaciones registradas.");

            return Ok(historial);
        }

        //PRIORIDAD
        [HttpGet("prioridades")]
        public async Task<ActionResult<IEnumerable<Prioridad>>> GetPrioridades()
        {
            var models = await _context.Prioridad
                .Where(p => p.Activo)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("prioridad/{id}")]
        public async Task<ActionResult<IEnumerable<Prioridad>>> GetPrioridadId(int id)
        {
            var models = await _context.Prioridad
                .Where(p => p.Activo && p.Id_prioridad == id)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpPost("crearPrioridad")]
        public async Task<IActionResult> CrearPrioridad([FromBody] DtoPrioridad dto)
        {
            var prioridad = new Prioridad
            {
                Descripcion = dto.Descripcion,
                Activo = true
            };

            _context.Prioridad.Add(prioridad);
            await _context.SaveChangesAsync();

            return Ok(prioridad);
        }

        [HttpDelete("eliminarPrioridad/{id}")]
        public async Task<IActionResult> DesactivarPrioridad(int id)
        {
            var prioridad = await _context.Prioridad
                                           .FirstOrDefaultAsync(p => p.Id_prioridad == id);

            if (prioridad == null)
                return NotFound("Prioridad no encontrada.");

            prioridad.Activo = false;

            _context.Prioridad.Update(prioridad);
            await _context.SaveChangesAsync();

            return Ok("Prioridad desactivada correctamente.");
        }

        //DEPARTAMENTO
        [HttpGet("departamentos")]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamentos()
        {
            var models = await _context.Departamento
                .Where(p => p.Activo)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("departamento/{id}")]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDeptoId(int id)
        {
            var models = await _context.Departamento
                .Where(p => p.Activo && p.Id_dep == id)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpPost("crearDepto")]
        public async Task<IActionResult> CrearDepartamento([FromBody] DtoDepto dto)
        {
            var departamento = new Departamento
            {
                Descripcion = dto.Descripcion,
                Activo = true
            };

            _context.Departamento.Add(departamento);
            await _context.SaveChangesAsync();

            return Ok(departamento);
        }

        [HttpDelete("eliminarDepto/{id}")]
        public async Task<IActionResult> DesactivarDepto(int id)
        {
            var depto = await _context.Departamento
                                           .FirstOrDefaultAsync(p => p.Id_dep == id);

            if (depto == null)
                return NotFound("Departamento no encontrado.");

            depto.Activo = false;

            _context.Departamento.Update(depto);
            await _context.SaveChangesAsync();

            return Ok("Departamento desactivado correctamente.");
        }
        //SUBDEPARTAMENTO
        [HttpGet("subdepartamentos")]
        public async Task<ActionResult<IEnumerable<Subdepartamento>>> GetSubdepartamentos()
        {
            var models = await _context.Subdepartamento
                .Where(p => p.Activo)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("subdepartamento/{id}")]
        public async Task<ActionResult<IEnumerable<Subdepartamento>>> GetSubeptoId(int id)
        {
            var models = await _context.Subdepartamento
                .Where(p => p.Activo && p.Id_subdep == id)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpPost("crearSubdepto")]
        public async Task<IActionResult> crearSubdepto([FromBody] DtoSubdepto dto)
        {
            var model = new Subdepartamento
            {
                Descripcion = dto.Descripcion,
                Id_dep = dto.Id_dep,
                Activo = true
            };

            _context.Subdepartamento.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("eliminarSubdepto/{id}")]
        public async Task<IActionResult> DesactivarSubdepto(int id)
        {
            var model = await _context.Subdepartamento
                                           .FirstOrDefaultAsync(p => p.Id_subdep == id);

            if (model == null)
                return NotFound("Subdepartamento no encontrado.");

            model.Activo = false;

            _context.Subdepartamento.Update(model);
            await _context.SaveChangesAsync();

            return Ok("Subdepartamento desactivado correctamente.");
        }
        //SECCION
        [HttpGet("secciones")]
        public async Task<ActionResult<IEnumerable<Seccion>>> GetSecciones()
        {
            var models = await _context.Seccion
                .Where(p => p.Activo)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("seccion/{id}")]
        public async Task<ActionResult<IEnumerable<Seccion>>> GetSeccionId(int id)
        {
            var models = await _context.Seccion
                .Where(p => p.Activo && p.Id_seccion == id)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpPost("crearSeccion")]
        public async Task<IActionResult> crearSeccion([FromBody] DtoSeccion dto)
        {
            var model = new Seccion
            {
                Descripcion = dto.Descripcion,
                Id_subdep = dto.Id_subdep,
                Activo = true
            };

            _context.Seccion.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("eliminarSeccion/{id}")]
        public async Task<IActionResult> DesactivarSeccion(int id)
        {
            var model = await _context.Seccion
                                           .FirstOrDefaultAsync(p => p.Id_seccion == id);

            if (model == null)
                return NotFound("Seccion no encontrado.");

            model.Activo = false;

            _context.Seccion.Update(model);
            await _context.SaveChangesAsync();

            return Ok("Seccion desactivado correctamente.");
        }
    }
}

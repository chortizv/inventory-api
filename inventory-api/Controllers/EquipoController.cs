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
                .Where(p => p.Activo)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }
        [HttpGet("equipos-sinasignar")]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetEquiposSA()
        {
            var models = await _context.Equipo
                .Where(p => p.Activo && p.Id_estado == 3)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }
        [HttpGet("equipos-descripcion")]
        public async Task<ActionResult<IEnumerable<DtoEquipoDescripcion>>> GetEquiposDescripcion()
        {
            var models = await (
                from e in _context.Equipo
                join est in _context.Estado on e.Id_estado equals est.Id_estado
                join mod in _context.Modelo on e.Id_modelo equals mod.Id_modelo
                join tipo in _context.Tipo_modelo on e.Id_tipoequipo equals tipo.Id_tipomodelo
                join con in _context.Contrato on e.Id_contrato equals con.Id_contrato
                where e.Activo
                select new DtoEquipoDescripcion
                    {
                        Serie = e.Serie,
                        Nombre = e.Nombre,
                        Observacion = e.Observacion,

                        Id_estado = e.Id_estado,
                        DescripcionEstado = est.Descripcion,

                        Id_modelo = e.Id_modelo,
                        DescripcionModelo = mod.Descripcion,

                        Id_tipoequipo = e.Id_tipoequipo,
                        DescripcionTipo = tipo.Descripcion,

                        Id_contrato = e.Id_contrato,
                        DescripcionContrato = con.Nomcontrato
                    }
                ).AsNoTracking().ToListAsync();

            return Ok(models);
        }

        [HttpGet("equipo/{serie}")]
        public async Task<ActionResult<DtoEquipoDescripcion>> GetEquipoSerie(string serie)
        {
            var equipo = await (
                from e in _context.Equipo
                join est in _context.Estado on e.Id_estado equals est.Id_estado
                join mod in _context.Modelo on e.Id_modelo equals mod.Id_modelo
                join tipo in _context.Tipo_modelo on e.Id_tipoequipo equals tipo.Id_tipomodelo
                join con in _context.Contrato on e.Id_contrato equals con.Id_contrato
                where e.Activo && e.Serie == serie
                select new DtoEquipoDescripcion
                {
                    Serie = e.Serie,
                    Nombre = e.Nombre,
                    Observacion = e.Observacion,

                    Id_estado = e.Id_estado,
                    DescripcionEstado = est.Descripcion,

                    Id_modelo = e.Id_modelo,
                    DescripcionModelo = mod.Descripcion,

                    Id_tipoequipo = e.Id_tipoequipo,
                    DescripcionTipo = tipo.Descripcion,

                    Id_contrato = e.Id_contrato,
                    DescripcionContrato = con.Nomcontrato
                }
                ).AsNoTracking().ToListAsync();

            if (equipo == null)
                return NotFound();

            return Ok(equipo);
        }

        [HttpPost("crearEquipo")]
        public async Task<IActionResult> crearEquipo([FromBody] DtoEquipo dto)
        {
            var model = new Equipo
            {
                Serie = dto.Serie,
                Nombre = dto.Nombre,
                Observacion = string.IsNullOrWhiteSpace(dto.Observacion) ? "" : dto.Observacion,
                Id_estado = dto.Id_estado,
                Id_modelo = dto.Id_modelo,
                Id_tipoequipo = dto.Id_tipoequipo,
                Id_contrato = dto.Id_contrato,
                Activo = true
            };

            _context.Equipo.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("eliminarEquipo/{serie}")]
        public async Task<IActionResult> DesactivarEquipo(string serie)
        {
            var model = await _context.Equipo
                                           .FirstOrDefaultAsync(p => p.Serie == serie);

            if (model == null)
                return NotFound("Equipo no encontrado.");

            model.Activo = false;

            _context.Equipo.Update(model);
            await _context.SaveChangesAsync();

            return Ok("Equipo desactivado correctamente.");
        }

        //MARCA
        [HttpGet("marcas")]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas()
        {
            var models = await _context.Marca
                .Where(p => p.Activo)
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
                .Where(p => p.Activo)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("modelo/{idMarca}")]
        public async Task<ActionResult<IEnumerable<Modelo>>> GetModeloId(int idMarca)
        {
            var models = await _context.Modelo
                .Where(p => p.Activo && p.Id_marca == idMarca)
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
                .Where(p => p.Activo)
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
                .Where(p => p.Activo)
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
                .Where(p => p.Activo)
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

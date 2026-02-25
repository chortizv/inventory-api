using inventory_api.Dtos;
using inventory_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace inventory_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        //USUARIO
        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var models = await _context.Usuario
                .Where(p => p.Activo)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpGet("usuario/{id}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarioId(int id)
        {
            var models = await _context.Usuario
                .Where(p => p.Activo && p.Id_usuario == id)
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        [HttpPost("crearUsuario")]
        public async Task<IActionResult> crearUsuario([FromBody] DtoUsuario dto)
        {
            var passwordHasher = new PasswordHasher<Usuario>();

            var model = new Usuario
            {
                Username = dto.Username,
                Correo = dto.Correo,
                Id_funcionario = dto.Id_funcionario,
                Fecha_creacion = DateTime.UtcNow,
                Activo = true
            };

            model.Password = passwordHasher.HashPassword(model, dto.Password);

            _context.Usuario.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("eliminarUsuario/{id}")]
        public async Task<IActionResult> DesactivarUsuario(int id)
        {
            var model = await _context.Usuario
                                           .FirstOrDefaultAsync(p => p.Id_usuario == id);

            if (model == null)
                return NotFound("Usuario no encontrado.");

            model.Activo = false;

            _context.Entry(model).Property(x => x.Fecha_creacion).IsModified = false;

            await _context.SaveChangesAsync();

            return Ok("Usuario desactivado correctamente.");
        }

        [HttpPost("usuario/login")]
        public async Task<IActionResult> Login([FromBody] DtoLogin dto)
        {
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Username == dto.Username && u.Activo);

            if (usuario == null)
                return Unauthorized(new DtoError{ 
                    Autorizado = false,
                    Mensaje = "Usuario no encontrado o inactivo"
                });

            var passwordHasher = new PasswordHasher<Usuario>();

            var resultado = passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.Password,
                dto.Password
            );

            if (resultado == PasswordVerificationResult.Failed)
                return Unauthorized(new DtoError
                {
                    Autorizado = false,
                    Mensaje = "Password incorrecta"
                });

            return Ok(new DtoError
            {
                Autorizado = true,
                Mensaje = "Login Exitoso"
            });
        }
    }
}

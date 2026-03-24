using inventory_api.Dtos;
using inventory_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace inventory_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] DtoLogin dto)
        {
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Username == dto.Username && u.Activo);

            if (usuario == null)
                return Unauthorized(new { Autorizado = false, Mensaje = "Usuario no encontrado" });

            var passwordHasher = new PasswordHasher<Usuario>();
            var resultado = passwordHasher.VerifyHashedPassword(usuario, usuario.Password, dto.Password);

            if (resultado == PasswordVerificationResult.Failed)
                return Unauthorized(new { Autorizado = false, Mensaje = "Password incorrecta" });

            var jwtConfig = _configuration.GetSection("Jwt");
            var secretKey = jwtConfig["Key"];
            var issuer = jwtConfig["Issuer"];
            var audience = jwtConfig["Audience"];
            var expiresMinutes = double.Parse(jwtConfig["ExpiresMinutes"]);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id_usuario", usuario.Id_usuario.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: creds
            );

            return Ok(new
            {
                Autorizado = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = DateTime.UtcNow.AddMinutes(expiresMinutes)
            });
        }

        [HttpPut("usuario/actualizarContrasena")]
        public async Task<IActionResult> ActualizarContrasena([FromBody] DtoActualizarContrasena dto)
        {
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Id_usuario == dto.Id_usuario && u.Activo);
            if (usuario == null)
                return NotFound("Usuario no encontrado o inactivo");
            var passwordHasher = new PasswordHasher<Usuario>();
            var resultado = passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.Password,
                dto.ActuallyPassword
            );
            if (resultado == PasswordVerificationResult.Failed)
                return Unauthorized("Contraseña actual incorrecta");
            usuario.Password = passwordHasher.HashPassword(usuario, dto.NewPassword);

            _context.Usuario.Update(usuario);

            _context.Entry(usuario).Property(x => x.Fecha_creacion).IsModified = false;

            await _context.SaveChangesAsync();
            return Ok("Contraseña actualizada correctamente");
        }

        [HttpPut("usuario/actualizarUsuario")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] DtoUserUpdate dto)
        {
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Id_usuario == dto.Id_usuario && u.Activo);
            if (usuario == null)
                return NotFound("Usuario no encontrado o inactivo");
            usuario.Username = dto.Username != "" ? dto.Username : usuario.Username;
            usuario.Correo = dto.Correo != "" ? dto.Correo : usuario.Correo;
            usuario.Id_funcionario = dto.Id_funcionario;
            _context.Usuario.Update(usuario);
            _context.Entry(usuario).Property(x => x.Fecha_creacion).IsModified = false;
            await _context.SaveChangesAsync();
            return Ok("Usuario actualizado correctamente");
        }
    }
}

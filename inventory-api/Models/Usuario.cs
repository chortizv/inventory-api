namespace inventory_api.Models
{
    public class Usuario
    {
        public int Id_usuario { get; set; }
        public string Username { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public Boolean Activo { get; set; }
        public DateTime Fecha_creacion { get; set; }
        public int Id_funcionario { get; set; }

        public Usuario()
        {
        }
    }
}

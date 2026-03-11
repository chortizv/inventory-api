namespace inventory_api.Dtos
{
    public class DtoUserUpdate
    {
        public int Id_usuario { get; set; }
        public string Username { get; set; }
        public string Correo { get; set; }
        public int Id_funcionario { get; set; }
    }
}

namespace inventory_api.Dtos
{
    public class DtoActualizarContrasena
    {
        public int Id_usuario { get; set; }
        public string ActuallyPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

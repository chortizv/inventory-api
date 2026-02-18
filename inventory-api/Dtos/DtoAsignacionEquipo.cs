namespace inventory_api.Dtos
{
    public class DtoAsignacionEquipo
    {
        public int Id_funcionario { get; set; }
        public string Serie { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public DateTime? Fecha_fin { get; set; }
        public int Estado { get; set; }
        public string? Observacion { get; set; }
        public IFormFile Archivo { get; set; }
    }
}

namespace inventory_api.Dtos
{
    public class DtoEquipoDescripcion
    {
        public string Serie { get; set; }
        public string Nombre { get; set; }
        public string? Observacion { get; set; }
        public int Id_estado { get; set; }
        public string DescripcionEstado { get; set; }
        public int Id_modelo { get; set; }
        public string DescripcionModelo { get; set; }
        public int Id_tipoequipo { get; set; }
        public string DescripcionTipo { get; set; }
        public int Id_contrato { get; set; }
        public string DescripcionContrato { get; set; }
    }
}

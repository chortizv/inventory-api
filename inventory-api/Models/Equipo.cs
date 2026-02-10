namespace inventory_api.Models
{
    public class Equipo
    {
        public string Serie { get; set; }
        public string Nombre { get; set; }
        public string? Observacion { get; set; }
        public int Id_estado { get; set; }
        public int Id_modelo { get; set; }
        public int Id_tipoequipo { get; set; }
        public int Id_contrato { get; set; }
        public Boolean Activo { get; set; }

        public Equipo()
        {
        }
    }
}

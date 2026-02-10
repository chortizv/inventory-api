namespace inventory_api.Models
{
    public class Asignacion_equipo
    {
        public int Id_asignacion { get; set; }
        public int Id_funcionario { get; set; }
        public string Serie { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public DateTime? Fecha_fin { get; set; }
        public int Estado { get; set; }
        public string Url_archivo { get; set; }
        public string? Observacion { get; set; }
        public Boolean Activo { get; set; }

        public Asignacion_equipo()
        {
        }
    }
}

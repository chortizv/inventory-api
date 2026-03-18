namespace inventory_api.Models
{
    public class Cinta
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public DateTime? Fecha_Respaldo { get; set; }
        public string Ubicacion { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public Boolean Activo { get; set; }
    }
}

namespace inventory_api.Models
{
    public class Modelo
    {
        public int Id_modelo { get; set; }
        public string Descripcion { get; set; }
        public int Id_marca { get; set; }
        public Boolean Activo { get; set; }

        public Modelo()
        {
        }
    }
}

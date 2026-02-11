namespace inventory_api.Models
{
    public class Seccion
    {
        public int Id_seccion { get; set; }
        public string Descripcion { get; set; }
        public int Id_subdep { get; set; }
        public Boolean Activo { get; set; }

        public Seccion()
        {
        }
    }
}

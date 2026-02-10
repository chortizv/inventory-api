namespace inventory_api.Models
{
    public class Subdepartamento
    {
        public int Id_subdep { get; set; }
        public string Descripcion { get; set; }
        public int Id_dep { get; set; }
        public Boolean Activo { get; set; }

        public Subdepartamento()
        {
        }
    }
}

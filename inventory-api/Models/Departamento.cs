using System.Numerics;

namespace inventory_api.Models
{
    public class Departamento
    {
        public int Id_dep { get; set; }
        public string Descripcion { get; set; }
        public Boolean Activo { get; set; }

        public Departamento()
        {
        }
    }
}

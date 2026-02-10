namespace inventory_api.Models
{
    public class Funcionario
    {
        public int Id_funcionario { get; set; }
        public string Pnombre { get; set; }
        public string? Snombre { get; set; }
        public string Appaterno { get; set; }
        public string Apmaterno { get; set; }
        public string Correo { get; set; }
        public int? Anexo { get; set; }
        public string Cargo { get; set; }
        public Boolean Teletrabajo { get; set; }
        public Boolean Notebook { get; set; }
        public Boolean Validado { get; set; }
        public int Id_seccion { get; set; }
        public int Id_prioridad { get; set; }
        public Boolean Activo { get; set; }

        public Funcionario()
        {
        }
    }
}

namespace inventory_api.Dtos
{
    public class DtoFuncionarioDes
    {
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

        public string DescripcionPrioridad { get; set; }
        public int Id_prioridad { get; set; }

        public string DescripcionDepto { get; set; }
        public int Id_Dep { get; set; }

        public string DescripcionSubDepto { get; set; }
        public int Id_Subdep { get; set; }
        
        public string DescripcionSeccion { get; set; }
        public int Id_seccion { get; set; }
    }
}

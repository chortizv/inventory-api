using Microsoft.EntityFrameworkCore;

namespace inventory_api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Subdepartamento> Subdepartamento { get; set; }
        public DbSet<Seccion> Seccion { get; set; }
        public DbSet<Prioridad> Prioridad { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Contrato> Contrato { get; set; }
        public DbSet<Tipo_modelo> Tipo_modelo { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Modelo> Modelo { get; set; }
        public DbSet<Equipo> Equipo { get; set; }
        public DbSet<Asignacion_equipo> Asignacion_Equipo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("departamento", "public");
                entity.HasKey(e => e.Id_dep);
                entity.Property(e => e.Id_dep)
                    .HasColumnName("id_dep");
                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion");
                entity.Property(e => e.Activo)
                    .HasColumnName("activo");
            });




            modelBuilder.Entity<Subdepartamento>(entity =>
            {
                entity.ToTable("subdepartamento", "public");
                entity.HasKey(e => e.Id_subdep);
                entity.Property(e => e.Id_subdep)
                    .HasColumnName("id_subdep");
                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion");
                entity.Property(e => e.Id_dep)
                    .HasColumnName("id_dep");
                entity.Property(e => e.Activo)
                    .HasColumnName("activo");

                //Foreign Key
            });

            modelBuilder.Entity<Seccion>(entity =>
            {
                entity.ToTable("seccion", "public");
                entity.HasKey(e => e.Id_seccion);
                entity.Property(e => e.Id_seccion)
                    .HasColumnName("id_seccion");
                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion");
                entity.Property(e => e.Id_subdep)
                    .HasColumnName("id_subdep");
                entity.Property(e => e.Activo)
                    .HasColumnName("activo");

                //Foreign Key
            });

            modelBuilder.Entity<Prioridad>(entity =>
            {
                entity.ToTable("prioridad", "public");
                entity.HasKey(e => e.Id_prioridad);
                entity.Property(e => e.Id_prioridad)
                    .HasColumnName("id_prioridad");
                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion");
                entity.Property(e => e.Activo)
                    .HasColumnName("activo");

                //Foreign Key
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario", "public");
                entity.HasKey(e => e.Id_usuario);
                entity.Property(e => e.Id_usuario)
                    .HasColumnName("id_usuario");
                entity.Property(e => e.Username)
                    .HasColumnName("username");
                entity.Property(e => e.Correo)
                    .HasColumnName("correo");
                entity.Property(e => e.Password)
                    .HasColumnName("password");
                entity.Property(e => e.Activo)
                    .HasColumnName("activo");
                entity.Property(e => e.Fecha_creacion)
                    .HasColumnName("fecha_creacion");
                entity.Property(e => e.Id_funcionario)
                    .HasColumnName("id_funcionario");

                //Foreign Key
            });

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.ToTable("funcionario", "public");
                entity.HasKey(e => e.Id_funcionario);
                entity.Property(e => e.Id_funcionario)
                    .HasColumnName("id_funcionario");
                entity.Property(e => e.Pnombre)
                    .HasColumnName("pnombre");
                entity.Property(e => e.Snombre)
                    .HasColumnName("snombre");
                entity.Property(e => e.Appaterno)
                    .HasColumnName("appaterno");
                entity.Property(e => e.Apmaterno)
                    .HasColumnName("apmaterno");
                entity.Property(e => e.Correo)
                    .HasColumnName("correo");
                entity.Property(e => e.Anexo)
                    .HasColumnName("anexo");
                entity.Property(e => e.Cargo)
                    .HasColumnName("cargo");
                entity.Property(e => e.Teletrabajo)
                    .HasColumnName("teletrabajo");
                entity.Property(e => e.Notebook)
                    .HasColumnName("notebook");
                entity.Property(e => e.Validado)
                    .HasColumnName("validado");
                entity.Property(e => e.Id_seccion)
                    .HasColumnName("id_seccion");
                entity.Property(e => e.Id_prioridad)
                    .HasColumnName("id_prioridad");
                entity.Property(e => e.Activo)
                    .HasColumnName("activo");

                // Foreign Key
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("estado", "public");
                entity.HasKey(e => e.Id_estado);
                entity.Property(e => e.Id_estado)
                    .HasColumnName("id_estado");
                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion");
                entity.Property(e => e.Activo)
                    .HasColumnName("activo");

                // Foreign Key
            });

            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.ToTable("contrato", "public");
                entity.HasKey(e => e.Id_contrato);
                entity.Property(e => e.Id_contrato)
                    .HasColumnName("id_contrato");
                entity.Property(e => e.Nomcontrato)
                    .HasColumnName("nomcontrato");
                entity.Property(e => e.Activo)
                    .HasColumnName("activo");

                // Foreign Key
            });

            modelBuilder.Entity<Tipo_modelo>(entity =>
            {
                entity.ToTable("tipo_modelo", "public");
                entity.HasKey(e => e.Id_tipomodelo);
                entity.Property(e => e.Id_tipomodelo)
                    .HasColumnName("id_tipomodelo");
                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion");
                entity.Property(e => e.Activo)
                    .HasColumnName("activo");

                // Foreign Key
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("marca", "public");
                entity.HasKey(e => e.Id_marca);
                entity.Property(e => e.Id_marca)
                    .HasColumnName("id_marca");
                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion");
                entity.Property(e => e.Activo)
                    .HasColumnName("activo");

                // Foreign Key
            });

            modelBuilder.Entity<Modelo>(entity =>
            {
                entity.ToTable("modelo", "public");
                entity.HasKey(e => e.Id_modelo);
                entity.Property(e => e.Id_modelo)
                    .HasColumnName("id_modelo");
                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion");
                entity.Property(e => e.Id_marca)
                    .HasColumnName("id_marca");
                entity.Property(e => e.Activo)
                    .HasColumnName("activo");

                // Foreign Key
            });

            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.ToTable("equipo", "public");
                entity.HasKey(e => e.Serie);
                entity.Property(e => e.Serie)
                    .HasColumnName("serie");
                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre");
                entity.Property(e => e.Observacion)
                    .HasColumnName("observacion");
                entity.Property(e => e.Id_estado)
                    .HasColumnName("id_estado");
                entity.Property(e => e.Id_modelo)
                    .HasColumnName("id_modelo");
                entity.Property(e => e.Id_tipoequipo)
                    .HasColumnName("id_tipoequipo");
                entity.Property(e => e.Id_contrato)
                    .HasColumnName("id_contrato");
                entity.Property(e => e.Activo)
                    .HasColumnName("activo");

                // Foreign Key
                //entity.HasOne(e => e.Id_estado)
                //    .WithMany()
                //    .HasForeignKey(e => e.Id_estado);

                //entity.HasOne(e => e.Modelo)
                //    .WithMany()
                //    .HasForeignKey(e => e.IdModelo);

                //entity.HasOne(e => e.Contrato)
                //    .WithMany()
                //    .HasForeignKey(e => e.IdContrato);
            });

            modelBuilder.Entity<Asignacion_equipo>(entity =>
            {
                entity.ToTable("asignacion_equipo", "public");
                entity.HasKey(e => e.Id_asignacion);
                entity.Property(e => e.Id_asignacion)
                    .HasColumnName("id_asignacion");
                entity.Property(e => e.Id_funcionario)
                    .HasColumnName("id_funcionario");
                entity.Property(e => e.Serie)
                    .HasColumnName("serie");
                entity.Property(e => e.Fecha_inicio)
                    .HasColumnName("fecha_inicio");
                entity.Property(e => e.Fecha_fin)
                    .HasColumnName("fecha_fin");
                entity.Property(e => e.Estado)
                    .HasColumnName("estado");
                entity.Property(e => e.Url_archivo)
                    .HasColumnName("url_archivo");
                entity.Property(e => e.Observacion)
                    .HasColumnName("observacion");
                entity.Property(e => e.Activo)
                    .HasColumnName("activo");

                // Foreign Key
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

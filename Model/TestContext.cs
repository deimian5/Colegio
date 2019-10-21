namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestContext : DbContext
    {
        public TestContext()
            : base("name=TestContext")
        {
        }

        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<Materia> Materia { get; set; }
        public virtual DbSet<Profesor> Profesor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>()
                .Property(e => e.Nombres)
                .IsUnicode(false);

            modelBuilder.Entity<Alumno>()
                .Property(e => e.Documento)
                .IsUnicode(false);

            modelBuilder.Entity<Alumno>()
                .HasMany(e => e.Materias)
                .WithMany(e => e.Alumno)
                .Map(m => m.ToTable("AlumnoMateria").MapLeftKey("Alumno_id").MapRightKey("Materia_id"));

            modelBuilder.Entity<Materia>()
                .Property(e => e.Nombre_Materia)
                .IsUnicode(false);

            modelBuilder.Entity<Materia>()
                .HasMany(e => e.Profesor)
                .WithMany(e => e.Materias)
                .Map(m => m.ToTable("ProfesorMateria").MapLeftKey("Materia_id").MapRightKey("Profesor_id"));

            modelBuilder.Entity<Profesor>()
                .Property(e => e.Nombre_Profesor)
                .IsUnicode(false);
        }
    }
}

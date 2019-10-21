namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Sql;
    using System.Data.SqlClient;

    [Table("Materia")]
    public partial class Materia
    {
        public Materia()
        {
            Alumno = new HashSet<Alumno>();
            Profesor = new HashSet<Profesor>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de la materia")]
        [StringLength(50)]
        public string Nombre_Materia { get; set; }

        public virtual ICollection<Alumno> Alumno { get; set; }

        public virtual ICollection<Profesor> Profesor { get; set; }


        public List<Materia> Todo()
        {
            var materias = new List<Materia>();
            try
            {
                using (var context = new TestContext())
                {
                    materias = context.Materia.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return materias;
        }

        public void GuardarMateria()
        {
            try
            {
                using (var context = new TestContext())
                {
                    context.Database.ExecuteSqlCommand("SP_CREAR_MATERIA  @Nombre_Materia",  new SqlParameter("@Nombre_Materia", Nombre_Materia));

                }
            }
            catch (Exception e)
            {
                throw e;
                //throw new Exception(e.Message);
            }
        }


    }
}

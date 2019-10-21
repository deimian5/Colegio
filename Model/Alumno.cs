namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Linq;

    [Table("Alumno")]
    public partial class Alumno
    {
        public Alumno()
        {
            Materias = new HashSet<Materia>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar los Nombres")]
        [StringLength(50)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Documento")]
        [StringLength(100)]
        public string Documento { get; set; }

        public int? Telefono { get; set; }

        public ICollection<Materia> Materias { get; set; }

        public List<Alumno> Listar()
        {
            var alumnos = new List<Alumno>();
            try
            {
                using (var context = new TestContext())
                {
                    alumnos = context.Alumno.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return alumnos;
        }

        //Muestra alumnos con las materias asignadas
        public Alumno ObtenerAlumno(int id)
        {
            var alumno = new Alumno();
            try
            {
                //Consulta alumnos con materias 
                using (var context = new TestContext())
                {
                    alumno = context.Alumno
                                    .Include("Materias")
                                    .Where(x => x.Id == id)
                                    .Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return alumno;
        }

        public void GuardarAlumno()
        {
            try
            {
                using (var context = new TestContext())
                {

                    
                    int Opcion = (this.Id == 0 ? Opcion = 0 : Opcion = 1);
                    
                    context.Database.ExecuteSqlCommand("SP_CREAR_ALUMNO @ID, @Nombres,  @Documento, @Telefono, @Id_Materia",
                                                                new SqlParameter("@Id", Id),
                                                                new SqlParameter("@Nombres", Nombres),
                                                                new SqlParameter("@Documento", Documento),
                                                                new SqlParameter("@Telefono", Telefono),
                                                                new SqlParameter("@Id_Materia", 1));

                    foreach (var c in this.Materias)
                    {
                        context.Database.ExecuteSqlCommand("SP_ASIGNAR_ALUMNO_MATERIA @Opcion, @Alumno_id, @Materia_id",
                                                                    new SqlParameter("@Opcion", Opcion),
                                                                    new SqlParameter("@Alumno_id", Id),
                                                                    new SqlParameter("@Materia_id", c.Id.ToString()));
                    }


                }
            }
            catch (Exception e)
            {
                throw e;
                //throw new Exception(e.Message);
            }
        }

        public void EliminarAlumno(int id)
        {
            try
            {
                using (var context = new TestContext())
                {
                    context.Database.ExecuteSqlCommand("SP_ELIMINAR_ALUMNO @ID",
                                                                 new SqlParameter("@Id", id));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

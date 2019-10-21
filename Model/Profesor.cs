namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Linq;

    [Table("Profesor")]
    public partial class Profesor
    {
        public Profesor()
        {
            Materias = new HashSet<Materia>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar los Nombres")]
        [StringLength(50)]
        public string Nombre_Profesor { get; set; }

        public ICollection<Materia> Materias { get; set; }


        public List<Profesor> Listar()
        {
            var profesor = new List<Profesor>();
            try
            {
                using (var context = new TestContext())
                {
                    profesor = context.Profesor.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return profesor;
        }

        //Muestra alumnos con las materias asignadas
        public Profesor ObtenerProfesor(int id)
        {
            var profesor = new Profesor();
            try
            {
                //Consulta alumnos con materias 
                using (var context = new TestContext())
                {
                    profesor = context.Profesor
                                    .Include("Materias")
                                    .Where(x => x.Id == id)
                                    .Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return profesor;
        }

        public void GuardarProfesor()
        {
            try
            {
                using (var context = new TestContext())
                {


                    int Opcion = (this.Id == 0 ? Opcion = 0 : Opcion = 1);

                    context.Database.ExecuteSqlCommand("SP_CREAR_PROFESOR @ID, @Nombre_Profesor",
                                                                new SqlParameter("@Id", Id),
                                                                new SqlParameter("@Nombre_Profesor", Nombre_Profesor));

                    foreach (var c in this.Materias)
                    {
                        context.Database.ExecuteSqlCommand("SP_ASIGNAR_PROFESOR_MATERIA @Opcion, @Profesor_id, @Materia_id",
                                                                    new SqlParameter("@Opcion", Opcion),
                                                                    new SqlParameter("@Profesor_id", Id),
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

        public void EliminarProfesor(int id)
        {
            try
            {
                using (var context = new TestContext())
                {
                    context.Database.ExecuteSqlCommand("SP_ELIMINAR_PROFESOR @ID",
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

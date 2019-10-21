using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Colegio.Controllers
{
    public class HomeController : Controller
    {
        private Alumno alumno = new Alumno();
        private Profesor profesor = new Profesor();
        private Materia materia = new Materia();

        public ActionResult Index()
        {
            return View(alumno.Listar());
        }

        public ActionResult VerAlumno(int id)
        {
            return View(alumno.ObtenerAlumno(id));
        }

        public ActionResult AgregarAlumno(int id = 0)
        {
            //Repositorio que guarda el listado de los cursos
            ViewBag.Materias = materia.Todo();
            return View(id > 0 ?
                 alumno.ObtenerAlumno(id)
                 : alumno);
        }

        public ActionResult EliminarAlumno(int id)
        {
            alumno.EliminarAlumno(id);
            return Redirect("~/home");
        }

        [HttpPost]
        public JsonResult GuardarAlumno(Alumno model, int[] ChekMateriaTomada = null)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/home/AgregarAlumno/" + model.Id,
                error = ""
            };

            if (ChekMateriaTomada != null)
            {
                foreach (var c in ChekMateriaTomada)
                {
                    model.Materias.Add(new Materia { Id = c });
                }
            }
            else
            {
                ModelState.AddModelError("materias-elegidas", "Debe seleccionar por lo menos un curso.");
                respuesta.respuesta = false;
                respuesta.error = "Debe seleccionar por lo menos un curso.";
            }

            if (ModelState.IsValid)
            {
                model.GuardarAlumno();
            }
            return Json(respuesta);

        }

        public ActionResult ListaProfesores()
        {
            return View(profesor.Listar());
        }

        public ActionResult VerProfesor(int id)
        {
            return View(profesor.ObtenerProfesor(id));
        }

        public ActionResult AgregarProfesor(int id = 0)
        {
            //Repositorio que guarda el listado de los cursos
            ViewBag.Materias = materia.Todo();
            return View(id > 0 ?
                 profesor.ObtenerProfesor(id)
                 : profesor);
        }

        [HttpPost]
        public JsonResult GuardarProfesor(Profesor model, int[] ChekMateriaAsignada = null)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/home/AgregarProfesor/" + model.Id,
                error = ""
            };

            if (ChekMateriaAsignada != null)
            {
                foreach (var c in ChekMateriaAsignada)
                {
                    model.Materias.Add(new Materia { Id = c });
                }
            }
            else
            {
                ModelState.AddModelError("materias-elegidas", "Debe seleccionar por lo menos un curso.");
                respuesta.respuesta = false;
                respuesta.error = "Debe seleccionar por lo menos un curso.";
            }

            if (ModelState.IsValid)
            {
                model.GuardarProfesor();
            }
            return Json(respuesta);

        }

        public ActionResult EliminarProfesor(int id)
        {
            profesor.EliminarProfesor(id);
            return Redirect("~/home/ListaProfesores");
        }



        public ActionResult ListaMaterias()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GuardarMateria(Materia model)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/home/ListaMaterias/" + model,
                error = ""
            };

            if (ModelState.IsValid)
            {
                model.GuardarMateria();
            }

            return Json(respuesta);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
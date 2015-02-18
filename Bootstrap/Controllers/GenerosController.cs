using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using Modelos;

namespace Bootstrap.Controllers
{
    public class GenerosController : Controller
    {
        IGeneros _Generos;
        public GenerosController(IGeneros Genero)
        {
            _Generos = Genero;
        }
        //public GenerosController()
        //    : this(new Generos(new TrailersDeVideoJuegosEntities()))
        //{

        //}
        //
        // GET: /Generos/
        public ActionResult Index()
        {
            var Generos = _Generos.ObtenerGeneros();
            return View(Generos);
        }
        // GET: /Generos/Agregar
        public ActionResult Agregar()
        {
            return View();
        }

        // POST: /Generos/Agregar--
        [HttpPost]
        public ActionResult Agregar(GenerosDTO GenerosDTO)
        {
            if (ModelState.IsValid)
            {
                _Generos.AgregarGeneros(GenerosDTO);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Editar(int id)
        {
            var Genero = _Generos.ObtenerGenero(id);
            return View(Genero);
        }

        [HttpPost]
        public ActionResult Editar(GenerosDTO GenerosDTO)
        {
            if (ModelState.IsValid)
            {
                _Generos.ActualizarGeneros(GenerosDTO);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Eliminar(int id)
        {
            var Genero = _Generos.ObtenerGenero(id);
            return View(Genero);
        }

        [HttpPost]
        public ActionResult Eliminar(int id, FormCollection collection)
        {
            _Generos.EliminarGenero(id);
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _Generos.Dispose();
        }
    }
}
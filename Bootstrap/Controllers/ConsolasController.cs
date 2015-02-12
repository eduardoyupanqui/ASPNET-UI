using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using Modelos;

namespace Bootstrap.Controllers
{
    public class ConsolasController : Controller
    {
        IConsolas _consolas;
        public ConsolasController(IConsolas consola)
        {
            _consolas = consola;
        }
        public ConsolasController() :this(new Consolas())
        {

        }
        //
        // GET: /Consolas/
        public ActionResult Index()
        {
            var consolas = _consolas.ObtenerConsolas();
            return View(consolas);
        }
        // GET: /Consolas/Agregar
        public ActionResult Agregar()
        {
            return View();
        }

        // POST: /Consolas/Agregar--
        [HttpPost]
        public ActionResult Agregar(ConsolasDTO consolasDTO)
        {
            if (ModelState.IsValid)
            {
                _consolas.AgregarConsolas(consolasDTO);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Editar(int id)
        {
            var consola = _consolas.ObtenerConsola(id);
            return View(consola);
        }

        [HttpPost]
        public ActionResult Editar(ConsolasDTO consolasDTO)
        {
            if (ModelState.IsValid)
            {
                _consolas.ActualizarConsolas(consolasDTO);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Eliminar(int id)
        {
            var consola =_consolas.ObtenerConsola(id);
            return View(consola);
        }

        [HttpPost]
        public ActionResult Eliminar(int id, FormCollection collection)
        {
            _consolas.EliminarConsola(id);
            return RedirectToAction("Index");
        }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using PagedList;

namespace Foundation.Controllers
{
    public class JuegosController : Controller
    {
        IJuegos _juegos;
        IConsolas _consolas;
        IGeneros _generos;

        public JuegosController(IJuegos juegos, IConsolas consolas, IGeneros generos)
        {
            _juegos = juegos;
            _consolas = consolas;
            _generos = generos;
        }

        //public JuegosController() : this(new Juegos(), new Consolas(), new Generos())
        //{

        //}

        public ActionResult Index(string nombreconsola, string currentFilter, int? page, int generoId = 0, string criterioBusqueda = "")
        {
            var juegos = _juegos.ObtenerJuegos(nombreconsola, generoId, criterioBusqueda);

            ViewBag.UrlConsola = this.Url.Action("Index");

            var juego = juegos.FirstOrDefault();

            if (juego != null)
            {
                ViewBag.ConsolaId = juego.ConsolasDTO.Id;
                ViewBag.NombreConsola = juego.ConsolasDTO.Nombre;
            }
            else
            {
                ViewBag.ConsolaId = 0;
                ViewBag.NombreConsola = nombreconsola;
            }
            ViewBag.CriterioBusqueda = criterioBusqueda;

            //parametros del pagedlist
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(juegos.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult VerJuego(string nombreConsola, string nombreJuego)
        {
            var juego = _juegos.ObtenerJuego(nombreConsola, nombreJuego);

            string temp = this.Url.Action("Index");
            ViewBag.UrlConsola = this.Url.Action("Index");

            if (juego != null)
            {
                ViewBag.ConsolaId = juego.ConsolasDTO.Id;
                ViewBag.NombreConsola = juego.ConsolasDTO.Nombre;
            }
            else
            {
                ViewBag.ConsolaId = 0;
                ViewBag.NombreConsola = nombreConsola;
            }

            return View(juego);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            //_consolas.Dispose();
            //_generos.Dispose();
            //_juegos.Dispose();
        }
    }
}

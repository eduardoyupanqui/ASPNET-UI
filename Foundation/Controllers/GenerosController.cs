using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;

namespace Foundation.Controllers
{
    public class GenerosController : Controller
    {
        IGeneros _generos;
        public GenerosController(IGeneros generos)
        {
            _generos = generos;
        }

        public GenerosController()
            : this(new Generos())
        {

        }

        [ChildActionOnly]
        public ActionResult Index(string @class)
        {
            var generos = _generos.ObtenerGeneros();

            ViewBag.Class = @class;

            return PartialView(generos);
        }

    }
}

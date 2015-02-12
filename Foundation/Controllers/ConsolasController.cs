using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;

namespace Foundation.Controllers
{
    public class ConsolasController : Controller
    {

        IConsolas _consolas;
        public ConsolasController(IConsolas consolas)
        {
            _consolas = consolas;
        }

        public ConsolasController() : this(new Consolas())
        {

        }

        [ChildActionOnly]
        public ActionResult Index(string @class)
        {
            var consolas = _consolas.ObtenerConsolas();

            ViewBag.Class = @class;

            return PartialView(consolas);
        }

    }
}

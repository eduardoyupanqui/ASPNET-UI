using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using Modelos;
using PagedList;


namespace Bootstrap.Controllers
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
        //public JuegosController()
        //    : this(new Juegos(new TrailersDeVideoJuegosEntities()), new Consolas(new TrailersDeVideoJuegosEntities()), new Generos(new TrailersDeVideoJuegosEntities()))
        //{

        //}
        //
        // GET: /Juegos/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.Nombre = String.IsNullOrEmpty(sortOrder) ? "nombre_desc" : "";
            ViewBag.Genero = sortOrder == "genero" ? "genero_desc" : "genero";
            ViewBag.Consola = sortOrder == "consola" ? "consola_desc" : "consola";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var juegos = _juegos.ObtenerJuegos(searchString, sortOrder);

            return View(juegos.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Agregar()
        {
            var juego = new JuegosDTO();
            juego.ListaGeneros = _generos.ObtenerGeneros();
            juego.ListaConsolas = _consolas.ObtenerConsolas();
            return View(juego);
        }

        [HttpPost]
        public ActionResult Agregar(JuegosDTO juegoDTO)
        {
            if (ModelState.IsValid)
            {
                var guardarImagen = new GuardarImagen();
                if (juegoDTO.ListaImagenes ==  null)
                {
                    AgregarImagenPredeterminada(juegoDTO);
                }
                else
                {
                    foreach (var imagen in juegoDTO.ListaImagenes)
                    {
                        string fileName = Guid.NewGuid().ToString();

                        imagen.UrlImagenMiniatura = guardarImagen.ResizeAndSave(fileName, imagen.ImagenSubida.InputStream, Tamanos.Miniatura, false);
                        imagen.UrlImagenMediana = guardarImagen.ResizeAndSave(fileName, imagen.ImagenSubida.InputStream, Tamanos.Mediana, false);
                    }
                }

                _juegos.AgregarJuegos(juegoDTO);
            }

            return RedirectToAction("Index");

        }

        private void AgregarImagenPredeterminada(JuegosDTO juegoDTO)
        {
            if (juegoDTO.ListaImagenes == null)
            {
                //Agregar Una Imagen Default
                juegoDTO.ListaImagenes = new List<ImagenesDTO>() 
                {
                    new ImagenesDTO()
                    {
                        UrlImagenMiniatura = "~/content/imagenes/imagennodisponible_min.jpg",
                        UrlImagenMediana = "~/content/imagenes/imagennodisponible.jpg",
                        EsPortada = true
                    },
                    new ImagenesDTO()
                    {
                        UrlImagenMiniatura = "~/content/imagenes/imagennodisponible_min.jpg",
                        UrlImagenMediana = "~/content/imagenes/imagennodisponible.jpg",
                        EsPortada = false
                    }
                };
            }
        }

        public ActionResult Editar(int id)
        {
            var juegoDTO = _juegos.ObtenerJuego(id);
            juegoDTO.ListaConsolas = _consolas.ObtenerConsolas();
            juegoDTO.ListaGeneros = _generos.ObtenerGeneros();
            
            return View(juegoDTO);
        }

        [HttpPost]
        public ActionResult Editar(JuegosDTO juegoDTO)
        {

            if (ModelState.IsValid)
            {
                if (juegoDTO.ListaImagenes != null && juegoDTO.ListaImagenes.Any())
                {
                    foreach (var imagen in juegoDTO.ListaImagenes)
                    {
                        //Remover las imagenes marcadas como ImagenEliminada = true
                        if (!imagen.ImagenEliminada && imagen.ImagenSubida !=null)
                        {
                            string fileName = Guid.NewGuid().ToString();

                            imagen.UrlImagenMiniatura = new GuardarImagen().ResizeAndSave(fileName, imagen.ImagenSubida.InputStream, Tamanos.Miniatura, false);
                            imagen.UrlImagenMediana = new GuardarImagen().ResizeAndSave(fileName, imagen.ImagenSubida.InputStream, Tamanos.Mediana, false);

                            //juegoDTO.ListaImagenes.Concat(new [] {new ImagenesDTO() 
                            //{
                            //    UrlImagenMiniatura = imagen.UrlImagenMiniatura,
                            //    UrlImagenMediana = imagen.UrlImagenMediana
                            //}});
                        }
                    }
                }
                _juegos.ActualizarJuegos(juegoDTO);
                return RedirectToAction("Index");
            }
            juegoDTO.ListaConsolas = _consolas.ObtenerConsolas();
            juegoDTO.ListaGeneros = _generos.ObtenerGeneros();

            return View(juegoDTO);
        }

        public ActionResult Eliminar(int id) 
        {
            var juegoDTO = _juegos.ObtenerJuego(id);
            return View(juegoDTO);
        }

        [HttpPost]
        public ActionResult Eliminar(int id, FormCollection collection) 
        {
            _juegos.EliminarJuego(id);
            return RedirectToAction("Index");
        }

        public ActionResult VistaPrevia(int id)
        {
            var juegoDTO = _juegos.ObtenerJuego(id, false);
            return View(juegoDTO);
        }



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _consolas.Dispose();
            _generos.Dispose();
            _juegos.Dispose();
        }
	}
}
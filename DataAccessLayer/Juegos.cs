using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos;
using AutoMapper;

namespace DataAccessLayer
{
    public class Juegos : IJuegos
    {
        private TrailersDeVideoJuegosEntities _dbcontext;
        public Juegos(TrailersDeVideoJuegosEntities dbcontext)
        {
            _dbcontext = dbcontext;
            //->
            Mapper.CreateMap<JuegosDTO, Juego>();
            Mapper.CreateMap<ConsolasDTO, Consolas>();
            Mapper.CreateMap<GenerosDTO, Generos>();
            Mapper.CreateMap<ImagenesDTO, Imagenes>();
            //<-
            Mapper.CreateMap<Juego, JuegosDTO>();
            Mapper.CreateMap<Consola, ConsolasDTO>();
            Mapper.CreateMap<Genero, GenerosDTO>();
            Mapper.CreateMap<Imagenes,  ImagenesDTO>();
        }

        public void AgregarJuegos(JuegosDTO JuegoDTO)
        {
                //var nuevoJuego = new Juego();
                //nuevoJuego.Nombre = JuegoDTO.Nombre;
                //nuevoJuego.Descripcion = JuegoDTO.Descripcion;
                //nuevoJuego.ConsolaId = JuegoDTO.ConsolaId;
                //nuevoJuego.GeneroId = JuegoDTO.GeneroId;
                //nuevoJuego.Activo = JuegoDTO.Activo;
                //nuevoJuego.VideoYoutube = JuegoDTO.VideoYoutube;

                //foreach (var item in JuegoDTO.ListaImagenes)
                //{
                //    nuevoJuego.Imagenes.Add(new Imagenes() 
                //    {
                //        UrlImagenMiniatura = item.UrlImagenMiniatura,
                //        UrlImagenMediana = item.UrlImagenMediana,
                //        EsPortada = item.EsPortada
                //    });
                //}

                var nuevoJuego = Mapper.Map<Juego>(JuegoDTO);
                nuevoJuego.Imagenes = Mapper.Map<ICollection<Imagenes>>(JuegoDTO.ListaImagenes);

                _dbcontext.Juego.Add(nuevoJuego);
                _dbcontext.SaveChanges();
        }

        public JuegosDTO ObtenerJuego(int JuegoId, bool incluirPortada = true)
        {
                var juego = _dbcontext.Juego.FirstOrDefault(j => j.Id == JuegoId);

                var juegoDTO = Mapper.Map<JuegosDTO>(juego);
                juegoDTO.ConsolasDTO = Mapper.Map<ConsolasDTO>(juego.Consola);
                juegoDTO.GenerosDTO = Mapper.Map<GenerosDTO>(juego.Genero);

                if (incluirPortada)
                {
                     juegoDTO.ListaImagenes = Mapper.Map<IEnumerable<ImagenesDTO>>(juego.Imagenes);

                }
                else
                {
                    juegoDTO.ListaImagenes = Mapper.Map<IEnumerable<ImagenesDTO>>(juego.Imagenes.Where(i => !i.EsPortada));
                }
                return juegoDTO;

        }
        public JuegosDTO ObtenerJuego(string nombreConsola, string nombreJuego, bool incluirPortada = true)
        {
                nombreConsola = nombreConsola.Replace("-", " ");

                var juegos = _dbcontext.Juego.Where(r => r.Consola.Nombre.ToLower() == nombreConsola.ToLower()
                    && r.Nombre.StartsWith(nombreJuego.Substring(0, 1)));

                Juego juego = null;

                foreach (var item in juegos)
                {
                    if (RemoverCaracteresEspeciales(item.Nombre).ToLower() == nombreJuego.ToLower())
                    {
                        juego = item;
                        break;
                    }
                }

                var juegoDTO = Mapper.Map<JuegosDTO>(juego);

                if (juego != null)
                {
                    juegoDTO.ConsolasDTO = Mapper.Map<ConsolasDTO>(juego.Consola);
                    juegoDTO.GenerosDTO = Mapper.Map<GenerosDTO>(juego.Genero);

                    if (incluirPortada)
                    {
                        juegoDTO.ListaImagenes = Mapper.Map<IEnumerable<ImagenesDTO>>(juego.Imagenes);
                    }
                    else
                    {
                        juegoDTO.ListaImagenes = Mapper.Map<IEnumerable<ImagenesDTO>>(juego.Imagenes.Where(i => !i.EsPortada));
                    }
                }

                return juegoDTO;
        }

        public List<JuegosDTO> ObtenerJuegos()
        {
                var juegos = _dbcontext.Juego;

                var listaJuegos = new List<JuegosDTO>();

                foreach (var juego in juegos)
                {

                    var juegoDTO = Mapper.Map<JuegosDTO>(juego);
                    juegoDTO.ConsolasDTO = Mapper.Map<ConsolasDTO>(juego.Consola);
                    juegoDTO.GenerosDTO = Mapper.Map<GenerosDTO>(juego.Genero);

                    var imagen = juego.Imagenes.FirstOrDefault(r => r.EsPortada);

                    if (imagen != null)
                    {
                        juegoDTO.ListaImagenes = new List<ImagenesDTO>() { new ImagenesDTO() 
                            {
                                UrlImagenMiniatura = imagen.UrlImagenMiniatura,
                                UrlImagenMediana = imagen.UrlImagenMediana
                            }};
                    }

                    
                    listaJuegos.Add(juegoDTO);
                }

                return listaJuegos;
        }
        public List<JuegosDTO> ObtenerJuegos(string criteriodeBusqueda, string campoOrdenamiento)
        {
            var listaJuegos = new List<JuegosDTO>();

                var juegos = _dbcontext.Juego.AsQueryable();

                if (!String.IsNullOrEmpty(criteriodeBusqueda))
                {
                    juegos = (from j in _dbcontext.Juego
                                where
                                j.Nombre.ToUpper().Contains(criteriodeBusqueda.ToUpper())
                                ||
                                j.Consola.Nombre.ToUpper().Contains(criteriodeBusqueda.ToUpper())
                                ||
                                j.Genero.Nombre.ToUpper().Contains(criteriodeBusqueda.ToUpper())
                                select j);
                }

                switch (campoOrdenamiento)
                {
                    case "nombre_desc":
                        juegos = juegos.OrderByDescending(o => o.Nombre);
                        break; ;
                    case "consola":
                        juegos = juegos.OrderBy(o => o.Consola.Nombre);
                        break;
                    case "consola_desc":
                        juegos = juegos.OrderByDescending(o => o.Consola.Nombre);
                        break;
                    case "genero":
                        juegos = juegos.OrderBy(o => o.Genero.Nombre);
                        break;
                    case "genero_desc":
                        juegos = juegos.OrderByDescending(o => o.Genero.Nombre);
                        break;
                    default:  // Nombre ascending 
                        juegos = juegos.OrderBy(s => s.Nombre);
                        break;
                }

                foreach (var juego in juegos)
                {
                    var juegoDTO = Mapper.Map<JuegosDTO>(juego);
                    juegoDTO.ConsolasDTO = Mapper.Map<ConsolasDTO>(juego.Consola);
                    juegoDTO.GenerosDTO = Mapper.Map<GenerosDTO>(juego.Genero);

                    var imagen = juego.Imagenes.FirstOrDefault(r => r.EsPortada);

                    if (imagen != null)
                    {
                        juegoDTO.ListaImagenes = new List<ImagenesDTO>() { new ImagenesDTO() 
                        {
                            UrlImagenMiniatura = imagen.UrlImagenMiniatura,
                            UrlImagenMediana = imagen.UrlImagenMediana
                        }};
                    }

                    listaJuegos.Add(juegoDTO);
                }

                return listaJuegos;
        }
        public List<JuegosDTO> ObtenerJuegos(string nombreConsola, int generoId, string criterioBusqueda)
        {
            var listaJuegos = new List<JuegosDTO>();

            nombreConsola = nombreConsola.Replace("-", " ");


                var juegos = _dbcontext.Juego.Where(r => r.Consola.Nombre.ToLower() == nombreConsola.ToLower());

                if (generoId > 0)
                {
                    juegos = juegos.Where(r => r.GeneroId == generoId);
                }

                if (!string.IsNullOrEmpty(criterioBusqueda))
                {
                    juegos = juegos.Where(r => r.Nombre.ToLower().Contains(criterioBusqueda.ToLower()));
                }

                foreach (var juego in juegos)
                {
                    var juegoDTO = Mapper.Map<JuegosDTO>(juego);
                    juegoDTO.ConsolasDTO = Mapper.Map<ConsolasDTO>(juego.Consola);
                    juegoDTO.GenerosDTO = Mapper.Map<GenerosDTO>(juego.Genero);

                    var imagen = juego.Imagenes.FirstOrDefault(r => r.EsPortada);

                    if (imagen != null)
                    {
                        juegoDTO.ListaImagenes = new List<ImagenesDTO>() { new ImagenesDTO() 
                            {
                                UrlImagenMiniatura = imagen.UrlImagenMiniatura,
                                UrlImagenMediana = imagen.UrlImagenMediana
                            }};
                    }

                    juegoDTO.NombreJuegoParaUrl = RemoverCaracteresEspeciales(juego.Nombre);

                    listaJuegos.Add(juegoDTO);
                }

                return listaJuegos;
            
        }


        public void ActualizarJuegos(JuegosDTO juegoDTO)
        {
                var juego = _dbcontext.Juego.FirstOrDefault(r => r.Id == juegoDTO.Id);

                if (juego != null)
                {
                    //juego = Mapper.Map<Juego>(juegoDTO); //Crea una nueva instancia
                    Mapper.Map(juegoDTO, juego); //Actualiza la entidad

                    //juego.Imagenes = Mapper.Map<ICollection<Imagenes>>(juegoDTO.ListaImagenes);
                    Mapper.Map(juegoDTO.ListaImagenes, juego.Imagenes);

                    //Remover las imagenes marcadas como eliminadas
                    if (juegoDTO.ListaImagenes != null)
                    {
                        foreach (var imagen in juegoDTO.ListaImagenes)
                        {
                            if (imagen.ImagenEliminada)
                            {
                                var eliminarImagen = juego.Imagenes.FirstOrDefault(r => r.Id == imagen.Id);
                                if (eliminarImagen != null)
                                {
                                    juego.Imagenes.Remove(eliminarImagen);
                                }
                            }
                        }
                    }

                    _dbcontext.SaveChanges();
                }

        }

        public void EliminarJuego(int JuegoId)
        {
            
                var Juego = _dbcontext.Juego.FirstOrDefault(c => c.Id == JuegoId);
                if (Juego != null)
                {
                    _dbcontext.Juego.Remove(Juego);
                    _dbcontext.SaveChanges();
                }
        }

        private string RemoverCaracteresEspeciales(string nombreJuego)
        {
            var sb = new StringBuilder();
            var nuevoNombreJuego = string.Empty;

            foreach (char c in nombreJuego)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ')
                {
                    sb.Append(c);
                }
            }

            nuevoNombreJuego = sb.ToString();

            nuevoNombreJuego = nuevoNombreJuego.Replace(" ", "-");

            return nuevoNombreJuego;
        }

        public void Dispose()
        {
            this._dbcontext.Dispose();
        }
    }
}

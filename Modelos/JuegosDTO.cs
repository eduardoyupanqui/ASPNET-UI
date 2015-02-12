using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Modelos
{
    public class JuegosDTO
    {
        public JuegosDTO()
        {
            Activo = true;
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el Nombre")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese la Descipción del Juego")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        //Genero
        [Required(ErrorMessage = "Ingrese el género del Juego")]
        [Display(Name = "Genero")]
        public int GeneroId { get; set; }
        public GenerosDTO GenerosDTO { get; set; }
        //Consola
        [Required(ErrorMessage = "Ingrese la consola del Juego")]
        [Display(Name = "Consola")]
        public int ConsolaId { get; set; }
        public ConsolasDTO ConsolasDTO { get; set; }
        //Video Youtube
        [Required(ErrorMessage="Ingrese el video de Youtube")]
        [DataType(DataType.MultilineText)]
        [Display(Name="Video Youtube")]
        [AllowHtml]
        public string VideoYoutube { get; set; }
        public bool Activo { get; set; }


        public string NombreJuegoParaUrl { get; set; }

        //Lista imagenes
        public IEnumerable<ImagenesDTO> ListaImagenes { get; set; }

        public IEnumerable<GenerosDTO> ListaGeneros { get; set; }
        public IEnumerable<ConsolasDTO> ListaConsolas { get; set; }
    }
}

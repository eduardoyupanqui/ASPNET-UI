using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Web;
namespace Modelos
{
    public class ImagenesDTO
    {
        public int Id { get; set; }
        public int JuegoId { get; set; }
        public string UrlImagenMiniatura { get; set; }
        public string UrlImagenMediana { get; set; }
        public bool EsPortada { get; set; }

        public HttpPostedFileWrapper ImagenSubida { get; set; }
        public bool ImagenEliminada { get; set; }
    }
}

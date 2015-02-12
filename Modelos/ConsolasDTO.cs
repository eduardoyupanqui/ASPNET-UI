﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class ConsolasDTO // data transfer object
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre de la consola")]
        public string Nombre { get; set; }
    }
}

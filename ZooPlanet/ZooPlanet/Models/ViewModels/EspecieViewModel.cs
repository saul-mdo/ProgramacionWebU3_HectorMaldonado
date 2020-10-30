﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooPlanet.Models.ViewModels
{
    public class EspecieViewModel
    {
        public Especies Especies { get; set; }
        public IEnumerable<Clase> Clases { get; set; }
        public IFormFile Archivo { get; set; }

        public string Imagen { get; set; }
    }
}

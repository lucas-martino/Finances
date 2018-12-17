using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Finances.WebApp.Models
{
    public class CategoriaViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
        public string Icone { get; set; }
        public CategoriaViewModel Pai { get; set; }

        public IEnumerable<SelectListItem> Categorias { get; set;}
    }
}
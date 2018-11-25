using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Finances.WebApp.Models
{
    public class OrcamentoCategoriaViewModel
    {
        public long ID { get; set; }
        [Required]
        public long CategoriaID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        public string Categoria { get; set; }
        public string CategoriaCor { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set;}
    }
}
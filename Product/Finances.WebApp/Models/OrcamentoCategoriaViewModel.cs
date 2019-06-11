using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Finances.WebApp.Models
{
    public class OrcamentoCategoriaViewModel
    {
        public OrcamentoCategoriaViewModel()
        {
            Categoria = new CategoriaViewModel();
        }

        public ulong ID { get; set; }
        public ulong OrcamentoID { get; set; }
        [Required]
        public CategoriaViewModel Categoria { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set;}
    }
}
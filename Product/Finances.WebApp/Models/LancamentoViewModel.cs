using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Finances.WebApp.Models
{
    public class LancamentoViewModel
    {
        [Required]
        public long ID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public Decimal Valor { get; set; }
        public long CategoriaID { get; set; }
        public string Categoria { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        public string MinDate { get; set; }
        public string MaxDate { get; set; }

        public IEnumerable<SelectListItem> Categorias { get; set;}
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Finances.WebApp.Models
{
    public class GastoViewModel
    {
        public GastoViewModel()
        {
            Categoria = new CategoriaViewModel();
        }

        public ulong ID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public Decimal Valor { get; set; }
        public CategoriaViewModel Categoria { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        public string MinDate { get; set; }
        public string MaxDate { get; set; }

        public IEnumerable<SelectListItem> Categorias { get; set;}
    }

    public class GastoListaViewModel
    {
        public VigenciaViewModel Vigencia { get; set; }
        public IEnumerable<GastoViewModel> Gastos { get; set; }
    }
}
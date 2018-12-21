using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finances.WebApp.Models
{
    public class DemostrativoItemViewModel
    {
        public DemostrativoItemViewModel()
        {
            Categoria = new CategoriaViewModel();
        }
        
        public CategoriaViewModel Categoria { get; set; }
        [Display(Name = "Gasto")]        
        [DataType(DataType.Currency)]
        public decimal ValorGastoCompleto { get; set; }
        [DataType(DataType.Currency)]
        public decimal ValorGasto { get; set; }
        [Display(Name = "Orçamento")]
        [DataType(DataType.Currency)]
        public decimal Orcamento { get; set; }
        [DataType(DataType.Currency)]
        public decimal Planejamento { get; set; }
        public decimal Percentual { get; set; }
        public string Cor { get; set; }
    }
}
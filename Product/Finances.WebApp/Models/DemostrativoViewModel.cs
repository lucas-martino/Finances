using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finances.WebApp.Models
{
    public class DemostrativoViewModel
    {
        public DemostrativoViewModel()
        {
            Itens = new List<DemostrativoItemViewModel>();
        }

        [Display(Name = "Gasto Total")]
        [DataType(DataType.Currency)]
        public decimal ValorTotal { get; set; }
        public string Cor { get; set; }
        public decimal Percentual { get; set; }
        public IEnumerable<DemostrativoItemViewModel> Itens { get; set; }
    }

    public class DemostrativoItemViewModel
    {
        public long CategoriaID { get; set; }
        public string Categoria { get; set; }
        public string CategoriaCor { get;set; }
        [Display(Name = "Gasto")]        
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        [Display(Name = "Orçamento")]
        public string Orcamento { get; set; }
        public decimal Percentual { get; set; }
        public string Cor { get; set; }
    }
}
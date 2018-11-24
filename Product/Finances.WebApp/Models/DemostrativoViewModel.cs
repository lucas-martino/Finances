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
        public decimal ValorTotal { get; set; }
        public IEnumerable<DemostrativoItemViewModel> Itens { get; set; }
    }

    public class DemostrativoItemViewModel
    {
        public string Categoria { get; set; }
        [Display(Name = "Gasto")]
        public string Valor { get; set; }
        [Display(Name = "Orçamento")]
        public string Orcamento { get; set; }
    }
}
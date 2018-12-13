using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finances.WebApp.Models
{
    public class DemostrativoViewModel
    {
        public DemostrativoViewModel()
        {
            DemostrativoOrcamentosCategoria = new List<DemostrativoItemViewModel>();
            DemostrativoCategoria = new List<DemostrativoItemViewModel>();
        }

        [Display(Name = "Gasto Total")]
        [DataType(DataType.Currency)]
        public decimal ValorGastoTotal { get; set; }
        [DataType(DataType.Currency)]
        public decimal OrcamentoTotal { get; set; }
        public string Cor { get; set; }
        public bool PossuiNaoCategorizado { get; set; }
        public IEnumerable<DemostrativoItemViewModel> DemostrativoOrcamentosCategoria { get; set; }
        public IEnumerable<DemostrativoItemViewModel> DemostrativoCategoria { get; set; }
        public DemostrativoItemViewModel NaoCategorizado { get; set; }
    }

    public class DemostrativoItemViewModel
    {
        public DemostrativoItemViewModel()
        {
            Categoria = new CategoriaViewModel();
        }
        
        public CategoriaViewModel Categoria { get; set; }
        [Display(Name = "Gasto")]        
        [DataType(DataType.Currency)]
        public decimal ValorGasto { get; set; }
        [Display(Name = "Orçamento")]
        [DataType(DataType.Currency)]
        public decimal Orcamento { get; set; }
        public string Cor { get; set; }
    }
}
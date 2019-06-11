using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finances.WebApp.Models
{
    public class OrcamentoViewModel
    {
        public OrcamentoViewModel()
        {
            OrcamentosCategoria = new List<OrcamentoCategoriaViewModel>();
        }

        public ulong ID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        public VigenciaViewModel Vigencia { get; set; }

        public IEnumerable<OrcamentoCategoriaViewModel> OrcamentosCategoria { get; set; }
    }
}
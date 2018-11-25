using System.ComponentModel.DataAnnotations;

namespace Finances.WebApp.Models
{
    public class OrcamentoViewModel
    {
        [Required]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
    }
}
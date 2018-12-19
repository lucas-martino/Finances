using System;
using System.Collections.Generic;

namespace Finances.WebApp.Models
{
    public class HistoricoViewModel
    {
        public IEnumerable<VigenciaViewModel> Vigencias { get; set; }
    }
}
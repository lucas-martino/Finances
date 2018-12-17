using System.Collections.Generic;
using Framework.Domain.Entity;

namespace Finances.Domain.Entity
{
    public class Demonstrativo : DemonstrativoParcial
    {
        public Demonstrativo()
        {
            Categorias = new List<DemonstrativoItemCategoria>();
        }

        public IList<DemonstrativoItemCategoria> Categorias { get; set; }
    }
}
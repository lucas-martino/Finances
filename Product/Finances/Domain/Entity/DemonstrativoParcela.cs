using Framework.Domain.Entity;

namespace Finances.Domain.Entity
{
    public class DemonstrativoParcela : IValueObject
    {
        public Categoria Categoria { get; set; }
        public decimal Percentual { get; set;}
    }
}
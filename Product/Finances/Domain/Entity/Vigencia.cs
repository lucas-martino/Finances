namespace Finances.Domain.Entity
{
    public class Vigencia : FinancesDomainEntity
    {
        public int Referencia { get; set; }
        public Usuario Usuario { get; set; }

        public int Mes()
        {
            return Referencia % 100;
        }

        public int Ano()
        {
            return Referencia / 100;
        }
    }
}
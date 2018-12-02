using System.Linq;
using Finances.Domain.Entity;
using Xunit;

namespace Test.Finances.Domain.Entity
{
    public class OrcamentoTest
    {
        [Fact]
        public void AcessandoOrcamentoCategoria()
        {
            //Given
            Orcamento orcamento = new Orcamento() { ID = 1};
            OrcamentoCategoria orcamentoCategoria = new OrcamentoCategoria();

            //When
            orcamento.AddOrcamentoCategoria(orcamentoCategoria);

            //Then
            Assert.Equal(orcamento.ID, orcamento.GetOrcamentosCategoria().FirstOrDefault().Orcamento.ID);
        }
    }
}
using System.Linq;
using Finances.Domain.Entity;
using Test.Finances.Factory;
using Xunit;

namespace Test.Finances.Domain.Entity
{
    public class OrcamentoTest
    {
        [Fact]
        public void AcessandoOrcamentoCategoria()
        {
            //Given
            Orcamento orcamento = OrcamentoFactory.GetValid().WithID(1).Build();
            OrcamentoCategoria orcamentoCategoria = new OrcamentoCategoria();

            //When
            orcamento.AddOrcamentoCategoria(orcamentoCategoria);

            //Then
            Assert.Equal(orcamento.ID, orcamento.GetOrcamentosCategoria().FirstOrDefault().Orcamento.ID);
        }

        [Fact]
        public void VigenciaObrigatoria()
        {
            //Given
            Orcamento orcamento = GetOrcamentoValido();
            orcamento.Vigencia = null;            

            //When
            var result = orcamento.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Vigencia"));
        }

        [Fact]
        public void ValorNaoPodeSerNegativo()
        {
            Orcamento orcamento = GetOrcamentoValido();
            orcamento.Valor = -1;

            //When
            var result = orcamento.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Valor"));
        }

        private static Orcamento GetOrcamentoValido()
        {
            //Given
            return OrcamentoFactory.GetValid().Build();
        }
    }
}
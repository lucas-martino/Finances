using System.Linq;
using Finances.Domain.Entity;
using Test.Finances.Factory;
using Xunit;

namespace Test.Finances.Domain.Entity
{
    public class OrcamentoCategoriaTest
    {
        [Fact]
        public void OrcamentoObrigatorio()
        {
            //Given
            OrcamentoCategoria orcamento = GetOrcamentoCategoriaValido();
            orcamento.Orcamento = null;            

            //When
            var result = orcamento.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Orcamento"));
        }

        [Fact]
        public void CategoriaObrigatoria()
        {
            OrcamentoCategoria orcamento = GetOrcamentoCategoriaValido();
            orcamento.Categoria = null;

            //When
            var result = orcamento.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Categoria"));
        }

        [Fact]
        public void ValorPositivo()
        {
            OrcamentoCategoria orcamentoValorZero = GetOrcamentoCategoriaValido();
            orcamentoValorZero.Valor = 0;
            OrcamentoCategoria orcamentoMenorZero = GetOrcamentoCategoriaValido();
            orcamentoMenorZero.Valor = -1;

            //When
            var resultValorZero = orcamentoValorZero.Validate();
            var resultMenorZero = orcamentoMenorZero.Validate();

            //Then
            Assert.NotNull(resultValorZero);
            Assert.False(resultValorZero.IsValid);
            Assert.Equal(1, resultValorZero.Errors.Count);
            Assert.NotNull(resultValorZero.Errors.FirstOrDefault(i => i.PropertyName == "Valor"));

            Assert.NotNull(resultMenorZero);
            Assert.False(resultMenorZero.IsValid);
            Assert.Equal(1, resultMenorZero.Errors.Count);
            Assert.NotNull(resultMenorZero.Errors.FirstOrDefault(i => i.PropertyName == "Valor"));
        }

        private static OrcamentoCategoria GetOrcamentoCategoriaValido()
        {
            //Given
            return OrcamentoCategoriaFactory.GetValid().Build();
        }
    }
}
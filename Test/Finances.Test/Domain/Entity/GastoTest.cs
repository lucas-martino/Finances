using System.Linq;
using Finances.Domain.Entity;
using Test.Finances.Factory;
using Xunit;

namespace Test.Finances.Domain.Entity
{
    public class GastoTest
    {
        [Fact]
        public void ValorMaiorZero()
        {
            Gasto gastoIgualZero = GetGastoValido();
            gastoIgualZero.Valor = 0;

            Gasto gastoMenorZero = GetGastoValido();
            gastoMenorZero.Valor = -1;

            //When
            var resultIgualZero = gastoIgualZero.Validate();
            var resultMenorZero = gastoMenorZero.Validate();

            //Then
            Assert.NotNull(resultIgualZero);
            Assert.False(resultIgualZero.IsValid);
            Assert.Equal(1, resultIgualZero.Errors.Count);
            Assert.NotNull(resultIgualZero.Errors.FirstOrDefault(i => i.PropertyName == "Valor"));

            Assert.NotNull(resultMenorZero);
            Assert.False(resultMenorZero.IsValid);
            Assert.Equal(1, resultIgualZero.Errors.Count);
            Assert.NotNull(resultMenorZero.Errors.FirstOrDefault(i => i.PropertyName == "Valor"));
        }

        [Fact]
        public void VigenciaObrigatoria()
        {
            //Given
            Gasto gasto = GetGastoValido();
            gasto.Vigencia = null;
            
            //When
            var result  = gasto.Validate();
            
            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Vigencia"));
        }

        private static Gasto GetGastoValido()
        {
            //Given
            return GastoFactory.GetValid().Build();
        }

    }
}
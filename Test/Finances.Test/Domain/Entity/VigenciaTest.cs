using System.Linq;
using Finances.Domain.Entity;
using Test.Finances.Factory;
using Xunit;

namespace Test.Finances.Domain.Entity
{
    public class VigenciaTest
    {
        [Fact]
        public void MesAnoVigencia()
        {
            //Given
            Vigencia vigencia = new Vigencia();

            //When
            vigencia.Referencia = 201811;

            //Then
            Assert.Equal(2018, vigencia.Ano());
            Assert.Equal(11, vigencia.Mes());
        }

        [Fact]
        public void UsuarioObrigatorio()
        {
            //Given
            Vigencia vigencia = GetVigenciaValida();
            vigencia.Usuario = null;

            //When
            var result = vigencia.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Usuario"));
        }

        [Fact]
        public void ReferenciaObrigatoria()
        {
            //Given
            Vigencia vigencia = GetVigenciaValida();
            vigencia.Referencia = 0;

            //When
            var result = vigencia.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Referencia"));
        }

        private static Vigencia GetVigenciaValida()
        {
            return VigenciaFactory.GetValid().Build();
        }
    }
}
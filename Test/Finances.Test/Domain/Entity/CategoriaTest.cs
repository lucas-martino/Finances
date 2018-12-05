using System.Linq;
using Finances.Domain.Entity;
using Test.Finances.Factory;
using Xunit;

namespace Test.Finances.Domain.Entity
{
    public class CategoriaTest
    {
        [Fact]
        public void UsuarioObrigatorio()
        {
            //Given
            Categoria categoria = GetCategoriaValida();
            categoria.Usuario = null;

            //When
            var result = categoria.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Usuario"));
        }

        [Fact]
        public void NomeObrigatorio()
        {
            //Given
            Categoria categoria = GetCategoriaValida();
            categoria.Nome = "";

            //When
            var result = categoria.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Nome"));
        }

        [Fact]
        public void CorPadraoBlack()
        {
            Categoria categoria = new Categoria();

            //Then
            Assert.Equal("Black", categoria.Cor);
        }

        [Fact]
        public void CorObrigatorio()
        {
            //Given
            Categoria categoria = GetCategoriaValida();
            categoria.Cor = "";

            //When
            var result = categoria.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Cor"));
        }

        private static Categoria GetCategoriaValida()
        {
            return CategoriaFactory.GetValid().Build();
        }
    }
}
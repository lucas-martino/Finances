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
            Assert.Equal(2, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Nome"));
        }

        [Fact]
        public void CorPadrao()
        {
            Categoria categoria = new Categoria();

            //Then
            Assert.Equal("Darkgray", categoria.Cor);
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

        [Fact]
        public void Level1PermiteFilhos()
        {
            Categoria level1 = GetCategoriaValida();
            Categoria level2 = CategoriaFactory.GetValid().WithID(2).WithPai(level1).Build();

            var resultLevel1 = level1.Validate();
            var resultLevel2 = level1.Validate();

            Assert.True(level1.PermiteFilhos());
            Assert.True(resultLevel1.IsValid);
            Assert.True(resultLevel2.IsValid);
        }

        [Fact]
        public void Level2NaoPermiteFilhos()
        {
            Categoria level1 = GetCategoriaValida();
            Categoria level2 = CategoriaFactory.GetValid().WithID(2).WithPai(level1).Build();
            Categoria level3 = CategoriaFactory.GetValid().WithID(3).WithPai(level2).Build();

            var result = level3.Validate();

            Assert.False(level2.PermiteFilhos());
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Pai"));
        }

        [Fact]
        public void NaoPermiteSerFilhaDeSeMesma()
        {
            Categoria level1 = GetCategoriaValida();
            level1.Pai = level1;

            var resultLevel1 = level1.Validate();

            Assert.False(resultLevel1.IsValid);
        }

        private static Categoria GetCategoriaValida()
        {
            return CategoriaFactory.GetValid().Build();
        }
    }
}
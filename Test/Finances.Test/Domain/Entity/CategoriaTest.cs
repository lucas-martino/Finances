using System.Linq;
using Finances.Domain.Entity;
using Xunit;

namespace Test.Finances.Domain.Entity
{
    public class CategoriaTest
    {
        [Fact]
        public void UsuarioObrigatorio()
        {
            //Given
            Categoria categoria = new Categoria();
            categoria.Cor = "Cor";
            categoria.Nome = "Nome";

            //When
            var result = categoria.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Usuario"));
        }

        [Fact]
        public void NomeObrigatorio()
        {
            //Given
            Categoria categoria = new Categoria();
            categoria.Usuario = GetUsuarioValido();
            categoria.Cor = "Cor";

            //When
            var result = categoria.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
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
            Categoria categoria = new Categoria();
            categoria.Usuario = GetUsuarioValido();
            categoria.Nome = "Nome";
            categoria.Cor = "";

            //When
            var result = categoria.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Cor"));
        }

        private static Usuario GetUsuarioValido()
        {
            return new Usuario() { ID = 1, Nome = "Nome", Login = "Login", Senha = "Senha" };
        }
    }
}
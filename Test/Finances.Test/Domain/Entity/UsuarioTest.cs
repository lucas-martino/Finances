using System.Linq;
using Finances.Domain.Entity;
using Test.Finances.Factory;
using Xunit;

namespace Test.Finances.Domain.Entity
{
    public class UsuarioTest
    {
        [Fact]
        public void LoginDeveSerSempreMinusculo()
        {
            //Given
            Usuario usuario = new Usuario();

            //When
            usuario.Login = "LOGIN";

            //Then
            Assert.Equal("login", usuario.Login);
        }

        [Fact]
        public void LoginObrigatorio()
        {
            //Given
            Usuario usuario = GetUsuarioValido();
            usuario.Login = "";

            //When
            var result = usuario.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Login"));
        }

        [Fact]
        public void NomeObrigatorio()
        {
            //Given
            Usuario usuario = GetUsuarioValido();
            usuario.Nome = "";

            //When
            var result = usuario.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Nome"));
        }

        [Fact]
        public void SenhaObrigatorio()
        {
            //Given
            Usuario usuario = GetUsuarioValido();
            usuario.Senha = "";

            //When
            var result = usuario.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Senha"));
        }

        private static Usuario GetUsuarioValido()
        {
            return UsuarioFactory.GetValid().Build();
        }
    }
}
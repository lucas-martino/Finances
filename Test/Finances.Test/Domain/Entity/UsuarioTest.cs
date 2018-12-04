using System.Linq;
using Finances.Domain.Entity;
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
            Usuario usuario = new Usuario();
            usuario.Nome = "Nome";
            usuario.Senha = "Senha";

            //When
            var result = usuario.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Login"));
        }

        [Fact]
        public void NomeObrigatorio()
        {
            //Given
            Usuario usuario = new Usuario();
            usuario.Login = "Login";
            usuario.Senha = "Senha";

            //When
            var result = usuario.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Nome"));
        }

        [Fact]
        public void SenhaObrigatorio()
        {
            //Given
            Usuario usuario = new Usuario();
            usuario.Login = "Login";
            usuario.Nome = "Nome";

            //When
            var result = usuario.Validate();

            //Then
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.NotNull(result.Errors.FirstOrDefault(i => i.PropertyName == "Senha"));
        }
    }
}
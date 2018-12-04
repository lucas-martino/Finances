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
    }
}
using System;
using Xunit;
using Finances.Service;
using Finances.Domain.Entity;
using Finances.Domain.Service;
using Moq;

namespace Test.Finances.Service
{
    public class UsuarioServiceTest
    {
        [Fact]
        public void UsuarioSenhaCorretoRetornaUsuario()
        {
            //Given
            string login = "user1";
            string senha = "senha1";
            var mockDomainService = new Mock<UsuarioDomainService>(null);
            mockDomainService.Setup(ur => ur.GetUsuario(login, senha)).Returns(new Usuario() { Login = login });
            UsuarioService service = new UsuarioService(mockDomainService.Object);

            //When
            Usuario usuario = service.GetUsuario(login, senha);

            //Then
            Assert.NotNull(usuario);
            Assert.Equal(login, usuario.Login);
        }
    }
}

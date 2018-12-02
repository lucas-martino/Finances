using System;
using Finances.Domain.Entity;
using Finances.Domain.Exception;
using Finances.Domain.Repository;
using Finances.Domain.Service;
using Moq;
using Xunit;

namespace Test.Finances.Domain.Service
{
    public class UsuarioDomainServiceTest
    {
        [Fact]
        public void UsuarioSenhaCorretoRetornaUsuario()
        {
            //Given
            string login = "user1";
            string senha = "senha1";
            var mockRepository = new Mock<IUsuarioRepository>();
            mockRepository.Setup(ur => ur.GetUsuarioByLoginSenha(login, senha)).Returns(new Usuario());
            UsuarioDomainService service = new UsuarioDomainService(mockRepository.Object);

            //When
            Usuario usuario = service.GetUsuario(login, senha);

            //Then
            Assert.NotNull(usuario);
        }

        [Fact]
        public void UsuarioSenhaIncorretoRetornaExcessao()
        {
            //Given
            string login = "user2";
            string senha = "senhaErrada";
            var mockRepository = new Mock<IUsuarioRepository>();
            mockRepository.Setup(ur => ur.GetUsuarioByLoginSenha(login, senha)).Returns((Usuario)null);
            UsuarioDomainService service = new UsuarioDomainService(mockRepository.Object);

            //When
            Exception ex = Assert.Throws<UsuarioOuSenhaInvalidoException>(() => service.GetUsuario(login, senha));

            //Then
            Assert.NotNull(ex);
            Assert.Equal("Usuário ou Senha inválido.", ex.Message);
        }
    }
}
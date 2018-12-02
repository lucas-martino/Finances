using System;
using Xunit;
using Finances.Service;
using Finances.Domain.Entity;
using Finances.Domain.Repository;
using Moq;
using System.Collections.Generic;
using Finances.Domain.Service;

namespace Test.Finances.Service
{
    public class VigenciaServiceTest
    {
        [Fact]
        public void BuscarTodasVigenciasDeUmUsuario()
        {
            //Given
            Usuario usuario = new Usuario() { ID = 1 };
            var mockRepository = new Mock<IVigenciaRepository>();
            mockRepository.Setup(ur => ur.GetVigenciasPorUsuario(usuario)).Returns(new List<Vigencia>());
            var mockUsuarioService = new Mock<UsuarioService>();
            mockUsuarioService.Setup(us => us.GetUsuario(usuario.ID)).Returns(usuario);
            VigenciaService service = new VigenciaService(mockRepository.Object, null, mockUsuarioService.Object);

            //When
            IEnumerable<Vigencia> lista = service.GetVigenciasPorUsuario(usuario.ID);

            //Then
            Assert.NotNull(lista);
        }

        [Fact]
        public void BuscarVigenciaAtualDoUsuario()
        {
            //Given
            Usuario usuario = new Usuario() { ID = 1 };
            var mockDomainService = new Mock<VigenciaDomainService>(null, null);
            mockDomainService.Setup(ur => ur.GetVigenciaAtualPorUsuario(usuario)).Returns(new Vigencia());
            var mockUsuarioService = new Mock<UsuarioService>();
            mockUsuarioService.Setup(us => us.GetUsuario(usuario.ID)).Returns(usuario);
            VigenciaService service = new VigenciaService(null, mockDomainService.Object, mockUsuarioService.Object);

            //When
            Vigencia vigencia = service.GetVigenciaAtualPorUsuario(usuario.ID);

            //Then
            Assert.NotNull(vigencia);
        }
    }
}

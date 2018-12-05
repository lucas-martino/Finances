using System;
using Xunit;
using Finances.Service;
using Finances.Domain.Entity;
using Finances.Domain.Repository;
using Moq;
using System.Collections.Generic;
using Test.Finances.Factory;
using Finances.Domain.Service;

namespace Test.Finances.Service
{
    public class CategoriaServiceTest
    {
        [Fact]
        public void BuscarCategoriaPorUsuario()
        {
            //Given
            Usuario usuario = UsuarioFactory.GetValid().Build();
            var mockRepository = new Mock<ICategoriaRepository>();
            var mockUsuarioService = new Mock<UsuarioService>(null);
            mockUsuarioService.Setup(us => us.GetUsuario(usuario.ID)).Returns(usuario);
            mockRepository.Setup(r => r.GetCategoriaPorUsuario(usuario)).Returns(new List<Categoria>());
            CategoriaService service = new CategoriaService(mockRepository.Object, null, mockUsuarioService.Object);

            //When
            var categorias = service.GetCategoriasPorUsuario(usuario.ID);

            //Then
            Assert.NotNull(categorias);
        }

        [Fact]
        public void BuscarCategoriaPorID()
        {
            //Given
            int id = 1;
            var mockRepository = new Mock<ICategoriaRepository>();
            mockRepository.Setup(r => r.GetByID(id)).Returns(CategoriaFactory.Get().WithID(id).Build());
            CategoriaService service = new CategoriaService(mockRepository.Object, null, null);

            //When
            var categoria = service.GetCategoriaPorID(id);

            //Then
            Assert.NotNull(categoria);
        }

        [Fact]
        public void SalvarCategoria()
        {
            //Given
            Categoria categoria = CategoriaFactory.GetValid().Build();
            bool salvouObjeto = false;
            var categoriaDomainService = new Mock<CategoriaDomainService>(null, null, null);
            categoriaDomainService.Setup(r => r.SaveCategoria(categoria)).Returns(categoria.ID).Callback(() => salvouObjeto = true);
            CategoriaService service = new CategoriaService(null, categoriaDomainService.Object, null);

            //When
            var id = service.SalvarCategoria(categoria);

            //Then
            Assert.Equal(id, categoria.ID);
            Assert.True(salvouObjeto);
        }

        [Fact]
        public void ApagarCategoria()
        {
            //Given
            int id = 1;
            bool deletou = false;
            var categoriaDomainService = new Mock<CategoriaDomainService>(null, null, null);
            categoriaDomainService.Setup(r => r.DeleteCategoria(id)).Callback(() => deletou = true);
            CategoriaService service = new CategoriaService(null, categoriaDomainService.Object, null);

            //When
            service.ApagarCategoria(id);

            //Then
            Assert.True(deletou);
        }
    }
}

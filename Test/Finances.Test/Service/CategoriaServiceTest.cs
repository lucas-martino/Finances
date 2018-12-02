using System;
using Xunit;
using Finances.Service;
using Finances.Domain.Entity;
using Finances.Domain.Repository;
using Moq;
using System.Collections.Generic;

namespace Test.Finances.Service
{
    public class CategoriaServiceTest
    {
        [Fact]
        public void BuscarCategoriaPorUsuario()
        {
            //Given
            Usuario usuario = new Usuario() { ID = 1 };
            var mockRepository = new Mock<ICategoriaRepository>();
            var mockUsuarioService = new Mock<UsuarioService>();
            mockUsuarioService.Setup(us => us.GetUsuario(usuario.ID)).Returns(usuario);
            mockRepository.Setup(r => r.GetCategoriaPorUsuario(usuario)).Returns(new List<Categoria>());
            CategoriaService service = new CategoriaService(mockRepository.Object, mockUsuarioService.Object);

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
            mockRepository.Setup(r => r.GetByID(id)).Returns(new Categoria());
            CategoriaService service = new CategoriaService(mockRepository.Object, null);

            //When
            var categoria = service.GetCategoriaPorID(id);

            //Then
            Assert.NotNull(categoria);
        }

        [Fact]
        public void SalvarCategoria()
        {
            //Given
            Categoria categoria = new Categoria() { ID = 1 };
            bool salvouObjeto = false;
            var mockRepository = new Mock<ICategoriaRepository>();
            mockRepository.Setup(r => r.Save(categoria)).Returns(categoria.ID).Callback(() => salvouObjeto = true);
            CategoriaService service = new CategoriaService(mockRepository.Object, null);

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
            var mockRepository = new Mock<ICategoriaRepository>();
            mockRepository.Setup(r => r.Delete(id)).Callback(() => deletou = true);
            CategoriaService service = new CategoriaService(mockRepository.Object, null);

            //When
            service.ApagarCategoria(id);

            //Then
            Assert.True(deletou);
        }
    }
}

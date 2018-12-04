using System;
using Xunit;
using Finances.Service;
using Finances.Domain.Entity;
using Finances.Domain.Repository;
using Moq;
using System.Collections.Generic;

namespace Test.Finances.Service
{
    public class GastoServiceTest
    {
        [Fact]
        public void BuscarGastosVigenciaAtual()
        {
            //Given
            Usuario usuario = new Usuario() { ID = 1 };
            var gastoRepository = new Mock<IGastoRepository>();
            var vigenciaService = new Mock<VigenciaService>(null, null, null);
            vigenciaService.Setup(vs => vs.GetVigenciaAtualPorUsuario(usuario.ID)).Returns(new Vigencia() { Usuario = usuario });
            gastoRepository.Setup(or => or.GetGastosPorVigencia(It.Is<Vigencia>(v => v.Usuario == usuario))).Returns(new List<Gasto>());
            GastoService service = new GastoService(gastoRepository.Object, vigenciaService.Object, null);

            //When
            var gastos = service.GetGastosVigenciaAtual(usuario.ID);

            //Then
            Assert.NotNull(gastos);
        }

        [Fact]
        public void BuscarGastosPorCategoriaVigenciaAtual()
        {
            //Given
            int categoriaID = 1;
            Usuario usuario = new Usuario() { ID = 1};
            var gastoRepository = new Mock<IGastoRepository>();
            var vigenciaService = new Mock<VigenciaService>(null, null, null);
            vigenciaService.Setup(vs => vs.GetVigenciaAtualPorUsuario(usuario.ID)).Returns(new Vigencia() { Usuario = usuario });
            gastoRepository.Setup(or => or.GetGastosPorCategoriaEVigencia(categoriaID, It.Is<Vigencia>(v => v.Usuario == usuario))).Returns(new List<Gasto>());
            GastoService service = new GastoService(gastoRepository.Object, vigenciaService.Object, null);

            //When
            var gastos = service.GetGastosPorCategoriaVigenciaAtual(categoriaID, usuario.ID);

            //Then
            Assert.NotNull(gastos);
        }

        [Fact]
        public void BuscarGastosPorID()
        {
            //Given
            int id = 1;
            var gastoRepository = new Mock<IGastoRepository>();
            gastoRepository.Setup(or => or.GetByID(id)).Returns(new Gasto());
            GastoService service = new GastoService(gastoRepository.Object, null, null);

            //When
            var gasto = service.GetGastosPorID(id);

            //Then
            Assert.NotNull(gasto);
        }

        [Fact]
        public void SalvarGasto()
        {
            //Given
            Gasto gasto = new Gasto() { ID = 1 };
            bool salvou = false;
            var gastoRepository = new Mock<IGastoRepository>();
            gastoRepository.Setup(or => or.Save(gasto)).Returns(gasto.ID).Callback(() => salvou = true);
            GastoService service = new GastoService(gastoRepository.Object, null, null);

            //When
            var id = service.SalvarGasto(gasto);

            //Then
            Assert.Equal(id, gasto.ID);
            Assert.True(salvou);
        }

        [Fact]
        public void ApagarGasto()
        {
            //Given
            int id = 1;
            bool apagou = false;
            var gastoRepository = new Mock<IGastoRepository>();
            gastoRepository.Setup(or => or.Delete(id)).Callback(() => apagou = true);
            GastoService service = new GastoService(gastoRepository.Object, null, null);

            //When
            service.ApagarGasto(id);

            //Then
            Assert.True(apagou);
        }
    }
}

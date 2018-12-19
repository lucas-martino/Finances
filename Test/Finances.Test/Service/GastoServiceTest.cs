using System;
using Xunit;
using Finances.Service;
using Finances.Domain.Entity;
using Finances.Domain.Repository;
using Moq;
using System.Collections.Generic;
using Test.Finances.Factory;

namespace Test.Finances.Service
{
    public class GastoServiceTest
    {
        [Fact]
        public void BuscarGastosVigenciaPorVigencia()
        {
            //Given
            Vigencia vigencia = VigenciaFactory.GetValid().Build();
            var gastoRepository = new Mock<IGastoRepository>();
            gastoRepository.Setup(or => or.GetGastosPorVigencia(vigencia)).Returns(new List<Gasto>());
            GastoService service = new GastoService(gastoRepository.Object);

            //When
            var gastos = service.GetGastosPorVigencia(vigencia);

            //Then
            Assert.NotNull(gastos);
        }

        [Fact]
        public void BuscarGastosPorCategoriaPorVigencia()
        {
            //Given
            int categoriaID = 1;
            Vigencia vigencia = VigenciaFactory.GetValid().Build();
            var gastoRepository = new Mock<IGastoRepository>();
            gastoRepository.Setup(or => or.GetGastosPorCategoriaEVigencia(categoriaID, vigencia)).Returns(new List<Gasto>());
            GastoService service = new GastoService(gastoRepository.Object);

            //When
            var gastos = service.GetGastosPorCategoriaEVigencia(categoriaID, vigencia);

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
            GastoService service = new GastoService(gastoRepository.Object);

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
            GastoService service = new GastoService(gastoRepository.Object);

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
            GastoService service = new GastoService(gastoRepository.Object);

            //When
            service.ApagarGasto(id);

            //Then
            Assert.True(apagou);
        }
    }
}

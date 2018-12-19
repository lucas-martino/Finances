using System;
using Xunit;
using Finances.Service;
using Finances.Domain.Entity;
using Finances.Domain.Service;
using Moq;
using Test.Finances.Factory;

namespace Test.Finances.Service
{
    public class DemonstrativoServiceTest
    {
        [Fact]
        public void BuscarDemonstrativoParcialPorVigencia()
        {
            //Given
            var demonstrativoDomainService = new Mock<DemonstrativoDomainService>(null, null, null);
            Vigencia vigencia = VigenciaFactory.GetValid().Build();
            demonstrativoDomainService.Setup(ds => ds.GenereteDemonstrativoParcial(vigencia))
                .Returns(new DemonstrativoParcial());
            DemonstrativoService service = new DemonstrativoService(demonstrativoDomainService.Object);

            //When
            DemonstrativoParcial demonstrativoParcial = service.GetDemonstrativoParcialPorVigencia(vigencia);

            //Then
            Assert.NotNull(demonstrativoParcial);
        }

        [Fact]
        public void BuscarDemonstrativoPorVigencia()
        {
            //Given
            var demonstrativoDomainService = new Mock<DemonstrativoDomainService>(null, null, null);
            Vigencia vigencia = VigenciaFactory.GetValid().Build();
            demonstrativoDomainService.Setup(ds => ds.GenereteDemonstrativo(vigencia))
                .Returns(new Demonstrativo());
            DemonstrativoService service = new DemonstrativoService(demonstrativoDomainService.Object);

            //When
            Demonstrativo demonstrativo = service.GetDemonstrativoPorVigencia(vigencia);

            //Then
            Assert.NotNull(demonstrativo);
        }
    }
}

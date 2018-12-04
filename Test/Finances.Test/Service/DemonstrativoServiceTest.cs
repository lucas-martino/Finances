using System;
using Xunit;
using Finances.Service;
using Finances.Domain.Entity;
using Finances.Domain.Service;
using Moq;

namespace Test.Finances.Service
{
    public class DemonstrativoServiceTest
    {
        [Fact]
        public void BuscarDemonstrativoParcialVigenciaAtual()
        {
            //Given
            var demonstrativoDomainService = new Mock<DemonstrativoDomainService>(null, null);
            var vigenciaService = new Mock<VigenciaService>(null, null, null);
            Usuario usuario = new Usuario() { ID = 1 };
            vigenciaService.Setup(vs => vs.GetVigenciaAtualPorUsuario(usuario.ID))
                .Returns(new Vigencia() { Usuario = usuario });
            demonstrativoDomainService.Setup(ds => ds.GenereteDemonstrativoParcial(It.Is<Vigencia>(v => v.Usuario == usuario)))
                .Returns(new DemonstrativoParcial());
            DemonstrativoService service = new DemonstrativoService(demonstrativoDomainService.Object, vigenciaService.Object);

            //When
            DemonstrativoParcial demonstrativoParcial = service.GetDemonstrativoParcialVigenciaAtual(usuario.ID);

            //Then
            Assert.NotNull(demonstrativoParcial);
        }
    }
}

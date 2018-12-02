using System;
using Xunit;
using Finances.Service;
using Finances.Domain.Entity;
using Finances.Domain.Repository;
using Moq;

namespace Test.Finances.Service
{
    public class OrcamentoServiceTest
    {
        [Fact]
        public void BuscarOrcamentoPorVigencia()
        {
            //Given
            var orcamentoRepository = new Mock<IOrcamentoRepository>();
            Vigencia vigencia = new Vigencia();
            orcamentoRepository.Setup(or => or.GetOrcamentoPorVigencia(vigencia)).Returns(new Orcamento());
            OrcamentoService service = new OrcamentoService(orcamentoRepository.Object, null, null);

            //When
            Orcamento orcamento = service.GetOrcamentoPorVigencia(vigencia);

            //Then
            Assert.NotNull(orcamento);
        }

        [Fact]
        public void BuscarOrcamentoPorID()
        {
            //Given
            var orcamentoRepository = new Mock<IOrcamentoRepository>();
            int id = 1;
            orcamentoRepository.Setup(or => or.GetByID(id)).Returns(new Orcamento());
            OrcamentoService service = new OrcamentoService(orcamentoRepository.Object, null, null);

            //When
            Orcamento orcamento = service.GetOrcamentoPorID(id);

            //Then
            Assert.NotNull(orcamento);
        }

        [Fact]
        public void BuscarOrcamentoVigenciaAtual()
        {
            //Given
            var orcamentoRepository = new Mock<IOrcamentoRepository>();
            var vigenciaService = new Mock<VigenciaService>(null, null);
            Usuario usuario = new Usuario() { ID = 1 };
            vigenciaService.Setup(vs => vs.GetVigenciaAtualPorUsuario(usuario.ID)).Returns(new Vigencia() { Usuario = usuario });
            orcamentoRepository.Setup(or => or.GetOrcamentoPorVigencia(It.Is<Vigencia>(v => v.Usuario == usuario))).Returns(new Orcamento());
            OrcamentoService service = new OrcamentoService(orcamentoRepository.Object, vigenciaService.Object, null);

            //When
            Orcamento orcamento = service.GetOrcamentoVigenciaAtual(usuario.ID);

            //Then
            Assert.NotNull(orcamento);
        }

        [Fact]
        public void SalvarOrcamento()
        {
            //Given
            bool salvou = false;
            var orcamentoRepository = new Mock<IOrcamentoRepository>();
            Orcamento orcamento = new Orcamento() { ID = 1 };
            orcamentoRepository.Setup(or => or.Save(orcamento)).Returns(orcamento.ID).Callback(() => salvou = true);
            OrcamentoService service = new OrcamentoService(orcamentoRepository.Object, null, null);

            //When
            long id = service.SalvarOrcamento(orcamento);

            //Then
            Assert.True(salvou);
            Assert.Equal(id, orcamento.ID);
        }

        [Fact]
        public void BuscarOrcamentoCategoriaPorID()
        {
            //Given
            int id = 1;
            var orcamentoRepository = new Mock<IOrcamentoRepository>();
            orcamentoRepository.Setup(or => or.GetOrcamentoCategoriaByID(id)).Returns(new OrcamentoCategoria());
            OrcamentoService service = new OrcamentoService(orcamentoRepository.Object, null, null);

            //When
            OrcamentoCategoria orcamento = service.GetOrcamentoCategoriaPorID(id);

            //Then
            Assert.NotNull(orcamento);
        }

        [Fact]
        public void ApagarOrcamentoCategoria()
        {
            //Given
            int id = 1;
            bool apagou = false;
            var orcamentoRepository = new Mock<IOrcamentoRepository>();
            orcamentoRepository.Setup(or => or.DeleteOrcamentoCategoria(id)).Callback(() => apagou = true);
            OrcamentoService service = new OrcamentoService(orcamentoRepository.Object, null, null);

            //When
            service.ApagarOrcamentoCategoria(id);

            //Then
            Assert.True(apagou);
        }
    }
}

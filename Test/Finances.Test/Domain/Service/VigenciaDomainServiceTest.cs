using System;
using Finances.Domain.Entity;
using Finances.Domain.Repository;
using Finances.Domain.Service;
using Moq;
using Xunit;

namespace Test.Finances.Domain.Service
{
    public class VigenciaDomainServiceTest
    {
    
        [Fact]
        public void BuscarVigenciaAtualDoUsuarioQuandoPossui()
        {
            //Given
            Usuario usuario = new Usuario();
            int referencia = GetReferencia(DateTime.Today.Year, DateTime.Today.Month);
            var vigenciaRepository = new Mock<IVigenciaRepository>();
            vigenciaRepository.Setup(vr => vr.GetVigencia(usuario, referencia)).Returns(new Vigencia());
            VigenciaDomainService domainService = new VigenciaDomainService(vigenciaRepository.Object, null);

            //When
            Vigencia vigencia = domainService.GetVigenciaAtualPorUsuario(usuario);

            //Then
            Assert.NotNull(vigencia);
        }

        [Fact]
        public void BuscarVigenciaAtualQuandoNaoExistenteNenhuma()
        {
            //Given
            Usuario usuario = new Usuario();
            int referencia = GetReferencia(DateTime.Today.Year, DateTime.Today.Month);
            bool salvouVigenciaCorreta = false;
            bool criouOrcamento = false;

            var vigenciaRepository = new Mock<IVigenciaRepository>();
            vigenciaRepository.Setup(vr => vr.GetVigencia(usuario, referencia))
                .Returns((Vigencia)null);            
            vigenciaRepository.Setup(vr =>  vr.Save(It.Is<Vigencia>(v => v.Usuario == usuario && v.Referencia == referencia)))
                .Callback(() => salvouVigenciaCorreta = true);
            vigenciaRepository.Setup(vr => vr.GetVigenciaAnterior(It.Is<Vigencia>(v => v.Usuario == usuario && v.Referencia == referencia)))
                .Returns((Vigencia)null);  

            var orcamentoRepository = new Mock<IOrcamentoRepository>();
            orcamentoRepository.Setup(or => or.Save(It.Is<Orcamento>(o =>  o.Vigencia.Usuario == usuario && o.Vigencia.Referencia == referencia)))
                .Callback(() => criouOrcamento = true);
            
            VigenciaDomainService domainService = new VigenciaDomainService(vigenciaRepository.Object, orcamentoRepository.Object);

            //When
            Vigencia vigencia = domainService.GetVigenciaAtualPorUsuario(usuario);

            //Then
            Assert.NotNull(vigencia);
            Assert.True(salvouVigenciaCorreta);
            Assert.True(criouOrcamento);
        }

        [Fact]
        public void BuscarVigenciaAtualQuandoNaoExistenteEExisteUmaAnterior()
        {
            //Given
            Usuario usuario = new Usuario();
            int referencia = GetReferencia(DateTime.Today.Year, DateTime.Today.Month);
            int referenciaAnterior = GetReferencia(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month);
            bool salvouVigenciaCorreta = false;
            bool criouOrcamentoCorreto = false;

            Vigencia vigenciaAnterior = new Vigencia() { Usuario = usuario, Referencia = referenciaAnterior };
            decimal valorOrcamentoAnterior = 1000;
            var vigenciaRepository = new Mock<IVigenciaRepository>();
            vigenciaRepository.Setup(vr => vr.GetVigencia(usuario, referencia))
                .Returns((Vigencia)null);            
            vigenciaRepository.Setup(vr =>  vr.Save(It.Is<Vigencia>(v => v.Usuario == usuario && v.Referencia == referencia)))
                .Callback(() => salvouVigenciaCorreta = true);
            vigenciaRepository.Setup(vr => vr.GetVigenciaAnterior(It.Is<Vigencia>(v => v.Usuario == usuario && v.Referencia == referencia)))
                .Returns(vigenciaAnterior);  

            var orcamentoRepository = new Mock<IOrcamentoRepository>();
            orcamentoRepository.Setup(or => or.GetOrcamentoPorVigencia(vigenciaAnterior))
                .Returns(new Orcamento() { Valor = valorOrcamentoAnterior });
            orcamentoRepository.Setup(or => or.Save(It.Is<Orcamento>(o => o.Valor == valorOrcamentoAnterior && o.Vigencia.Usuario == usuario && o.Vigencia.Referencia == referencia)))
                .Callback(() => criouOrcamentoCorreto = true);
            
            VigenciaDomainService domainService = new VigenciaDomainService(vigenciaRepository.Object, orcamentoRepository.Object);

            //When
            Vigencia vigencia = domainService.GetVigenciaAtualPorUsuario(usuario);

            //Then
            Assert.NotNull(vigencia);
            Assert.True(salvouVigenciaCorreta);
            Assert.True(criouOrcamentoCorreto);
        }

        private int GetReferencia(int ano, int mes)
        {
            return ano * 100 + mes;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Finances.Domain.Entity;
using Finances.Domain.Repository;
using Finances.Domain.Service;
using Moq;
using Xunit;

namespace Test.Finances.Domain.Service
{
    public class DemonstrativoDomainServiceTest
    {
        [Theory]
        [InlineData(1000, 0, "Black")] //Sem orcamento Cadastrado
        [InlineData(1000, 5000, "Green")] // Orcamento positivo
        [InlineData(1000, 1000, "Green")] // Orcamento positivo com valor igual ao orcamento
        [InlineData(1000, 999, "Red")] // Orcamento negativo
        public void DemonstrativoParcial(decimal valorGastoTotal, decimal valorOrcamento, string corEsperada)
        {
            //Given
            Vigencia vigencia = new Vigencia();
            DemonstrativoDomainService service = GenereteDepedencia(vigencia, valorGastoTotal, valorOrcamento);

            //When
            DemonstrativoParcial demonstrativo = service.GenereteDemonstrativoParcial(vigencia);

            //Then
            Assert.NotNull(demonstrativo);
            Assert.Equal(valorGastoTotal, demonstrativo.ValorGastoTotal);
            Assert.Equal(corEsperada, demonstrativo.Cor);
        }

        [Theory]
        [InlineData(1000, 0, "Black", 50, 100, "Green")] //Sem orcamento Cadastrado e orcamento categoria positivo    
        [InlineData(1000, 5000, "Green", 50, 100, "Green")] //Orcamento positivo e orcamento categoria positivo
        [InlineData(1000, 999, "Red", 50, 100, "Green")] //Orcamento negativo e orcamento categoria positivo
        [InlineData(1000, 0, "Black", 100, 99, "Red")] //Sem orcamento Cadastrado e orcamento categoria negativo    
        [InlineData(1000, 5000, "Green", 100, 99, "Red")] //Orcamento positivo e orcamento categoria negativo
        [InlineData(1000, 999, "Red", 100, 99, "Red")] //Orcamento negativo e orcamento categoria negativo
        public void DemonstrativoParcialComOrcamentoCategoria(decimal valorGastoTotal, decimal valorOrcamento, string corEsperadaOrcamento,
            decimal valorGastoCategoria, decimal valorOrcamentoCategoria, string corEsperadaOrcamentoCategoria)
        {
            //Given
            Vigencia vigencia = new Vigencia();
            Categoria categoria = new Categoria() { ID = 1 };
            DemonstrativoDomainService service = GenereteDepedenciaComOrcamentoCategoria(vigencia, valorGastoTotal, valorOrcamento, 
                categoria, valorGastoCategoria, valorOrcamentoCategoria);

            //When
            DemonstrativoParcial demonstrativo = service.GenereteDemonstrativoParcial(vigencia);            

            //Then
            Assert.NotNull(demonstrativo);
            Assert.Equal(valorGastoTotal, demonstrativo.ValorGastoTotal);
            Assert.Equal(corEsperadaOrcamento, demonstrativo.Cor);
            DemonstrativoItem item = demonstrativo.Orcamentos.FirstOrDefault();
            Assert.Equal(categoria.ID, item.Categoria.ID);
            Assert.Equal(valorGastoCategoria, item.ValorGasto);
            Assert.Equal(valorOrcamentoCategoria - valorGastoCategoria, item.OrcamentoRestante);
            Assert.Equal(corEsperadaOrcamentoCategoria, item.Cor); 
        }

        [Fact]
        public void DemonstrativoParcialComGastoNaoCategorizado()
        {
            //Given
            Vigencia vigencia = new Vigencia();
            decimal valorGastoTotal = 1000;
            decimal valorOrcamento = 5000;
            decimal valorNaoCategorizado = 200;
            DemonstrativoDomainService service = GenereteDepedencia(vigencia, valorGastoTotal, valorOrcamento, valorNaoCategorizado);

            //When 
            DemonstrativoParcial demonstrativo = service.GenereteDemonstrativoParcial(vigencia); 

            //Then
            Assert.NotNull(demonstrativo);
            Assert.Equal(valorGastoTotal, demonstrativo.ValorGastoTotal);
            Assert.Equal("Green", demonstrativo.Cor);
            Assert.NotNull(demonstrativo.NaoCategorizado);
            Assert.Equal(valorNaoCategorizado, demonstrativo.NaoCategorizado.ValorGasto);
            Assert.Equal("Yellow", demonstrativo.NaoCategorizado.Cor);
        }

        private DemonstrativoDomainService GenereteDepedencia(Vigencia vigencia, decimal valorGastoTotal, decimal valorOrcamento)
        {            
            var gastoRepository = new Mock<IGastoRepository>();
            var orcamentoRepository = new Mock<IOrcamentoRepository>();
            
            Orcamento orcamento = new Orcamento() { Valor = valorOrcamento, Vigencia = vigencia };
            gastoRepository.Setup(r => r.GetGastoTotalPorVigencia(vigencia))
                .Returns(valorGastoTotal);
            orcamentoRepository.Setup(r => r.GetOrcamentoPorVigencia(vigencia))
                .Returns(orcamento);

            return new DemonstrativoDomainService(gastoRepository.Object, orcamentoRepository.Object, null);
        }

        private DemonstrativoDomainService GenereteDepedencia(Vigencia vigencia, decimal valorGastoTotal, decimal valorOrcamento, decimal valorNaoCategoizado)
        {            
            var gastoRepository = new Mock<IGastoRepository>();
            var orcamentoRepository = new Mock<IOrcamentoRepository>();
            
            Orcamento orcamento = new Orcamento() { Valor = valorOrcamento, Vigencia = vigencia };
            gastoRepository.Setup(r => r.GetGastoTotalPorVigencia(vigencia))            
                .Returns(valorGastoTotal);
            gastoRepository.Setup(r => r.GetGastoTotalVigenciaSemCategoria(vigencia))
                .Returns(valorNaoCategoizado);
            orcamentoRepository.Setup(r => r.GetOrcamentoPorVigencia(vigencia))
                .Returns(orcamento);

            return new DemonstrativoDomainService(gastoRepository.Object, orcamentoRepository.Object, null);
        }

        private DemonstrativoDomainService GenereteDepedenciaComOrcamentoCategoria(Vigencia vigencia, decimal valorGastoTotal, decimal valorOrcamento,
            Categoria categoria, decimal valorGastoCategoria, decimal valorOrcamentoCategoria)
        {   
            var gastoRepository = new Mock<IGastoRepository>();
            var orcamentoRepository = new Mock<IOrcamentoRepository>();            
            
            Orcamento orcamento = new Orcamento() { Valor = valorOrcamento, Vigencia = vigencia };
            OrcamentoCategoria orcamentoCategoria = new OrcamentoCategoria() { Categoria = categoria, Valor = valorOrcamentoCategoria };
            orcamento.AddOrcamentoCategoria(orcamentoCategoria);
            orcamentoRepository.Setup(r => r.GetOrcamentoPorVigencia(vigencia))
                .Returns(orcamento);
            gastoRepository.Setup(r => r.GetGastoTotalPorVigencia(vigencia))
                .Returns(valorGastoTotal);
            gastoRepository.Setup(r => r.GetGastoTotalPorCategoriaEVigencia(categoria, vigencia))
                .Returns(valorGastoCategoria);

            return new DemonstrativoDomainService(gastoRepository.Object, orcamentoRepository.Object, null);
        }
    }
}
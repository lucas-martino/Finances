using System;
using System.Collections.Generic;
using System.Linq;
using Finances.Domain.Entity;
using Finances.Service;
using Finances.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finances.WebApp.Controllers
{
    public class DemonstrativoController : FinancesController<DemonstrativoService>
    {
        public DemonstrativoController(DemonstrativoService service) 
            : base(service)
        {
        }

        public IActionResult Index()
        {
            var model = ConvertEntityToModel(Service.GetDemonstrativoVigenciaAtual(UsuarioLogadoID));
            return View(model);
        }

        public IActionResult Home()
        {
            var model = ConvertEntityToModel(Service.GetDemonstrativoParcialVigenciaAtual(UsuarioLogadoID));
            return View(model);
        }

        private static DemostrativoViewModel ConvertEntityToModel(Demonstrativo entidade)
        {
            DemostrativoViewModel model = ConvertEntityToModel((DemonstrativoParcial)entidade);
            model.DemostrativoCategoria = ConvertEntityToModel(entidade.Categorias);
            model.OrcamentoTotal = entidade.OrcamentoTotal;
            
            return model;
        }

        private static DemostrativoViewModel ConvertEntityToModel(DemonstrativoParcial entidade)
        {
            DemostrativoViewModel model = new DemostrativoViewModel();
            model.ValorGastoTotal = entidade.ValorGastoTotal;
            model.Cor = entidade.Cor;
            model.DemostrativoOrcamentosCategoria = ConvertEntityToModel(entidade.Orcamentos);
            model.NaoCategorizado = ConvertEntityToModel(entidade.NaoCategorizado);
            model.PossuiNaoCategorizado = entidade.NaoCategorizado != null;
            
            return model;
        }

        private static DemostrativoItemViewModel ConvertEntityToModel(DemonstrativoItemOrcamento entidade)
        {
            DemostrativoItemViewModel model = ConvertEntityToModel((DemonstrativoItemCategoria)entidade);
            model.Orcamento = entidade.OrcamentoRestante;

            return model;
        }

        private static DemostrativoItemViewModel ConvertEntityToModel(DemonstrativoItemCategoria entidade)
        {
            DemostrativoItemViewModel model = ConvertEntityToModel((DemonstrativoItem)entidade);
            model.Categoria = CategoriaController.ConvertEntityToModel(entidade.Categoria);

            return model;
        }

        private static DemostrativoItemViewModel ConvertEntityToModel(DemonstrativoItem entidade)
        {
            DemostrativoItemViewModel model = null;
            if (entidade != null)
            {
                model = new DemostrativoItemViewModel();
                model.Cor = entidade.Cor;
                model.ValorGasto = entidade.ValorGasto;
                model.Percentual = entidade.Percentual;
            }

            return model;
        }

        private static IEnumerable<DemostrativoItemViewModel> ConvertEntityToModel(IEnumerable<DemonstrativoItemCategoria> entidade)
        {
            IList<DemostrativoItemViewModel> lista = new List<DemostrativoItemViewModel>();
            foreach (var item in entidade)
                lista.Add(ConvertEntityToModel(item));
                
            return lista;
        }

        private static IEnumerable<DemostrativoItemViewModel> ConvertEntityToModel(IEnumerable<DemonstrativoItemOrcamento> entidade)
        {
            IList<DemostrativoItemViewModel> lista = new List<DemostrativoItemViewModel>();
            foreach (var item in entidade)
                lista.Add(ConvertEntityToModel(item));
                
            return lista;
        }
    }
}
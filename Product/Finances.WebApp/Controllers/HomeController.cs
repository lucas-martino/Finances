using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Finances.WebApp.Models;

namespace Finances.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            DemostrativoViewModel model = new DemostrativoViewModel();
            model.ValorTotal = GetGastoTotal();
            model.Itens = GetDemostrativosItens();

            return View(model);
        }

        private IEnumerable<DemostrativoItemViewModel> GetDemostrativosItens()
        {
            IList<DemostrativoItemViewModel> lista = new List<DemostrativoItemViewModel>();
            DemostrativoItemViewModel item;
            decimal valor;

            foreach (var orcamento in Banco.Get().Orcamentos)
            {
                item = new DemostrativoItemViewModel();
                item.Categoria = Banco.Get().Categorias.FirstOrDefault(i => i.ID == orcamento.CategoriaID).Nome;
                item.Orcamento = orcamento.Valor.ToString();
                item.Valor = Banco.Get().Lancamentos.Where(i => i.CategoriaID == orcamento.CategoriaID).Sum(i => i.Valor).ToString();

                lista.Add(item);
            }
            foreach (var categoria in Banco.Get().Categorias)
            {
                if (Banco.Get().Orcamentos.FirstOrDefault(i => i.CategoriaID == categoria.ID) == null)
                {
                    valor = Banco.Get().Lancamentos.Where(i => i.CategoriaID == categoria.ID).Sum(i => i.Valor);
                    if (valor > 0)
                    {
                        item = new DemostrativoItemViewModel();
                        item.Categoria = categoria.Nome;
                        item.Orcamento = "-";
                        item.Valor = valor.ToString();

                        lista.Add(item);
                    }
                }
            }
            valor = Banco.Get().Lancamentos.Where(i => i.CategoriaID <= 0).Sum(i => i.Valor);
            if (valor > 0)
            {
                    item = new DemostrativoItemViewModel();
                    item.Categoria = "Não categorizado";
                    item.Orcamento = "-";
                    item.Valor = valor.ToString();

                    lista.Add(item);
            }

            return lista;
        }

        private decimal GetGastoTotal()
        {
            return Banco.Get().Lancamentos.Sum(i => i.Valor);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

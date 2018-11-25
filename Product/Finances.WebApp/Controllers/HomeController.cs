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
            return RedirectToAction(nameof(Demonstrativo));
        }

        public IActionResult Demonstrativo()
        {
            DemostrativoViewModel model = new DemostrativoViewModel();
            model.ValorTotal = GetGastoTotal();
            model.Itens = GetDemostrativosItens(model.ValorTotal);
            model.Percentual = model.ValorTotal * 100 / Banco.Get().Orcamento.Valor;
            if (Banco.Get().Orcamento.Valor < model.ValorTotal)
                model.Cor = "Red";
            else
                model.Cor = "Green";

            return View(model);
        }

        private IEnumerable<DemostrativoItemViewModel> GetDemostrativosItens(decimal valorTotal)
        {
            IList<DemostrativoItemViewModel> lista = new List<DemostrativoItemViewModel>();
            DemostrativoItemViewModel item;
            decimal valor;

            foreach (var orcamento in Banco.Get().OrcamentosCategoria)
            {
                item = new DemostrativoItemViewModel();
                Categoria categoria = Banco.Get().Categorias.FirstOrDefault(i => i.ID == orcamento.CategoriaID);
                item.CategoriaID = orcamento.CategoriaID;
                item.Categoria = categoria.Nome;
                item.CategoriaCor = categoria.Cor;           
                item.Valor = Banco.Get().Lancamentos.Where(i => i.CategoriaID == orcamento.CategoriaID).Sum(i => i.Valor);
                item.Orcamento = String.Format("R${0}", (orcamento.Valor - item.Valor).ToString()); 
                item.Percentual = item.Valor * 100 / valorTotal;
                if (item.Valor > orcamento.Valor)
                    item.Cor = "Red";
                else
                    item.Cor = "Green";

                lista.Add(item);
            }
            foreach (var categoria in Banco.Get().Categorias)
            {
                if (Banco.Get().OrcamentosCategoria.FirstOrDefault(i => i.CategoriaID == categoria.ID) == null)
                {
                    valor = Banco.Get().Lancamentos.Where(i => i.CategoriaID == categoria.ID).Sum(i => i.Valor);
                    if (valor > 0)
                    {
                        item = new DemostrativoItemViewModel();
                        item.CategoriaID = categoria.ID;
                        item.Categoria = categoria.Nome;
                        item.CategoriaCor = categoria.Cor;
                        item.Orcamento = "-";
                        item.Valor = valor;
                        item.Cor = "Black";
                        item.Percentual = item.Valor * 100 / valorTotal;

                        lista.Add(item);
                    }
                }
            }
            valor = Banco.Get().Lancamentos.Where(i => i.CategoriaID <= 0).Sum(i => i.Valor);
            if (valor > 0)
            {
                    item = new DemostrativoItemViewModel();
                    item.CategoriaID = 0;
                    item.Categoria = "Não categorizado";
                    item.Categoria = "Yellow";
                    item.Orcamento = "-";
                    item.Valor = valor;
                    item.Cor = "Yellow";
                    item.Percentual = item.Valor * 100 / valorTotal;

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

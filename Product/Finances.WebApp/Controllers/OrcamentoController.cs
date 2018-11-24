using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Finances.WebApp.Models;

namespace Finances.WebApp.Controllers
{
    public class OrcamentoController : Controller
    {
        
        public IActionResult Index()
        {
            return View(ConvertEntityToModel(Banco.Get().Orcamentos));
        }        
        
        public IActionResult Create()
        {
            OrcamentoViewModel model = new OrcamentoViewModel();
            model.Categorias = CategoriaController.ConvertEntityToSelectListItem(Banco.Get().Categorias);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] OrcamentoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Orcamento entidade = new Orcamento();
                entidade.ID = Banco.Get().Orcamentos.Count() + 1;
                entidade.Valor = model.Valor;
                entidade.CategoriaID = model.CategoriaID;

                Banco.Get().Orcamentos.Add(entidade);
                Banco.Salvar();
                return RedirectToAction(nameof(Index));
            }

            model.Categorias = CategoriaController.ConvertEntityToSelectListItem(Banco.Get().Categorias);
            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Orcamento entidade = Banco.Get().Orcamentos.FirstOrDefault(i => i.ID == id.Value);
            if (entidade == null)
                return NotFound();

            OrcamentoViewModel model = ConvertEntityToModel(entidade);
            model.Categorias = CategoriaController.ConvertEntityToSelectListItem(Banco.Get().Categorias);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind] OrcamentoViewModel model)
        {
            if (id == null)
                return NotFound();

            Orcamento entidade = Banco.Get().Orcamentos.FirstOrDefault(i => i.ID == id.Value);
            if (entidade == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                entidade.Valor = model.Valor;
                entidade.CategoriaID = model.CategoriaID;
                Banco.Salvar();
                return RedirectToAction(nameof(Index));
            }

            model.Categorias = CategoriaController.ConvertEntityToSelectListItem(Banco.Get().Categorias);
            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            Orcamento entidade = Banco.Get().Orcamentos.FirstOrDefault(i => i.ID == id.Value);
            if (entidade == null)
                return NotFound();

            Banco.Get().Orcamentos.Remove(entidade);
            Banco.Salvar();

            return RedirectToAction(nameof(Index));
        }

        private static OrcamentoViewModel ConvertEntityToModel(Orcamento entidade)
        {
            OrcamentoViewModel model = new OrcamentoViewModel();
            model.ID = entidade.ID;
            model.Valor = entidade.Valor;
            model.CategoriaID = entidade.CategoriaID;
            model.Categoria = Banco.Get().Categorias.FirstOrDefault(i => i.ID == entidade.CategoriaID).Nome;

            return model;
        }

        private static IEnumerable<OrcamentoViewModel> ConvertEntityToModel(IEnumerable<Orcamento> entidade)
        {
            IList<OrcamentoViewModel> lista = new List<OrcamentoViewModel>();
            foreach (Orcamento orcamento in entidade)
                lista.Add(ConvertEntityToModel(orcamento));
                
            return lista;
        }
    }
}

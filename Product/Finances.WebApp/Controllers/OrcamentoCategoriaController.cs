using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Finances.WebApp.Models;

namespace Finances.WebApp.Controllers
{
    public class OrcamentoCategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View(ConvertEntityToModel(Banco.Get().OrcamentosCategoria));
        }        
        
        public IActionResult Create()
        {
            OrcamentoCategoriaViewModel model = new OrcamentoCategoriaViewModel();
            model.Categorias = CategoriaController.ConvertEntityToSelectListItem(Banco.Get().Categorias);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] OrcamentoCategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                OrcamentoCategoria entidade = new OrcamentoCategoria();
                entidade.ID = Banco.Get().OrcamentosCategoria.Count() + 1;
                entidade.Valor = model.Valor;
                entidade.CategoriaID = model.CategoriaID;

                Banco.Get().OrcamentosCategoria.Add(entidade);
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

            OrcamentoCategoria entidade = Banco.Get().OrcamentosCategoria.FirstOrDefault(i => i.ID == id.Value);
            if (entidade == null)
                return NotFound();

            OrcamentoCategoriaViewModel model = ConvertEntityToModel(entidade);
            model.Categorias = CategoriaController.ConvertEntityToSelectListItem(Banco.Get().Categorias);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind] OrcamentoCategoriaViewModel model)
        {
            if (id == null)
                return NotFound();

            OrcamentoCategoria entidade = Banco.Get().OrcamentosCategoria.FirstOrDefault(i => i.ID == id.Value);
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

            OrcamentoCategoria entidade = Banco.Get().OrcamentosCategoria.FirstOrDefault(i => i.ID == id.Value);
            if (entidade == null)
                return NotFound();

            Banco.Get().OrcamentosCategoria.Remove(entidade);
            Banco.Salvar();

            return RedirectToAction(nameof(Index));
        }

        private static OrcamentoCategoriaViewModel ConvertEntityToModel(OrcamentoCategoria entidade)
        {
            OrcamentoCategoriaViewModel model = new OrcamentoCategoriaViewModel();
            Categoria categoria = Banco.Get().Categorias.FirstOrDefault(i => i.ID == entidade.CategoriaID);
            model.ID = entidade.ID;
            model.Valor = entidade.Valor;
            model.CategoriaID = entidade.CategoriaID;
            model.Categoria = categoria.Nome;
            model.CategoriaCor = categoria.Cor;

            return model;
        }

        private static IEnumerable<OrcamentoCategoriaViewModel> ConvertEntityToModel(IEnumerable<OrcamentoCategoria> entidade)
        {
            IList<OrcamentoCategoriaViewModel> lista = new List<OrcamentoCategoriaViewModel>();
            foreach (var orcamento in entidade)
                lista.Add(ConvertEntityToModel(orcamento));
                
            return lista;
        }
    }
}

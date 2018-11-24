using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Finances.WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Finances.WebApp.Controllers
{
    public class CategoriaController : Controller
    {        
        public IActionResult Index()
        {
            return View(ConvertEntityToModel(Banco.Get().Categorias.OrderBy(i => i.Nome)));
        }        
        
        public IActionResult Create()
        {
            return View(new CategoriaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] CategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = new Categoria();
                categoria.ID = Banco.Get().Categorias.Count() + 1;
                categoria.Nome = model.Nome;
                Banco.Get().Categorias.Add(categoria);
                Banco.Salvar();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Categoria entidade = Banco.Get().Categorias.FirstOrDefault(i => i.ID == id.Value);
            if (entidade == null)
                return NotFound();

            CategoriaViewModel model = new CategoriaViewModel();
            model.ID = entidade.ID;
            model.Nome = entidade.Nome;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind] CategoriaViewModel model)
        {
            if (id == null)
                return NotFound();

            Categoria entidade = Banco.Get().Categorias.FirstOrDefault(i => i.ID == id.Value);
            if (entidade == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                entidade.Nome = model.Nome;
                Banco.Salvar();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            Categoria entidade = Banco.Get().Categorias.FirstOrDefault(i => i.ID == id.Value);
            if (entidade == null)
                return NotFound();

            Banco.Get().Categorias.Remove(entidade);
            Banco.Salvar();

            return RedirectToAction(nameof(Index));
        }

        private static CategoriaViewModel ConvertEntityToModel(Categoria entidade)
        {
            CategoriaViewModel model = new CategoriaViewModel();
            model.ID = entidade.ID;
            model.Nome = entidade.Nome;

            return model;
        }

        private static SelectListItem ConvertEntityToSelectListItem(Categoria entidade)
        {
            SelectListItem model = new SelectListItem();
            model.Value = entidade.ID.ToString();
            model.Text = entidade.Nome;

            return model;
        }

        public static IEnumerable<CategoriaViewModel> ConvertEntityToModel(IEnumerable<Categoria> entidade)
        {
            IList<CategoriaViewModel> lista = new List<CategoriaViewModel>();
            foreach (Categoria categoria in entidade)
                lista.Add(ConvertEntityToModel(categoria));
                
            return lista;
        }

        public static IEnumerable<SelectListItem> ConvertEntityToSelectListItem(IEnumerable<Categoria> entidade)
        {
            IList<SelectListItem> lista = new List<SelectListItem>();
            foreach (Categoria categoria in entidade)
                lista.Add(ConvertEntityToSelectListItem(categoria));
                
            return lista;
        }
    }
}

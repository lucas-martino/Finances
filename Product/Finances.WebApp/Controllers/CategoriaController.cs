using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Finances.Service;
using Finances.Domain.Entity;
using Finances.WebApp.Models;

namespace Finances.WebApp.Controllers
{
    public class CategoriaController : FinancesController<CategoriaService>
    {        
        public CategoriaController(CategoriaService categoriaService)
            :base(categoriaService)
        {            
        }
        
        public IActionResult Index()
        {
            var model = ConvertEntityToModel(Service.GetCategoriasPorUsuario(UsuarioLogadoID));
            return View(model);
        }        
        
        public IActionResult Create()
        {
            var model = ConvertEntityToModel(new Categoria());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] CategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = new Categoria();
                categoria.Nome = model.Nome;
                categoria.Cor = model.Cor;
                if (string.IsNullOrWhiteSpace(categoria.Cor))
                    categoria.Cor = Categoria.DEFAULT_COR;
                categoria.Usuario = GetUsuario();

                Service.SalvarCategoria(categoria);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        private Usuario GetUsuario()
        {
            return Service.UsuarioService.GetUsuario(UsuarioLogadoID);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Categoria entidade = Service.GetCategoriaPorID(id.Value);
            if (entidade == null)
                return NotFound();

            return View(ConvertEntityToModel(entidade));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind] CategoriaViewModel model)
        {
            if (id == null)
                return NotFound();

            Categoria entidade = Service.GetCategoriaPorID(id.Value);
            if (entidade == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                entidade.Nome = model.Nome;
                entidade.Cor = model.Cor;
                if (string.IsNullOrWhiteSpace(entidade.Cor))
                    entidade.Cor = Categoria.DEFAULT_COR;     
                
                Service.SalvarCategoria(entidade);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Service.ApagarCategoria(id);

            return RedirectToAction(nameof(Index));
        }

        public static CategoriaViewModel ConvertEntityToModel(Categoria entidade)
        {
            CategoriaViewModel model = new CategoriaViewModel();
            model.ID = entidade.ID;
            model.Nome = entidade.Nome;
            model.Cor = entidade.Cor;

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

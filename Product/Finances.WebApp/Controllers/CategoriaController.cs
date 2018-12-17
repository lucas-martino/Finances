using System.Linq;
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
        private UsuarioService UsuarioService;
        public CategoriaController(CategoriaService categoriaService, UsuarioService usuarioService)
            :base(categoriaService)
        {
            UsuarioService = usuarioService;
        }
        
        public IActionResult Index()
        {
            var model = ConvertEntityToModel(Service.GetCategoriasPorUsuario(UsuarioLogadoID));

            return View(model);
        }        
        
        public IActionResult Create()
        {
            var model = ConvertEntityToModel(new Categoria());
            model.Categorias = GetCategoriasQuePermiteFilhos();

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
                categoria.Icone = model.Icone;
                categoria.Pai = GetCategoria(model.Pai.ID);
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
            return UsuarioService.GetUsuario(UsuarioLogadoID);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Categoria entidade = Service.GetCategoriaPorID(id.Value);
            if (entidade == null)
                return NotFound();

            var model = ConvertEntityToModel(entidade);
            model.Categorias = GetCategoriasQuePermiteFilhos();
            
            return View(model);
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
                entidade.Icone = model.Icone;
                entidade.Pai = GetCategoria(model.Pai.ID);
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
            model.Icone = entidade.Icone;
            if (entidade.Pai != null)
                model.Pai = ConvertEntityToModel(entidade.Pai);

            return model;
        }

        private static SelectListItem ConvertEntityToSelectListItem(Categoria entidade)
        {
            SelectListItem model = new SelectListItem();
            model.Value = entidade.ID.ToString();
            if (entidade.Pai != null)
                model.Text = string.Format("{0} - {1}", entidade.Pai.Nome, entidade.Nome);
            else
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

        public static IList<SelectListItem> ConvertEntityToSelectListItem(IEnumerable<Categoria> entidade)
        {
            IList<SelectListItem> lista = new List<SelectListItem>();
            foreach (Categoria categoria in entidade)
                lista.Add(ConvertEntityToSelectListItem(categoria));
                
            return lista.OrderBy(i => i.Text).ToList();
        }

        private IEnumerable<SelectListItem> GetCategoriasQuePermiteFilhos()
        {
            IList<SelectListItem> lista = CategoriaController.ConvertEntityToSelectListItem(Service.GetCategoriaQuePermiteFilhosPorUsuario(UsuarioLogadoID));
            lista.Insert(0, new SelectListItem(" ", "0"));

            return lista;
        }

        private Categoria GetCategoria(int categoriaID)
        {
            return Service.GetCategoriaPorID(categoriaID);
        }
    }
}

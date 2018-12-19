using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Finances.WebApp.Models;
using Finances.Service;
using Finances.Domain.Entity;

namespace Finances.WebApp.Controllers
{
    public class OrcamentoController : FinancesController<OrcamentoService>
    {
        private CategoriaService CategoriaService;
        private VigenciaService VigenciaService;
        public OrcamentoController(OrcamentoService orcamentoService, CategoriaService categoriaService, VigenciaService vigenciaService)
            :base(orcamentoService)
        {
            CategoriaService = categoriaService;
            VigenciaService = vigenciaService;
        }

        public IActionResult Index(int? vigenciaRefencia)
        {
            Vigencia vigencia = ResolveVigencia(vigenciaRefencia);
            var model = ConvertEntityToModel(Service.GetOrcamentoPorVigencia(vigencia));
            model.Vigencia = VigenciaController.ConvertEntityToModel(vigencia);

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = ConvertEntityToModel(GetOrcamento(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] OrcamentoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Orcamento orcamento = GetOrcamento(id);
                orcamento.Valor = model.Valor;
                SalvarOrcamento(orcamento);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult CreateOrcamentoCategoria(int orcamentoID)
        {
            OrcamentoCategoriaViewModel model = new OrcamentoCategoriaViewModel();
            model.OrcamentoID = orcamentoID;
            model.Categorias = GetCategorias();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrcamentoCategoria([Bind] OrcamentoCategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Orcamento orcamento = GetOrcamento(model.OrcamentoID);
                if (orcamento is null)
                    return NotFound();

                OrcamentoCategoria orcamentoCategoria = new OrcamentoCategoria();
                orcamentoCategoria.Valor = model.Valor;
                orcamentoCategoria.Categoria = GetCategoria(model.Categoria.ID);
                orcamento.AddOrcamentoCategoria(orcamentoCategoria);
                Service.SalvarOrcamento(orcamento);

                return RedirectToAction(nameof(Index));
            }

            model.Categorias = GetCategorias();
            return View(model);
        }

        private Categoria GetCategoria(int categoriaID)
        {
            return CategoriaService.GetCategoriaPorID(categoriaID);
        }

        public IActionResult EditOrcamentoCategoria(int orcamentoCategoriaID)
        {
            OrcamentoCategoria entidade = Service.GetOrcamentoCategoriaPorID(orcamentoCategoriaID);
            if (entidade == null)
                return NotFound();

            OrcamentoCategoriaViewModel model = ConvertEntityToModel(entidade);
            model.Categorias = GetCategorias();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOrcamentoCategoria([Bind] OrcamentoCategoriaViewModel model)
        {
            OrcamentoCategoria orcamentoCategoria = Service.GetOrcamentoCategoriaPorID(model.ID);
            if (orcamentoCategoria == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                orcamentoCategoria.Valor = model.Valor;
                orcamentoCategoria.Categoria = GetCategoria(model.Categoria.ID);
                Service.SalvarOrcamentoCategoria(orcamentoCategoria);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult DeleteOrcamentoCategoria(int? orcamentoCategoriaID)
        {
            if (orcamentoCategoriaID == null)
                return NotFound();

            Service.ApagarOrcamentoCategoria(orcamentoCategoriaID.Value);

            return RedirectToAction(nameof(Index));
        }

        private static OrcamentoViewModel ConvertEntityToModel(Orcamento entidade)
        {
            OrcamentoViewModel model = new OrcamentoViewModel();
            model.ID = entidade.ID;
            model.Valor = entidade.Valor;
            model.OrcamentosCategoria = ConvertEntityToModel(entidade.OrcamentosCategoria);

            return model;
        }

        private Orcamento GetOrcamento(int id)
        {
            return Service.GetOrcamentoPorID(id);
        }

        private IEnumerable<SelectListItem> GetCategorias()
        {
            return GetCategorias(null);
        }

        private IEnumerable<SelectListItem> GetCategorias(Categoria categoria)
        {
            IList<Categoria> lista = CategoriaService.GetCategoriasDisponiveisOrcamentoPorUsuario(UsuarioLogadoID);
            if (categoria != null && lista.FirstOrDefault(C => C.ID == categoria.ID) == null)
                lista.Add(categoria);

            return CategoriaController.ConvertEntityToSelectListItem(lista);
        }

        private static OrcamentoCategoriaViewModel ConvertEntityToModel(OrcamentoCategoria entidade)
        {
            OrcamentoCategoriaViewModel model = new OrcamentoCategoriaViewModel();
            model.ID = entidade.ID;
            model.Valor = entidade.Valor;
            model.OrcamentoID = entidade.Orcamento.ID;
            model.Categoria = CategoriaController.ConvertEntityToModel(entidade.Categoria);

            return model;
        }

        private static IEnumerable<OrcamentoCategoriaViewModel> ConvertEntityToModel(IEnumerable<OrcamentoCategoria> entidade)
        {
            IList<OrcamentoCategoriaViewModel> lista = new List<OrcamentoCategoriaViewModel>();
            foreach (var orcamento in entidade)
                lista.Add(ConvertEntityToModel(orcamento));
                
            return lista;
        }

        private void SalvarOrcamento(Orcamento orcamento)
        {
            Service.SalvarOrcamento(orcamento);
        }
        private Vigencia ResolveVigencia(int? vigenciaRefencia)
        {
            if (vigenciaRefencia.HasValue)
                return VigenciaService.GetVigenciaPorUsuario(UsuarioLogadoID, vigenciaRefencia.Value);
            else
                return VigenciaService.GetVigenciaAtualPorUsuario(UsuarioLogadoID);
        }
    }
}

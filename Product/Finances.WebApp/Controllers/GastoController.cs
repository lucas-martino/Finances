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
    public class GastoController : FinancesController<GastoService>
    {
        private VigenciaService VigenciaService;
        private CategoriaService CategoriaService;
        public GastoController(GastoService gastoService, VigenciaService vigenciaService, CategoriaService categoriaService)
            :base(gastoService)
        {
            VigenciaService = vigenciaService;
            CategoriaService = categoriaService;
        }

        public IActionResult Index(int? vigenciaRefencia)
        {
            ViewBag.Title = "Gastos";
            Vigencia vigencia = ResolveVigencia(vigenciaRefencia);
            GastoListaViewModel model = new GastoListaViewModel();
            model.Gastos = ConvertEntityToModel(Service.GetGastosPorVigencia(vigencia));
            model.Vigencia = VigenciaController.ConvertEntityToModel(vigencia);

            return View(model);
        }     

        public IActionResult GastoPorCategoria(int? vigenciaRefencia, int categoriaID)
        {
            Categoria categoria = CategoriaService.GetCategoriaPorID(categoriaID);
            ViewBag.Title = string.Format("Gastos Por Categoria: {0}{1}", ((categoria.Pai == null)? "" : string.Format("{0} - ", categoria.Pai.Nome)), categoria.Nome);
            Vigencia vigencia = ResolveVigencia(vigenciaRefencia);
            GastoListaViewModel model = new GastoListaViewModel();
            model.Gastos = ConvertEntityToModel(Service.GetGastosPorCategoriaEVigencia(categoriaID, vigencia));
            model.Vigencia = VigenciaController.ConvertEntityToModel(vigencia);

            return View("Index", model);
        }   

        public IActionResult GastoNaoCategorizado(int? vigenciaRefencia)
        {
            ViewBag.Title = "Gastos Não Categorizado";
            Vigencia vigencia = ResolveVigencia(vigenciaRefencia);
            GastoListaViewModel model = new GastoListaViewModel();
            model.Gastos = ConvertEntityToModel(Service.GetGastosNaoCategorizadoPorVigencia(vigencia));
            model.Vigencia = VigenciaController.ConvertEntityToModel(vigencia);
            
            return View("Index", model);
        } 
        
        public IActionResult Create()
        {
            GastoViewModel model = new GastoViewModel();
            model.Data = DateTime.Today;
            model.MinDate = GenereteMinDate(model.Data);
            model.MaxDate = GenereteMaxDate(model.Data);;
            model.Categorias = GetCategorias();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] GastoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Gasto entidade = new Gasto();
                entidade.Data = model.Data;
                entidade.Valor = model.Valor;
                entidade.Categoria =  GetCategoria(model.Categoria.ID);
                entidade.Observacao = model.Observacao;
                entidade.Vigencia = GetVigenciaAtual();
                SalvarGasto(entidade);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        private Vigencia GetVigenciaAtual()
        {
            return VigenciaService.GetVigenciaAtualPorUsuario(UsuarioLogadoID);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Gasto entidade = GetGasto(id.Value);
            if (entidade == null)
                return NotFound();

            GastoViewModel model = ConvertEntityToModel(entidade);
            model.MinDate = GenereteMinDate(entidade.Data);
            model.MaxDate = GenereteMaxDate(entidade.Data);
            model.Categorias = GetCategorias();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind] GastoViewModel model)
        {
            if (id == null)
                return NotFound();

            Gasto entidade = GetGasto(id.Value);
            if (entidade == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                entidade.Data = model.Data;
                entidade.Valor = model.Valor;
                entidade.Categoria = GetCategoria(model.Categoria.ID);
                entidade.Observacao = model.Observacao;
                SalvarGasto(entidade);

                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Service.ApagarGasto(id);

            return RedirectToAction(nameof(Index));
        }

        private static GastoViewModel ConvertEntityToModel(Gasto entidade)
        {
            GastoViewModel model = new GastoViewModel();
            model.ID = entidade.ID;
            model.Data = entidade.Data;
            model.Valor = entidade.Valor;
            model.Observacao = entidade.Observacao;
            if (entidade.Categoria != null)
                model.Categoria = CategoriaController.ConvertEntityToModel(entidade.Categoria);            

            return model;
        }

        private static IEnumerable<GastoViewModel> ConvertEntityToModel(IEnumerable<Gasto> entidade)
        {
            IList<GastoViewModel> lista = new List<GastoViewModel>();
            foreach (Gasto gasto in entidade)
                lista.Add(ConvertEntityToModel(gasto));
                
            return lista;
        }

        private IEnumerable<SelectListItem> GetCategorias()
        {
            IList<SelectListItem> lista = CategoriaController.ConvertEntityToSelectListItem(CategoriaService.GetCategoriasPorUsuario(UsuarioLogadoID));
            lista.Insert(0, new SelectListItem(" ", "0"));

            return lista;
        }

        private static string GenereteMinDate(DateTime data)
        {
            return new DateTime(data.Year, data.Month, 1).ToString("yyyy-MM-dd");
        }

        private static string GenereteMaxDate(DateTime data)
        {
            return new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month)).ToString("yyyy-MM-dd");
        }

        private Gasto GetGasto(int gastoID)
        {
            return Service.GetGastosPorID(gastoID);
        }

        private Categoria GetCategoria(int categoriaID)
        {
            return CategoriaService.GetCategoriaPorID(categoriaID);
        }

        private void SalvarGasto(Gasto gasto)
        {
            Service.SalvarGasto(gasto);
        }

        private Vigencia ResolveVigencia(int? vigenciaRefencia)
        {
            if (vigenciaRefencia.HasValue)
                return VigenciaService.GetVigenciaPorUsuario(UsuarioLogadoID, vigenciaRefencia.Value);
            else
                return VigenciaService.GetVigenciaAtualPorUsuario(UsuarioLogadoID);
        }

        private string ResolveTitulo(string titulo, int? vigenciaRefencia)
        {
            return string.Format("{0}{1}", titulo, ((vigenciaRefencia.HasValue) ? string.Format(" - Vigência: {0}", vigenciaRefencia.Value.ToString())  : ""));
        }
    }
}

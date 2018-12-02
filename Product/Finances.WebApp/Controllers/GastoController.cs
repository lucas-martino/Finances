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
        public GastoController(GastoService gastoService)
            :base(gastoService)
        {            
        }

        public IActionResult Index()
        {
            var model = ConvertEntityToModel(Service.GetGastosVigenciaAtual(UsuarioLogadoID));
            return View(model);
        }     

        public IActionResult GastoPorCategoria(int categoriaID)
        {
            var model = ConvertEntityToModel(Service.GetGastosPorCategoriaVigenciaAtual(categoriaID, UsuarioLogadoID));
            return View(model);
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
            return Service.VigenciaService.GetVigenciaAtualPorUsuario(UsuarioLogadoID);
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
            model.Categoria = CategoriaController.ConvertEntityToModel(entidade.Categoria);
            model.Observacao = entidade.Observacao;

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
            return CategoriaController.ConvertEntityToSelectListItem(Service.CategoriaService.GetCategoriasPorUsuario(UsuarioLogadoID));
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
            return Service.CategoriaService.GetCategoriaPorID(categoriaID);
        }

        private void SalvarGasto(Gasto gasto)
        {
            Service.SalvarGasto(gasto);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Finances.WebApp.Models;

namespace Finances.WebApp.Controllers
{
    public class LancamentoController : Controller
    {
        
        public IActionResult Index()
        {
            return View(ConvertEntityToModel(Banco.Get().Lancamentos));
        }     

        public IActionResult LancamentoPorCategoria(int categoriaID)
        {
            return View(ConvertEntityToModel(Banco.Get().Lancamentos.Where(i => i.CategoriaID == categoriaID)));
        }   
        
        public IActionResult Create()
        {
            LancamentoViewModel model = new LancamentoViewModel();
            model.Data = DateTime.Today;
            model.MinDate = GenereteMinDate(model.Data);
            model.MaxDate = GenereteMaxDate(model.Data);;
            model.Categorias = CategoriaController.ConvertEntityToSelectListItem(Banco.Get().Categorias);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] LancamentoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Lancamento entidade = new Lancamento();
                entidade.ID = Banco.Get().Lancamentos.Count() + 1;
                entidade.Data = model.Data;
                entidade.Valor = model.Valor;
                entidade.CategoriaID = model.CategoriaID;
                entidade.Observacao = model.Observacao;

                Banco.Get().Lancamentos.Add(entidade);
                Banco.Salvar();
                return RedirectToAction(nameof(Index));
            }

            model.MinDate = GenereteMinDate(model.Data);
            model.MaxDate = GenereteMaxDate(model.Data);
            model.Categorias = CategoriaController.ConvertEntityToSelectListItem(Banco.Get().Categorias);
            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Lancamento entidade = Banco.Get().Lancamentos.FirstOrDefault(i => i.ID == id.Value);
            if (entidade == null)
                return NotFound();

            LancamentoViewModel model = ConvertEntityToModel(entidade);
            model.MinDate = GenereteMinDate(entidade.Data);
            model.MaxDate = GenereteMaxDate(entidade.Data);
            model.Categorias = CategoriaController.ConvertEntityToSelectListItem(Banco.Get().Categorias);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind] LancamentoViewModel model)
        {
            if (id == null)
                return NotFound();

            Lancamento entidade = Banco.Get().Lancamentos.FirstOrDefault(i => i.ID == id.Value);
            if (entidade == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                entidade.Data = model.Data;
                entidade.Valor = model.Valor;
                entidade.CategoriaID = model.CategoriaID;
                entidade.Observacao = model.Observacao;
                Banco.Salvar();
                return RedirectToAction(nameof(Index));
            }

            model.MinDate = GenereteMinDate(model.Data);
            model.MaxDate = GenereteMaxDate(model.Data);
            model.Categorias = CategoriaController.ConvertEntityToSelectListItem(Banco.Get().Categorias);
            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            Lancamento entidade = Banco.Get().Lancamentos.FirstOrDefault(i => i.ID == id.Value);
            if (entidade == null)
                return NotFound();

            Banco.Get().Lancamentos.Remove(entidade);
            Banco.Salvar();

            return RedirectToAction(nameof(Index));
        }

        private static LancamentoViewModel ConvertEntityToModel(Lancamento entidade)
        {
            LancamentoViewModel model = new LancamentoViewModel();
            Categoria categoria = Banco.Get().Categorias.FirstOrDefault(i => i.ID == entidade.CategoriaID);
            model.ID = entidade.ID;
            model.Data = entidade.Data;
            model.Valor = entidade.Valor;
            model.CategoriaID = entidade.CategoriaID;
            model.Categoria = categoria.Nome;
            model.CategoriaCor = categoria.Cor;
            model.Observacao = entidade.Observacao;

            return model;
        }

        private static IEnumerable<LancamentoViewModel> ConvertEntityToModel(IEnumerable<Lancamento> entidade)
        {
            IList<LancamentoViewModel> lista = new List<LancamentoViewModel>();
            foreach (Lancamento lancamento in entidade)
                lista.Add(ConvertEntityToModel(lancamento));
                
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

    }
}

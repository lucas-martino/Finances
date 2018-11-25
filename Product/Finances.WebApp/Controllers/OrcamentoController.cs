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
            return RedirectToAction(nameof(Edit));
        }

        public IActionResult Edit()
        {
            return View(ConvertEntityToModel(Banco.Get().Orcamento));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] OrcamentoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Banco.Get().Orcamento.Valor = model.Valor;
                Banco.Salvar();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        private static OrcamentoViewModel ConvertEntityToModel(Orcamento entidade)
        {
            OrcamentoViewModel model = new OrcamentoViewModel();
            model.Valor = entidade.Valor;

            return model;
        }
    }
}

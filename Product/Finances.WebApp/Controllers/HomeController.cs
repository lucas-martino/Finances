using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Finances.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using Finances.Domain.Exception;
using Framework.Domain.Exception;

namespace Finances.WebApp.Controllers
{
    public class HomeController : FinancesController
    {
        public IActionResult Index()
        {
            if (IsUsuarioLogado())
                return RedirectToAction("Home", "Demonstrativo");
            else
                return RedirectToAction("Login", "Usuario");                   
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ErrorViewModel model = new ErrorViewModel();
            model.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {

                model.CustomError = (exceptionFeature.Error is DomainException);
                model.Message = exceptionFeature.Error.Message;
            }

            return View(model);
        }
    }
}

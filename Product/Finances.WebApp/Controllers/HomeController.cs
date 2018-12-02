using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Finances.WebApp.Models;
using Microsoft.AspNetCore.Http;

namespace Finances.WebApp.Controllers
{
    public class HomeController : FinancesController
    {
        public IActionResult Index()
        {
            if (IsUsuarioLogado())
                return RedirectToAction("Index", "Demonstrativo");
            else
                return RedirectToAction("Login", "Usuario");                   
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Finances.WebApp.Models;
using Finances.Service;
using Finances.Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace Finances.WebApp.Controllers
{
    public class UsuarioController : FinancesController<UsuarioService>
    {
        public UsuarioController(UsuarioService service)
            : base(service)
        {
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login([Bind] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = Service.GetUsuario(model.Login, model.Senha);
                if (usuario != null)
                {
                    UserLogin(usuario.Id);

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            UserLogout();

            return RedirectToAction(nameof(Login));
        }
    }
}

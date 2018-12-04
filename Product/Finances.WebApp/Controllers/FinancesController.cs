using Finances.Domain.Entity;
using Finances.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finances.WebApp.Controllers
{
    public class FinancesController<TService> : FinancesController
    where TService: IFinancesApplicationService
    {
        public FinancesController(TService service)
        {
            Service = service;
        }

        public TService Service { get; private set; }        
    }

    public class FinancesController : Controller
    {
        public bool IsUsuarioLogado()
        {
            return UsuarioLogadoID > 0;
        }

        public int UsuarioLogadoID
        {
            get
            {
                int? id = HttpContext.Session.GetInt32("UsuarioLogadoID"); 
                if (id.HasValue)
                    return id.Value;

                return 0; 
            }
        }

        public void UserLogin(int usuarioID)
        {
            HttpContext.Session.SetInt32("UsuarioLogadoID", usuarioID);
        }

        public void UserLogout()
        {
            HttpContext.Session.Clear();
        }
    }
}
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Finances.Service;
using Finances.Domain.Entity;
using Finances.WebApp.Models;
using System;

namespace Finances.WebApp.Controllers
{
    public class VigenciaController : FinancesController<VigenciaService>
    {
        public VigenciaController(VigenciaService vigenciaService)
            :base(vigenciaService)
        {
        }
        
        public IActionResult Index()
        {
            HistoricoViewModel model = new HistoricoViewModel();
            model.Vigencias = ConvertEntityToModel(Service.GetVigenciasPorUsuario(UsuarioLogadoID));

            return View(model);
        }

        public static IEnumerable<VigenciaViewModel> ConvertEntityToModel(IEnumerable<Vigencia> entityList)
        {
            IList<VigenciaViewModel> lista = new List<VigenciaViewModel>();
            foreach (var item in entityList)
                lista.Add(ConvertEntityToModel(item));

            return lista;
        }

        public static VigenciaViewModel ConvertEntityToModel(Vigencia entity)
        {
            VigenciaViewModel model = new VigenciaViewModel();
            model.Referencia = entity.Referencia;
            model.Nome = string.Format("{0}/{1}", entity.Mes().ToString(), entity.Ano().ToString());

            return model;
        }
    }
}

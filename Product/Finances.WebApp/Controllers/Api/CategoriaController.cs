using System.Collections.Generic;
using Finances.Domain.Entity;
using Finances.Service;
using Microsoft.AspNetCore.Mvc;

namespace Finances.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private CategoriaService CategoriaService;
        private int UsuarioLogadoID = 1;
        public CategoriaController(CategoriaService categoriaService)
        {
            CategoriaService = categoriaService;
        }

        [HttpGet("[action]")]
        public IEnumerable<Categoria> Index()
        {
            return CategoriaService.GetCategoriasPorUsuario(UsuarioLogadoID);            
        }
    }
}
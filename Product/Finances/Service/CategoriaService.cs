using System;
using System.Collections.Generic;
using Finances.Domain.Entity;
using Finances.Domain.Repository;
using Finances.Domain.Service;

namespace Finances.Service
{
    public class CategoriaService : IFinancesApplicationService
    {
        private ICategoriaRepository CategoriaRepository;
        private CategoriaDomainService CategoriaDomainService;
        private UsuarioService UsuarioService;

        public CategoriaService(ICategoriaRepository categoriaRepository, CategoriaDomainService categoriaDomainService, UsuarioService usuarioService)
        {
            CategoriaRepository = categoriaRepository;
            CategoriaDomainService  = categoriaDomainService;
            UsuarioService = usuarioService;
        }

        public IEnumerable<Categoria> GetCategoriasPorUsuario(ulong usuarioID)
        {
            Usuario usuario = GetUsuario(usuarioID);
            return CategoriaRepository.GetCategoriaPorUsuario(usuario);
        }

        public IEnumerable<Categoria> GetCategoriaQuePermiteFilhosPorUsuario(ulong usuarioID)
        {
            Usuario usuario = GetUsuario(usuarioID);
            return CategoriaRepository.GetCategoriaLevel1PorUsuario(usuario);
        }

        public Categoria GetCategoriaPorID(ulong id)
        {
            return CategoriaRepository.GetByID(id);
        }

        public ulong SalvarCategoria(Categoria categoria)
        {
            return CategoriaDomainService.SaveCategoria(categoria);
        }

        public void ApagarCategoria(ulong id)
        {
            CategoriaDomainService.DeleteCategoria(id);
        }

        public IList<Categoria> GetCategoriasDisponiveisOrcamentoPorUsuario(ulong usuarioID)
        {
            return new List<Categoria>(GetCategoriasPorUsuario(usuarioID));
        }

        private Usuario GetUsuario(ulong usuarioID)
        {
            return UsuarioService.GetUsuario(usuarioID);
        }
    }
}
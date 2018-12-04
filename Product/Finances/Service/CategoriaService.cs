using System;
using System.Collections.Generic;
using Finances.Domain.Entity;
using Finances.Domain.Repository;
using Finances.Domain.Service;

namespace Finances.Service
{
    public class CategoriaService : IApplicationService
    {
        private ICategoriaRepository CategoriaRepository;
        private UsuarioService UsuarioService;
        
        public CategoriaService(ICategoriaRepository categoriaRepository, UsuarioService usuarioService)
        {
            CategoriaRepository = categoriaRepository;
            UsuarioService = usuarioService;
        }

        public IEnumerable<Categoria> GetCategoriasPorUsuario(int usuarioID)
        {
            Usuario usuario = GetUsuario(usuarioID);
            return CategoriaRepository.GetCategoriaPorUsuario(usuario);
        }

        public Categoria GetCategoriaPorID(int id)
        {
            return CategoriaRepository.GetByID(id);
        }

        public long SalvarCategoria(Categoria categoria)
        {
            return CategoriaRepository.Save(categoria);
        }

        public void ApagarCategoria(int id)
        {
            CategoriaRepository.Delete(id);
        }

        public IList<Categoria> GetCategoriasDisponiveisOrcamentoPorUsuario(int usuarioID)
        {
            return new List<Categoria>(GetCategoriasPorUsuario(usuarioID));
        }

        private Usuario GetUsuario(int usuarioID)
        {
            return UsuarioService.GetUsuario(usuarioID);
        }
    }
}
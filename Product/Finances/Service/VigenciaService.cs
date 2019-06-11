using System;
using System.Collections.Generic;
using Finances.Domain.Entity;
using Finances.Domain.Repository;
using Finances.Domain.Service;

namespace Finances.Service
{
    public class VigenciaService : IFinancesApplicationService
    {
        private IVigenciaRepository VigenciaRepository;
        private VigenciaDomainService VigenciaDomainService;
        private UsuarioService UsuarioService;

        public VigenciaService(IVigenciaRepository vigenciaRepository, VigenciaDomainService vigenciaDomainService, UsuarioService usuarioService)
        {
            VigenciaRepository = vigenciaRepository;
            VigenciaDomainService = vigenciaDomainService;
            UsuarioService = usuarioService;
        }

        public IEnumerable<Vigencia> GetVigenciasPorUsuario(ulong usuarioID)
        {
            Usuario usuario = GetUsuario(usuarioID);
            return VigenciaRepository.GetVigenciasPorUsuario(usuario);
        }

        public virtual Vigencia GetVigenciaPorUsuario(ulong usuarioID, int referencia)
        {
            Usuario usuario = GetUsuario(usuarioID);
            return VigenciaRepository.GetVigencia(usuario, referencia);
        }

        public virtual Vigencia GetVigenciaAtualPorUsuario(ulong usuarioID)
        {
            Usuario usuario = GetUsuario(usuarioID);
            return VigenciaDomainService.GetVigenciaAtualPorUsuario(usuario);
        }

        public Vigencia GetVigenciaPorID(ulong vigenciaID)
        {
            return VigenciaRepository.GetByID(vigenciaID);
        }

        private Usuario GetUsuario(ulong usuarioID)
        {
            return UsuarioService.GetUsuario(usuarioID);
        }
    }
}
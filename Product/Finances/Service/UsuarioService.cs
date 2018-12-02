using System;
using Finances.Domain.Entity;
using Finances.Domain.Service;

namespace Finances.Service
{
    public class UsuarioService : IApplicationService
    {
        private UsuarioDomainService UsuarioDomainService;

        public UsuarioService(UsuarioDomainService usuarioDomainService)
        {
            UsuarioDomainService = usuarioDomainService;
        }

        public Usuario GetUsuario(string login, string senha)
        {
            return UsuarioDomainService.GetUsuario(login, senha);
        }

        public virtual Usuario GetUsuario(int usuarioID)
        {
            return UsuarioDomainService.GetUsuario(usuarioID);
        }
    }
}
using System;
using Finances.Domain.Entity;
using Finances.Domain.Exception;
using Finances.Domain.Repository;

namespace Finances.Domain.Service
{
    public class UsuarioDomainService
    {
        private IUsuarioRepository UsuarioRepository;

        public UsuarioDomainService(IUsuarioRepository usuarioRepository)
        {
            UsuarioRepository = usuarioRepository;
        }
        
        public virtual Usuario GetUsuario(int usuarioID)
        {
            Usuario usuario = UsuarioRepository.GetByID(usuarioID);
            if (UsuarioNaoLocalizado(usuario))
                throw new UsuarioInvalidoException();

            return usuario;
        }

        public virtual Usuario GetUsuario(string login, string senha)
        {
            Usuario usuario = UsuarioRepository.GetUsuarioByLoginSenha(login, senha);
            if (UsuarioNaoLocalizado(usuario))
                throw new UsuarioOuSenhaInvalidoException();

            return usuario;
        }

        private bool UsuarioNaoLocalizado(Usuario usuario)
        {
            return usuario is null;
        }
    }
}
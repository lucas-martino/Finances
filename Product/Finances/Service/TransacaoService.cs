using System;
using System.Collections.Generic;
using Finances.Domain.Entity;
using Finances.Domain.Repository;

namespace Finances.Service
{
    public class TransacaoService : IFinancesApplicationService
    {
        private ITransacaoRepository TransacaoRepository; 
        private UsuarioService UsuarioService;
        
        public TransacaoService(ITransacaoRepository transacaoRepository, UsuarioService usuarioService)
        {
            TransacaoRepository = transacaoRepository;
            UsuarioService = usuarioService;
        }

        public Transacao GetTransacaoById(int id)
        {
            return TransacaoRepository.GetByID(id);
        }

        public IEnumerable<Transacao> GetTransacoesDebitoPorVigenciaPorUsuario(int referenciaVigencia, int usuarioID)
        {
            return TransacaoRepository.GetTransacoesDebitoPorVigenciaPorUsuario(new Vigencia(referenciaVigencia), GetUsuario(usuarioID));
        }

        private Usuario GetUsuario(int usuarioID)
        {
            return UsuarioService.GetUsuario(usuarioID);
        }
    }
}
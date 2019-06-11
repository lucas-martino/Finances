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

        public Transacao GetTransacaoById(ulong id)
        {
            return TransacaoRepository.GetByID(id);
        }

        public IEnumerable<Transacao> GetTransacoesDebitoPorVigenciaPorUsuario(int referenciaVigencia, ulong usuarioID)
        {
            return TransacaoRepository.GetTransacoesDebitoPorVigenciaPorUsuario(new Vigencia(referenciaVigencia), GetUsuario(usuarioID));
        }

        private Usuario GetUsuario(ulong usuarioID)
        {
            return UsuarioService.GetUsuario(usuarioID);
        }
    }
}
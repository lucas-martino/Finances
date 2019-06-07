using System;
using System.Collections.Generic;
using Finances.Domain.Entity;
using Finances.Service;
using Microsoft.AspNetCore.Mvc;

namespace Finances.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : Controller
    {
        private TransacaoService TransacaoService;
        private int UsuarioLogadoID = 1;
        public TransacaoController(TransacaoService transacaoService)
        {
            TransacaoService = transacaoService;
        }

        [HttpGet("{id}")]
        public object Get(int id)
        {
            Transacao transacao = TransacaoService.GetTransacaoById(id);
            if (transacao == null)
                return NotFound();

            if (transacao.Usuario.ID != UsuarioLogadoID)
                return NotPermit();

            return transacao;
        }

        private object NotPermit()
        {
            throw new NotImplementedException();
        }

        [HttpGet("vigencia/{vigencia}")]
        public IEnumerable<Transacao> GetTransacaoes(int vigencia)
        {
            return TransacaoService.GetTransacoesDebitoPorVigenciaPorUsuario(vigencia, UsuarioLogadoID);            
        }

        [HttpGet("vigencia/{vigencia}/debitos")]
        public IEnumerable<Transacao> GetDebitos(int vigencia)
        {
            return TransacaoService.GetTransacoesDebitoPorVigenciaPorUsuario(vigencia, UsuarioLogadoID);            
        }

        [HttpGet("vigencia/{vigencia}/creditos")]
        public IEnumerable<Transacao> GetCreditos(int vigencia)
        {
            return TransacaoService.GetTransacoesDebitoPorVigenciaPorUsuario(vigencia, UsuarioLogadoID);            
        }
    }
}
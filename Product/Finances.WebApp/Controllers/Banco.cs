using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Finances.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Finances.WebApp.Controllers
{
    public class Banco
    {
        private const string ARQUIVO = "banco.json";
        
        private static Tabela _tabela = null;
        public static Tabela Get()
        {
            if (_tabela == null)
            {
                _tabela = Deserialize();
                if (_tabela == null)
                    _tabela = new Tabela();
            }

            return _tabela;
        }

        public static void Salvar()
        {

            Serialize(_tabela);
        }

        public static void Serialize(object obj)
        {
            var serializer = new JsonSerializer();        

            using (var sw = new StreamWriter(ARQUIVO))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, obj);           
                }
        }
    
        public static Tabela Deserialize()
        {
            using (var sw = new StreamReader(ARQUIVO))
                return JsonConvert.DeserializeObject<Tabela>(sw.ReadToEnd());
        }
    }

    public class Tabela
    {
        public IList<Lancamento> Lancamentos = new List<Lancamento>();
        public IList<Categoria> Categorias = new List<Categoria>();
        public IList<Orcamento> Orcamentos = new List<Orcamento>();
    }

    public class Lancamento
    {
        public long ID { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public long CategoriaID { get; set; }
        public string Observacao { get; set; }
    }

    public class Categoria
    {
        public long ID { get; set; }
        public string Nome { get; set; }
    }

    public class Orcamento
    {
        public long ID { get; set; }
        public long CategoriaID { get; set; }
        public decimal Valor { get; set; }
    }
}
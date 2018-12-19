using Finances.Domain.Entity;

namespace Test.Finances.Factory
{
    public class OrcamentoCategoriaFactory : BaseFactory<OrcamentoCategoria>
    {
        private OrcamentoCategoriaFactory()
        {            
        }

        public static OrcamentoCategoriaFactory Get()
        {
            return new OrcamentoCategoriaFactory();
        }

        public static OrcamentoCategoriaFactory GetValid()
        {
            var factory = Get();
            factory.PopulateValid();

            return factory;
        }

        public OrcamentoCategoriaFactory WithID(int id)
        {
            Instance.ID = id;

            return this;
        }

        public OrcamentoCategoriaFactory WithCategoria(Categoria categoria)
        {
            Instance.Categoria = categoria;

            return this;
        }

        public OrcamentoCategoriaFactory WithValor(decimal valor)
        {
            Instance.Valor = valor;

            return this;
        }

        protected override void PopulateValid()
        {
            Instance.ID = 1;
            Instance.Orcamento = OrcamentoFactory.GetValid().Build();
            Instance.Categoria = CategoriaFactory.GetValid().Build();
            Instance.Valor = 1000;
        }
    }
}
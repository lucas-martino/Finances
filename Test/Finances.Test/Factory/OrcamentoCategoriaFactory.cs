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

        protected override void PopulateValid()
        {
            Instance.ID = 1;
            Instance.Orcamento = OrcamentoFactory.GetValid().Build();
            Instance.Categoria = CategoriaFactory.GetValid().Build();
            Instance.Valor = 1000;
        }
    }
}
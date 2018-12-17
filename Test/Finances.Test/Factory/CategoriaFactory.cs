using Finances.Domain.Entity;

namespace Test.Finances.Factory
{
    public class CategoriaFactory : BaseFactory<Categoria>
    {
        private CategoriaFactory()
        {
        }
        
        public static CategoriaFactory Get()
        {
            return new CategoriaFactory();
        }

        public static CategoriaFactory GetValid()
        {
            var factory = Get();
            factory.PopulateValid();

            return factory;
        }

        public CategoriaFactory WithID(int id)
        {
            Instance.ID = id;

            return this;
        }

        public CategoriaFactory WithPai(Categoria pai)
        {
            Instance.Pai = pai;

            return this;
        }

        protected override void PopulateValid()
        {
            Instance.ID = 1;
            Instance.Nome = "Nome";
            Instance.Cor = "Black";
            Instance.Usuario = UsuarioFactory.GetValid().Build();
        }
    }
}
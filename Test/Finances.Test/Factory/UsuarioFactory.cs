using Finances.Domain.Entity;

namespace Test.Finances.Factory
{
    public class UsuarioFactory : BaseFactory<Usuario>
    {
        private UsuarioFactory()
        {            
        }

        public static UsuarioFactory Get()
        {
            return new UsuarioFactory();
        }

        public static UsuarioFactory GetValid()
        {
            var factory = Get();
            factory.PopulateValid();

            return factory;
        }

        public UsuarioFactory WithID(int id)
        {
            Instance.ID = id;

            return this;
        }

        protected override void PopulateValid()
        {
            Instance.ID = 1;
            Instance.Login = "Login";
            Instance.Nome = "Nome";
            Instance.Senha = "Senha";
        }
    }
}
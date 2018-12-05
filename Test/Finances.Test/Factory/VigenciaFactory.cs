using Finances.Domain.Entity;

namespace Test.Finances.Factory
{
    public class VigenciaFactory : BaseFactory<Vigencia>
    {
        private VigenciaFactory()
        {            
        }
        
        public static VigenciaFactory Get()
        {
            return new VigenciaFactory();
        }

        public static VigenciaFactory GetValid()
        {
            var factory = Get();
            factory.PopulateValid();

            return factory;
        }

        public VigenciaFactory WithUsuario(Usuario usuario)
        {
            Instance.Usuario = usuario;

            return this;
        }

        protected override void PopulateValid()
        {
            Instance.ID = 1;
            Instance.Referencia = 201812;
            Instance.Usuario = UsuarioFactory.GetValid().Build();
        }
    }
}
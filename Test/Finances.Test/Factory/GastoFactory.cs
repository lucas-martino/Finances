using System;
using Finances.Domain.Entity;

namespace Test.Finances.Factory
{
    public class GastoFactory : BaseFactory<Gasto>
    {
        private GastoFactory()
        {
        }
        
        public static GastoFactory Get()
        {
            return new GastoFactory();
        }

        public static GastoFactory GetValid()
        {
            var factory = Get();
            factory.PopulateValid();

            return factory;
        }

        protected override void PopulateValid()
        {
            Instance.ID = 1;
            Instance.Vigencia = VigenciaFactory.GetValid().Build();
            Instance.Data = DateTime.Today;
            Instance.Valor = 100;

        }
    }
}
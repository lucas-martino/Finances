using Finances.Domain.Repository;
using Finances.Domain.Service;
using Finances.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Finances.WebApp
{
    public class DependencyInjection
    {
        public static void Setup(IServiceCollection services)
        {
            SetupServices(services);
            SetupDomainServices(services);
            SetupRepository(services);
        }

        private static void SetupServices(IServiceCollection services)
        {
            services.AddSingleton<CategoriaService, CategoriaService>();
            services.AddSingleton<DemonstrativoService, DemonstrativoService>();
            services.AddSingleton<GastoService, GastoService>();
            services.AddSingleton<OrcamentoService, OrcamentoService>();
            services.AddSingleton<UsuarioService, UsuarioService>();
            services.AddSingleton<VigenciaService, VigenciaService>();
        }

        private static void SetupDomainServices(IServiceCollection services)
        {
            services.AddSingleton<DemonstrativoDomainService, DemonstrativoDomainService>();
            services.AddSingleton<UsuarioDomainService, UsuarioDomainService>();
            services.AddSingleton<VigenciaDomainService, VigenciaDomainService>();
            services.AddSingleton<CategoriaDomainService, CategoriaDomainService>();
        }

        private static void SetupRepository(IServiceCollection services)
        {
            services.AddSingleton<FinancesContext, FinancesContext>(i => new FinancesContext("server=localhost;database=Finances;user=finances;password=pwd;"));
            services.AddSingleton<ICategoriaRepository, CategoriaRepository>();
            services.AddSingleton<IGastoRepository, GastoRepository>();
            services.AddSingleton<IOrcamentoRepository, OrcamentoRepository>();
            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            services.AddSingleton<IVigenciaRepository, VigenciaRepository>();
        }
    }

}
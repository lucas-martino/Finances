using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Finances.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var c = new Finances.Domain.Repository.FinancesContext("server=localhost;database=Finances;user=finances;password=pwd;"))
                c.Database.EnsureCreated();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

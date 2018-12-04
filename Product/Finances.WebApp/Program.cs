using Finances.Domain.Entity;
using Finances.Domain.Repository;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Finances.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

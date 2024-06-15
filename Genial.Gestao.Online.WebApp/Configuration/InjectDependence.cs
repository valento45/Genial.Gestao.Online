using Genial.Gestao.Online.Repository;
using Genial.Gestao.Online.Repository.Interfaces;
using Genial.Gestao.Online.Service;
using Genial.Gestao.Online.Service.Interfaces;
using Npgsql;
using System.Data;
using System.Diagnostics;

namespace Genial.Gestao.Online.WebApp.Configuration
{
    public static class InjectDependence
    {

        public static void AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = "";

            if (!Debugger.IsAttached)
                connectionString = configuration.GetConnectionString("Production");

            else
                connectionString = configuration.GetConnectionString("Postgres");


            NpgsqlConnection con = new NpgsqlConnection(connectionString);
            services.AddSingleton<IDbConnection>(con);
        }

        public static void AddRepositorys(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioService, UsuarioService>();
        }
    }
}

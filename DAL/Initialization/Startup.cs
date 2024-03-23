using ServerWordle.DAL.Repositories;
using ServerWordle.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Hangfire.SqlServer;

namespace ServerWordle.DAL.Initialization
{
    public static class DALStartup
    {
        public static void Init(IConfiguration configuration, IServiceCollection services)
        {
            var connectionString = configuration.GetConnectionString("Develop");
            services.AddDbContextPool<WordsDbContext>(option => option.UseSqlServer(connectionString));

            services.AddTransient<IWordRepository, WordRepository>();
        }
    }
}

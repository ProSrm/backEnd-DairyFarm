using Microsoft.EntityFrameworkCore;

 namespace dairyFarm.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseContext<T>(this IServiceCollection services,
           string connectionString,
           string migrationTableName,
           string migrationSchemaName)
           where T : DbContext
        {
            services.AddDbContext<T>(options =>
            {
                options.UseSqlServer(connectionString,
                    dbOptions => { dbOptions.MigrationsHistoryTable(migrationTableName, migrationSchemaName); });
            });

            return services;
        }
    }
}

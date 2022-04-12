using Limbo.DataAccess.Settings.Extensions;
using Limbo.DataAccess.UnitOfWorks.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Limbo.DataAccess.Extensions {

    /// <inheritdoc/>
    public static class DataAccessExtensions {

        /// <summary>
        /// Adds services for data access
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration) {
            return AddDataAccess(services, configuration, "Nikcio.DataAccess");
        }

        /// <summary>
        /// Adds services for data access
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="configurationSection"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration, string configurationSection) {
            services
                .AddSettings(configuration, configurationSection)
                .AddUnitOfWorks();

            return services;
        }
    }
}

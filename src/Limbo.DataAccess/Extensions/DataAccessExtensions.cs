using System;
using Limbo.DataAccess.Extensions.Options;
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
        /// <param name="dataAccessOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration, Action<DataAccessOptions> dataAccessOptions) {
            var options = new DataAccessOptions(configuration);
            dataAccessOptions(options);
            return AddDataAccess(services, options);
        }

        /// <summary>
        /// Adds services for data access
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dataAccessOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataAccess(this IServiceCollection services, DataAccessOptions dataAccessOptions) {
            services
                .AddSettings(dataAccessOptions.SettingsOptions)
                .AddUnitOfWorks();

            return services;
        }
    }
}

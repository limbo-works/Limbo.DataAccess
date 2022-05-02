using System;
using Limbo.EntityFramework.Extensions.Options;
using Limbo.EntityFramework.Settings.Extensions;
using Limbo.EntityFramework.UnitOfWorks.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Limbo.EntityFramework.Extensions {

    /// <inheritdoc/>
    public static class EntityFrameworkExtensions {

        /// <summary>
        /// Adds services for data access
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="entityFrameworkOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration, Action<EntityFrameworkOptions> entityFrameworkOptions) {
            var options = new EntityFrameworkOptions(configuration);
            entityFrameworkOptions(options);
            return AddEntityFramework(services, options);
        }

        /// <summary>
        /// Adds services for data access
        /// </summary>
        /// <param name="services"></param>
        /// <param name="entityFrameworkOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, EntityFrameworkOptions entityFrameworkOptions) {
            services
                .AddSettings(entityFrameworkOptions.SettingsOptions)
                .AddUnitOfWorks();

            return services;
        }
    }
}

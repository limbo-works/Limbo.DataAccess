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
        /// <param name="EntityFrameworkOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration, Action<EntityFrameworkOptions> EntityFrameworkOptions) {
            var options = new EntityFrameworkOptions(configuration);
            EntityFrameworkOptions(options);
            return AddEntityFramework(services, options);
        }

        /// <summary>
        /// Adds services for data access
        /// </summary>
        /// <param name="services"></param>
        /// <param name="EntityFrameworkOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, EntityFrameworkOptions EntityFrameworkOptions) {
            services
                .AddSettings(EntityFrameworkOptions.SettingsOptions)
                .AddUnitOfWorks();

            return services;
        }
    }
}

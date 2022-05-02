using Limbo.EntityFramework.Settings.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Limbo.EntityFramework.Settings.Extensions {
    /// <summary>
    /// Extensions
    /// </summary>
    public static class SettingsExtensions {
        /// <summary>
        /// Adds the data access settings
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settingsOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddSettings(this IServiceCollection services, SettingsOptions settingsOptions) {
            var EntityFrameworkSettings = new EntityFrameworkSettings();
            settingsOptions.Configuration.Bind(settingsOptions.ConfigurationSection, EntityFrameworkSettings);
            services
                .AddSingleton(EntityFrameworkSettings);

            return services;
        }
    }
}

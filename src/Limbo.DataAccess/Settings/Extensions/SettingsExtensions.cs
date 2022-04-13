using Limbo.DataAccess.Settings.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Limbo.DataAccess.Settings.Extensions {
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
            var dataAccessSettings = new DataAccessSettings();
            settingsOptions.Configuration.Bind(settingsOptions.ConfigurationSection, dataAccessSettings);
            services
                .AddSingleton(dataAccessSettings);

            return services;
        }
    }
}

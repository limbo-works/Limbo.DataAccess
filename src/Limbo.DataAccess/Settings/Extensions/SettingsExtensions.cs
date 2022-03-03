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
        /// <param name="configuration"></param>
        /// <param name="configurationSection"></param>
        /// <returns></returns>
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration, string configurationSection) {
            var dataAccessSettings = new DataAccessSettings();
            configuration.Bind(configurationSection, dataAccessSettings);
            services
                .AddSingleton(dataAccessSettings);

            return services;
        }
    }
}

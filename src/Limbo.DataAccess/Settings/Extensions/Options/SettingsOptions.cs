using Microsoft.Extensions.Configuration;

namespace Limbo.DataAccess.Settings.Extensions.Options {
    /// <summary>
    /// Options for settings
    /// </summary>
    public class SettingsOptions {
        /// <summary>
        /// The app configuration
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// The configruation section to use for the settings
        /// </summary>
        public string ConfigurationSection { get; set; } = "Limbo:DataAccess";

        /// <inheritdoc/>
        public SettingsOptions(IConfiguration configuration) {
            Configuration = configuration;
        }
    }
}

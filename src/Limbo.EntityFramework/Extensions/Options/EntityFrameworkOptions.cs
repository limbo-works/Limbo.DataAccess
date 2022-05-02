using Limbo.EntityFramework.Settings.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Limbo.EntityFramework.Extensions.Options {
    /// <summary>
    /// Options for the data access package
    /// </summary>
    public class EntityFrameworkOptions {
        /// <summary>
        /// Options for the settings
        /// </summary>
        public SettingsOptions SettingsOptions { get; set; }

        /// <inheritdoc/>
        public EntityFrameworkOptions(IConfiguration configuration) {
            SettingsOptions = new(configuration);
        }
    }
}

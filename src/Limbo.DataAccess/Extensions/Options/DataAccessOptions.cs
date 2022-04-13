using Limbo.DataAccess.Settings.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Limbo.DataAccess.Extensions.Options {
    /// <summary>
    /// Options for the data access package
    /// </summary>
    public class DataAccessOptions {
        /// <summary>
        /// Options for the settings
        /// </summary>
        public SettingsOptions SettingsOptions { get; set; }

        /// <inheritdoc/>
        public DataAccessOptions(IConfiguration configuration) {
            SettingsOptions = new(configuration);
        }
    }
}

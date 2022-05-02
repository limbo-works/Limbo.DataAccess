using System.Data;

namespace Limbo.EntityFramework.Settings {
    /// <summary>
    /// Settings avalible for data access
    /// </summary>
    public class EntityFrameworkSettings {
        /// <summary>
        /// The default isolation level of generic methods
        /// </summary>
        public IsolationLevel DefaultIsolationLevel { get; set; } = IsolationLevel.Serializable;
    }
}

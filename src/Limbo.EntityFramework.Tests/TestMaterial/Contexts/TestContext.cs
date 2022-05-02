using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Limbo.EntityFramework.Tests.TestMaterial.Contexts {
    internal class TestContext : DbContext, ITestContext {
        private static readonly string _tablePrefix = "TEST";

        public TestContext(DbContextOptions<TestContext> options) : base(options) {
        }

        public DbContext Context => this;

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Prefix tables
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
                entityType.SetTableName(_tablePrefix + "_" + entityType.GetTableName());
            }
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Limbo.DataAccess.UnitOfWorks.Extensions {

    /// <inheritdoc/>
    public static class UnitOfWorkExtensions {

        /// <summary>
        /// Adds unit of work services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWorks(this IServiceCollection services) {
            services
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            return services;
        }
    }
}

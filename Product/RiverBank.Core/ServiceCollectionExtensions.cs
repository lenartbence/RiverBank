using Microsoft.Extensions.DependencyInjection;
using RiverBank.Core.Calculators.LoanCalculator;

namespace RiverBank.Core
{
    /// <summary>
    /// Class containing methods that add services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds calculator related services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        public static void AddCalculatorServices(this IServiceCollection services)
        {
            services.AddTransient<ILoanCalculator, LoanCalculator>();
        }
    }
}

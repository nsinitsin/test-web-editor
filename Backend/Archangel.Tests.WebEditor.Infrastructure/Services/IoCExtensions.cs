using Microsoft.Extensions.DependencyInjection;

namespace Archangel.Tests.WebEditor.Infrastructure.Services
{
    public static class IoCExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IWeekCompareService, WeekCompareService>();
        }
    }
}

using Archangel.Tests.WebEditor.Common.Services.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Archangel.Tests.WebEditor.Common
{
    public static class IoCExtensions
    {
        public static void AddCommonIntegrations(this IServiceCollection services)
        {
            services.AddTransient<ILoggerService, LoggerService>();
        }
    }
}

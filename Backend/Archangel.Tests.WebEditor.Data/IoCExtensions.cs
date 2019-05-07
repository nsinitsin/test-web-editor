using Archangel.Tests.WebEditor.Data.Domains;
using Archangel.Tests.WebEditor.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Archangel.Tests.WebEditor.Data
{
    public static class IoCExtensions
    {
        public static void AddDataIntegrations(this IServiceCollection services)
        {
            services.AddTransient<IAsyncRepository<News>, AsyncRepository<News>>();
        }
    }
}

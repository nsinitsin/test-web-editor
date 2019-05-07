using Archangel.Tests.WebEditor.Common;
using Archangel.Tests.WebEditor.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Archangel.Tests.WebEditor.IntegrationTests
{
    public static class DIInitializer
    {
        public static IServiceCollection Init()
        {
            IServiceCollection services = new ServiceCollection();
            var config = ConfigurationCreator.InitializeConfiguration();
            services.AddSingleton(x => config.Item1);

            services.AddSingleton<WebEditorDbContext>(_ => new FakeDbContext(config.Item2).GetFakeDbContext());
            IoCContainerFactory.MapInterfaces(services, config.Item2);

            return services;
        }
    }
}
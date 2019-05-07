using System;
using Archangel.Tests.WebEditor.Infrastructure.Commands.News;
using Archangel.Tests.WebEditor.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Archangel.Tests.WebEditor.Infrastructure
{
    public static class IoCExtensions
    {
        public static void AddInfrustructure(this IServiceCollection services)
        {
            services.AddServices();
            services.AddNewsCommands();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Common;
using Archangel.Tests.WebEditor.Data;
using Archangel.Tests.WebEditor.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Archangel.Tests.WebEditor
{
    public static class IoCContainerFactory
    {
        public static void MapInterfaces(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddCommonIntegrations();
            services.AddDataIntegrations();
            services.AddInfrustructure();
        }
    }
}

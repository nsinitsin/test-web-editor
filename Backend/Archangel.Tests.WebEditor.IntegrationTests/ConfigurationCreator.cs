using System.IO;
using Archangel.Tests.WebEditor.Common;
using Microsoft.Extensions.Configuration;

namespace Archangel.Tests.WebEditor.IntegrationTests
{
    public static class ConfigurationCreator
    {
        public static (Configuration, IConfiguration) InitializeConfiguration()
        {
            var projectDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));

            var builder = new ConfigurationBuilder()
                .SetBasePath(projectDir)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            var buildedConfig = builder.Build();
            var config = new Configuration();
            buildedConfig.Bind(config);
            return (config, buildedConfig);
        }
    }
}
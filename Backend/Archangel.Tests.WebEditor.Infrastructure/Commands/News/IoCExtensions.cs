using Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News
{
    public static class IoCExtensions
    {
        public static void AddNewsCommands(this IServiceCollection services)
        {
            services.AddTransient<IGetArchivedNewsCommand, GetArchivedNewsCommand>();
            services.AddTransient<IGetDraftNewsCommand, GetDraftNewsCommand>();
            services.AddTransient<IGetLiveNewsCommand, GetLiveNewsCommand>();
            services.AddTransient<ICreateDraftNewsCommand, CreateDraftNewsCommand>();
            services.AddTransient<IActivateDraftNewsCommand, ActivateDraftNewsCommand>();
            services.AddTransient<IUpdateActiveNewsCommand, UpdateActiveNewsCommand>();
            services.AddTransient<IUpdateDraftNewsCommand, UpdateDraftNewsCommand>();
        }
    }
}

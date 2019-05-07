using System;
using System.Linq;
using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Data.Domains;
using Archangel.Tests.WebEditor.Data.Repositories;
using Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces;
using Archangel.Tests.WebEditor.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Archangel.Tests.WebEditor.IntegrationTests
{
    public class WebEditorIntegrationTests
    {
        public WebEditorIntegrationTests()
        {
            
        }

        [Fact]
        public async Task CreateNewActiveNews_1ActiveNews()
        {
            var provider = Init();

            var createCommand = provider.GetService<ICreateDraftNewsCommand>();
            var activateCommand = provider.GetService<IActivateDraftNewsCommand>();
            var repository = provider.GetService<IAsyncRepository<News>>();

            var draftId = await createCommand.ExecuteAsync("Test");
            await activateCommand.ExecuteAsync(draftId.Result);

            var news = await repository.FindAsync(s => s is ActiveNews);

            news.Count().Should().Be(1);
        }

        [Fact]
        public async Task Create2NewActiveNews_OnlyOneActiveNewsCouldBe()
        {
            var provider = Init();

            var createCommand = provider.GetService<ICreateDraftNewsCommand>();
            var activateCommand = provider.GetService<IActivateDraftNewsCommand>();
            var repository = provider.GetService<IAsyncRepository<News>>();

            var draftId = await createCommand.ExecuteAsync("Test");
            await activateCommand.ExecuteAsync(draftId.Result);
            draftId = await createCommand.ExecuteAsync("Test2");
            var activeCommandResult = await activateCommand.ExecuteAsync(draftId.Result);
            activeCommandResult.IsSuccessful.Should().BeFalse();

            var news = await repository.FindAsync(s => s is ActiveNews);

            news.Count().Should().Be(1);
        }

        [Fact]
        public async Task Create2NewActiveNews_OnlyOneDraftNewsCouldBe()
        {
            var provider = Init();

            var createCommand = provider.GetService<ICreateDraftNewsCommand>();
            var repository = provider.GetService<IAsyncRepository<News>>();

            var text = "Test";

            var draftId = await createCommand.ExecuteAsync(text);
            

            draftId = await createCommand.ExecuteAsync("Test2");
            draftId.IsSuccessful.Should().BeFalse();

            var news = await repository.FindAsync(s => s is DraftNews);
            news.Count().Should().Be(1);

            var firstNews = news.FirstOrDefault();
            firstNews.Text.Should().Be(text);
        }

        [Fact]
        public async Task UpdateDraftNews_ValueUpdated()
        {
            var provider = Init();

            var createCommand = provider.GetService<ICreateDraftNewsCommand>();
            var updateCommand = provider.GetService<IUpdateDraftNewsCommand>();
            var repository = provider.GetService<IAsyncRepository<News>>();

            var newText = "Test2";

            var draftId = await createCommand.ExecuteAsync("text");
            await updateCommand.ExecuteAsync(draftId.Result, newText);

            var news = await repository.FindAsync(s => s is DraftNews);
            news.Count().Should().Be(1);

            var firstNews = news.FirstOrDefault();
            firstNews.Text.Should().Be(newText);
        }

        [Fact]
        public async Task UpdateActiveNews_ValueUpdated()
        {
            var provider = Init();

            var createCommand = provider.GetService<ICreateDraftNewsCommand>();
            var activateCommand = provider.GetService<IActivateDraftNewsCommand>();
            var updateCommand = provider.GetService<IUpdateActiveNewsCommand>();
            var getLiveCommand = provider.GetService<IGetLiveNewsCommand>();
            var repository = provider.GetService<IAsyncRepository<News>>();
            

            var newText = "Test2";

            var draftId = await createCommand.ExecuteAsync("text");
            await activateCommand.ExecuteAsync(draftId.Result);

            var liveNews = await getLiveCommand.ExecuteAsync();
            await updateCommand.ExecuteAsync(liveNews.Result.NewsId, newText);

            var news = await repository.FindAsync(s => s is ActiveNews);
            news.Count().Should().Be(1);

            var firstNews = news.FirstOrDefault();
            firstNews.Text.Should().Be(newText);
        }

        private ServiceProvider Init()
        {
            var serviceCollection = DIInitializer.Init();
            var provider = serviceCollection.BuildServiceProvider();
            var uow = provider.GetService<IUnitOfWork>();
            var repository = provider.GetService<IAsyncRepository<News>>();
            repository.Delete(s => s is News);
            uow.Commit();
            return provider;
        }
    }
}

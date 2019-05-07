using Archangel.Tests.WebEditor.Data.Domains;
using Archangel.Tests.WebEditor.Data.Repositories;
using Archangel.Tests.WebEditor.Infrastructure.Commands.News;
using Archangel.Tests.WebEditor.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Xunit;

namespace Archangel.Tests.WebEditor.Tests.Commands
{
    public class ActivateDraftNewsCommandTests
    {
        private IList<News> _newsInDb;
        private Mock<IAsyncRepository<News>> _newsRepositoryMock;
        private IWeekCompareService _weekCompareService;
        private ActivateDraftNewsCommand _command;

        public ActivateDraftNewsCommandTests()
        {

        }

        [Fact]
        public async void ExecuteAsync_NoDraftNews_Error()
        {
            Init();

            var needDel = _newsInDb.FirstOrDefault(s => s is DraftNews);
            _newsInDb.Remove(needDel);

            var result = await _command.ExecuteAsync(1);
            result.IsSuccessful.Should().BeFalse();
        }

        [Fact]
        public async void ExecuteAsync_AddNewNewsWhenLiveNewsInTheSameWeek_False()
        {
            Init();
            
            var result = await _command.ExecuteAsync(1);
            result.IsSuccessful.Should().BeFalse();
        }

        [Fact]
        public async void ExecuteAsync_AddNewNewsWhenLiveNewsEarlierThenCurrentWeek_True()
        {
            Init();

            var activeNews = _newsInDb.FirstOrDefault(s => s is ActiveNews);
            activeNews.CreatedOn = DateTime.UtcNow.AddDays(-10);

            var result = await _command.ExecuteAsync(1);
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public async void ExecuteAsync_OneDraftNewsAndZeroLiveNews_Added()
        {
            Init();

            var activeNews = _newsInDb.FirstOrDefault(s => s is ActiveNews);
            _newsInDb.Remove(activeNews);

            var result = await _command.ExecuteAsync(1);
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public async void ExecuteAsync_AddNewNews_OldIsGoingToArchived()
        {
            Init();

            _newsRepositoryMock.Setup(s => s.Delete(It.IsAny<ArchivedNews>()));
            _newsRepositoryMock.Setup(s => s.Add(It.IsAny<ActiveNews>()));

            var activeNews = _newsInDb.FirstOrDefault(s => s is ActiveNews);
            activeNews.CreatedOn = DateTime.UtcNow.AddDays(-10);

            var result = await _command.ExecuteAsync(1);
            result.IsSuccessful.Should().BeTrue();

            _newsRepositoryMock.Verify(s => s.Delete(It.IsAny<ActiveNews>()), Times.Once);
            _newsRepositoryMock.Verify(s => s.Add(It.IsAny<ArchivedNews>()), Times.Once);
        }

        private void Init()
        {
            _newsInDb = new List<News>
            {
                new DraftNews("draft1"){NewsId = 1},
                new ActiveNews(new DraftNews("draft2")){NewsId = 2}
            };

            _newsRepositoryMock = new Mock<IAsyncRepository<News>>();
            _newsRepositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<News, bool>>>()))
                .ReturnsAsync((Expression<Func<News, bool>> filter) =>
                {
                    return _newsInDb.FirstOrDefault(filter.Compile());
                });

            _weekCompareService = new WeekCompareService();

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            _command = new ActivateDraftNewsCommand(_newsRepositoryMock.Object, unitOfWorkMock.Object,
                _weekCompareService);
        }
    }
}

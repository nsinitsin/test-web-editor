using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Archangel.Tests.WebEditor.Data.Domains;
using Archangel.Tests.WebEditor.Data.Repositories;
using Archangel.Tests.WebEditor.Infrastructure.Commands.News;
using Archangel.Tests.WebEditor.Infrastructure.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Archangel.Tests.WebEditor.Tests.Commands
{
    public class CreateDraftNewsCommandTests
    {
        private IList<News> _newsInDb;
        private Mock<IAsyncRepository<News>> _newsRepositoryMock;
        private CreateDraftNewsCommand _command;

        [Fact]
        public async void ExecuteAsync_NoDraftNews_AddedNew()
        {
            Init();

            var needDel = _newsInDb.FirstOrDefault(s => s is DraftNews);
            _newsInDb.Remove(needDel);

            var result = await _command.ExecuteAsync("Text");
            result.IsSuccessful.Should().BeTrue();
            _newsRepositoryMock.Verify(s => s.Add(It.IsAny<DraftNews>()), Times.Once);
        }

        [Fact]
        public async void ExecuteAsync_DraftNewsExistInDb_Error()
        {
            Init();
            
            var result = await _command.ExecuteAsync("Text");
            result.IsSuccessful.Should().BeFalse();
            _newsRepositoryMock.Verify(s => s.Add(It.IsAny<DraftNews>()), Times.Never);
        }

        private void Init()
        {
            _newsInDb = new List<News>
            {
                new DraftNews("draft1"){NewsId = 1}
            };

            _newsRepositoryMock = new Mock<IAsyncRepository<News>>();
            _newsRepositoryMock.Setup(s => s.FindAsync(It.IsAny<Expression<Func<News, bool>>>()))
                .ReturnsAsync((Expression<Func<News, bool>> filter) =>
                {
                    return _newsInDb.Where(filter.Compile()).AsQueryable();
                });
            
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            _command = new CreateDraftNewsCommand(_newsRepositoryMock.Object, unitOfWorkMock.Object);
        }
    }
}
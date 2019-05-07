using System;
using System.Net;
using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Data.Domains;
using Archangel.Tests.WebEditor.Data.Repositories;
using Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces;
using Archangel.Tests.WebEditor.Infrastructure.Services;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News
{
    public class UpdateActiveNewsCommand : IUpdateActiveNewsCommand
    {
        private readonly IAsyncRepository<Data.Domains.News> _newsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateActiveNewsCommand(IAsyncRepository<Data.Domains.News> newsRepository, IUnitOfWork unitOfWork)
        {
            _newsRepository = newsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult<long>> ExecuteAsync(long activeNewsId, string newtext)
        {
            var news = await _newsRepository.FindOneAsync(s => s is ActiveNews && s.NewsId == activeNewsId);
            if (news == null)
                return new CommandResult<long>($"System doesn't have any active news with id {activeNewsId}.", HttpStatusCode.NotFound);

            var activeNews = news as ActiveNews;
            try
            {
                activeNews.UpdateText(newtext);
                await _unitOfWork.CommitAsync();
                return new CommandResult<long>(activeNews.NewsId);
            }
            catch (Exception e)
            {
                return new CommandResult<long>(false, e.Message);
            }
        }
    }
}
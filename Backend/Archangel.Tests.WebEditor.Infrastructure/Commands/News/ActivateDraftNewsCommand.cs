using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Data.Domains;
using Archangel.Tests.WebEditor.Data.Repositories;
using Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces;
using Archangel.Tests.WebEditor.Infrastructure.Services;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News
{
    public class ActivateDraftNewsCommand : IActivateDraftNewsCommand
    {
        private readonly IAsyncRepository<Data.Domains.News> _newsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWeekCompareService _weekCompareService;

        public ActivateDraftNewsCommand(IAsyncRepository<Data.Domains.News> newsRepository,
            IUnitOfWork unitOfWork,
            IWeekCompareService weekCompareService)
        {
            _newsRepository = newsRepository;
            _unitOfWork = unitOfWork;
            _weekCompareService = weekCompareService;
        }

        public async Task<ICommandResult<bool>> ExecuteAsync(long draftId)
        {
            var singleDraftNews = await _newsRepository.FindOneAsync(s=>s is DraftNews && s.NewsId == draftId);
            if (singleDraftNews == null)
                return new CommandResult<bool>("System doesn't have any draft news for actiovation", HttpStatusCode.NotFound);
            
            var activeNews = await _newsRepository.FindOneAsync(s=>s is ActiveNews);
//            if (activeNews != null && _weekCompareService.Compare(DateTime.UtcNow, activeNews.CreatedOn) == 0)
//            {
//                return new CommandResult<bool>(false, "Week is not ended for updating news");
//            }

            try
            {
                var newNews = new ActiveNews(singleDraftNews as DraftNews);
                _newsRepository.Add(newNews);
                _newsRepository.Delete(singleDraftNews);
                if (activeNews != null)
                {
                    _newsRepository.Add(new ArchivedNews(activeNews));
                    _newsRepository.Delete(activeNews);
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                return new CommandResult<bool>(false, e.Message);
            }

            return new CommandResult<bool>(true);
        }
    }
}
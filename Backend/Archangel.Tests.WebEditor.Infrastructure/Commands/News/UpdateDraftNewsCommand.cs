using System;
using System.Net;
using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Data.Domains;
using Archangel.Tests.WebEditor.Data.Repositories;
using Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces;
using Archangel.Tests.WebEditor.Infrastructure.Services;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News
{
    public class UpdateDraftNewsCommand : IUpdateDraftNewsCommand
    {
        private readonly IAsyncRepository<Data.Domains.News> _draftNewsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDraftNewsCommand(IAsyncRepository<Data.Domains.News> draftNewsRepository, IUnitOfWork unitOfWork)
        {
            _draftNewsRepository = draftNewsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult<long>> ExecuteAsync(long draftId, string newtext)
        {
            var draftNews = await _draftNewsRepository.FindOneAsync(s => s is DraftNews && s.NewsId == draftId);
            if (draftNews == null)
                return new CommandResult<long>($"System doesn't have any draft news with id {draftId}.", HttpStatusCode.NotFound);

            var draft = draftNews as DraftNews;
            try
            {
                draft.UpdateText(newtext);
                await _unitOfWork.CommitAsync();
                return new CommandResult<long>(draft.NewsId);
            }
            catch (Exception e)
            {
                return new CommandResult<long>(false, e.Message);
            }

            
        }
    }
}
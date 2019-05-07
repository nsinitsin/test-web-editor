using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Data.Domains;
using Archangel.Tests.WebEditor.Data.Repositories;
using Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces;
using Archangel.Tests.WebEditor.Infrastructure.Services;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News
{
    public class CreateDraftNewsCommand : ICreateDraftNewsCommand
    {
        private readonly IAsyncRepository<Data.Domains.News> _draftNewsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDraftNewsCommand(IAsyncRepository<Data.Domains.News> draftNewsRepository, IUnitOfWork unitOfWork)
        {
            _draftNewsRepository = draftNewsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult<long>> ExecuteAsync(string inputModelText)
        {
            var draftNews = (await _draftNewsRepository.FindAsync(s => s is DraftNews)).ToList();
            if (draftNews.Count > 1)
                return new CommandResult<long>("In database more then one draft", HttpStatusCode.InternalServerError);

            var singleDraftNews = draftNews.FirstOrDefault();
            if (singleDraftNews != null)
                return new CommandResult<long>("System already has draft news. You need to activate old draft before create new.", HttpStatusCode.Found);

            try
            {
                var draft = new DraftNews(inputModelText);
                _draftNewsRepository.Add(draft);

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
using System.Net;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands
{
    public interface ICommandResult<T> : IExecuteResult
    {
        T Result { get; }
        HttpStatusCode HttpStatusCode { get; }
    }

    public interface IExecuteResult
    {
        bool IsSuccessful { get; }
        string ReasonPhrase { get; }
    }
}
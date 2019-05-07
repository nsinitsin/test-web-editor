using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands
{
    public class CommandResult<T> : ICommandResult<T>
    {
        public T Result { get; private set; }
        public bool IsSuccessful { get; private set; }
        public string ReasonPhrase { get; private set; }

        public HttpStatusCode HttpStatusCode { get; }

        public CommandResult(T terms)
        {
            Result = terms;
            IsSuccessful = true;
            HttpStatusCode = HttpStatusCode.OK;
        }

        public CommandResult(bool isSuccessful, string reasonPhrase)
        {
            if (!isSuccessful && string.IsNullOrWhiteSpace(reasonPhrase))
                throw new Exception("If command failed you must give a reasonPhrase");

            IsSuccessful = isSuccessful;
            ReasonPhrase = reasonPhrase;
            HttpStatusCode = HttpStatusCode.BadRequest;
        }

        public CommandResult(string reasonPhrase, HttpStatusCode httpStatusCode)
        {
            if (string.IsNullOrWhiteSpace(reasonPhrase))
                throw new Exception("If command failed you must give a reasonPhrase");

            IsSuccessful = false;
            ReasonPhrase = reasonPhrase;
            HttpStatusCode = httpStatusCode;
        }
    }
}

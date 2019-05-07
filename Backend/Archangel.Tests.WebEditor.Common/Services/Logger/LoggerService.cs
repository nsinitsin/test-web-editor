using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;

namespace Archangel.Tests.WebEditor.Common.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        private readonly TelemetryClient _aiClient;
        public string OperationId { get => _aiClient.Context.Operation.Id; set => _aiClient.Context.Operation.Id = value; }
        public string ParentId { get => _aiClient.Context.Operation.ParentId; set => _aiClient.Context.Operation.ParentId = value; }

        public LoggerService(Configuration configuration)
        {
            var aiKey = configuration.InstrumentationKey;
            if (string.IsNullOrWhiteSpace(aiKey)) throw new NullReferenceException(aiKey);

            _aiClient = new TelemetryClient()
            {
                InstrumentationKey = aiKey
            };

            OperationId = System.Diagnostics.Activity.Current?.RootId;
            ParentId = System.Diagnostics.Activity.Current?.Id;
        }

        public void LogEvent(string name, Dictionary<string, string> props = null)
        {
            _aiClient.TrackEvent(name, props.ToAiProp());
        }

        public void LogTrace(string message, SeverityLevel level, IDictionary<string, string> properties = null)
        {
            _aiClient.TrackTrace(message, (Microsoft.ApplicationInsights.DataContracts.SeverityLevel)level, properties.ToAiProp());
        }

        public void LogException(Exception ex, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            _aiClient.TrackException(ex, properties.ToAiProp(), metrics);
        }
    }
}
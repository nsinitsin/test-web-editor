using System.Collections.Generic;

namespace Archangel.Tests.WebEditor.Common.Services.Logger
{
    public static class AiPropertyExtension
    {
        public static IDictionary<string, string> ToAiProp(this IDictionary<string, string> props)
        {
            var copy = props != null ? new Dictionary<string, string>(props) : new Dictionary<string, string>();
            if (copy.ContainsKey("application"))
                copy["application"] = "webEditor";
            else
                copy.Add("application", "webEditor");

            return copy;
        }
    }
}
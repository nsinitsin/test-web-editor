namespace Archangel.Tests.WebEditor.Common
{
    public class Configuration
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public string InstrumentationKey { get; set; }
    }

    public class ConnectionStrings
    {
        public string WebEditorContext { get; set; }
    }
}

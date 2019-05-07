using System;

namespace Archangel.Tests.WebEditor.Data.Domains
{
    public class DraftNews : UpdatableNews
    {
        internal DraftNews() { }

        public DraftNews(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException(nameof(text));

            CreatedOn = DateTime.UtcNow;
            Text = text;
        }
    }
}
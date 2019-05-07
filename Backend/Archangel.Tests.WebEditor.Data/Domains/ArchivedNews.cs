using System;

namespace Archangel.Tests.WebEditor.Data.Domains
{
    public class ArchivedNews : News
    {
        internal ArchivedNews() {}

        public ArchivedNews(News news)
        {
            CreatedOn = news.CreatedOn;
            Text = news.Text;
            ArchivedOn = DateTime.UtcNow;
        }

        public DateTime ArchivedOn { get; internal set; }
    }
}
using System;

namespace Archangel.Tests.WebEditor.Controllers.News.ViewModels
{
    public class NewsViewModel
    {
        public NewsViewModel(Data.Domains.News news)
        {
            NewsId = news.NewsId;
            CreatedOn = news.CreatedOn;
            Text = news.Text;
        }

        public long NewsId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Text { get; set; }
    }
}
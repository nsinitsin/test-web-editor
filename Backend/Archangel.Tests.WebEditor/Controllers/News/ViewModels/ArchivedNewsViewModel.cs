using System;
using Archangel.Tests.WebEditor.Data.Domains;

namespace Archangel.Tests.WebEditor.Controllers.News.ViewModels
{
    public class ArchivedNewsViewModel : NewsViewModel
    {
        public ArchivedNewsViewModel(ArchivedNews news) : base(news)
        {
            ArchivedOn = news.ArchivedOn;
        }

        public DateTime ArchivedOn { get; set; }
    }
}
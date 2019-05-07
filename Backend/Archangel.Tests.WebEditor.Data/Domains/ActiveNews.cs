namespace Archangel.Tests.WebEditor.Data.Domains
{
    public class ActiveNews : UpdatableNews
    {
        internal ActiveNews() { }

        public ActiveNews(DraftNews draftNews)
        {
            CreatedOn = draftNews.CreatedOn;
            Text = draftNews.Text;
        }
    }
}
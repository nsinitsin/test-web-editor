using System;

namespace Archangel.Tests.WebEditor.Data.Domains
{
    public class UpdatableNews : News
    {
        public void UpdateText(string newText)
        {
            if (string.IsNullOrWhiteSpace(newText))
                throw new ArgumentException($"{nameof(newText)} should have some value.");

            Text = newText;
            UpdateOn = DateTime.UtcNow;
        }
    }
}
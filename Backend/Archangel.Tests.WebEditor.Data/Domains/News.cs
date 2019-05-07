using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Archangel.Tests.WebEditor.Tests")]
namespace Archangel.Tests.WebEditor.Data.Domains
{
    public class News
    {
        protected internal News() { }

        public int NewsId { get; internal set; }
        public DateTime CreatedOn { get; protected internal set; }
        public DateTime? UpdateOn { get; protected set; }
        public string Text { get; protected set; }
    }
}

using System;
using Archangel.Tests.WebEditor.Data.Domains;
using FluentAssertions;
using Xunit;

namespace Archangel.Tests.WebEditor.Tests.Domains
{
    public class DraftDomainTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Const_InvalidData_ThrownException(string inputData)
        {
            Action act = () => new DraftNews(inputData);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Const_ValidText_DraftNewsCreated()
        {
            var text = "Some text";
            Func<DraftNews> func = () => new DraftNews(text);

            func.Should().NotThrow();

            var draftNews = func();
            draftNews.Text.Should().Be(text);
        }
    }
}
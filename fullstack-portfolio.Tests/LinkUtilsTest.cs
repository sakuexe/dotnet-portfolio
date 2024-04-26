using fullstack_portfolio.Utils;
using Xunit;

namespace fullstack_portfolio.Tests;

public class LinkUtilsTest
{
    [Theory]
    [InlineData("https://www.linkedin.com/in/username", "www.linkedin.com")]
    [InlineData("http://mycool.site.saku.dev/", "mycool.site.saku.dev")]
    [InlineData("http://www.twitter.com/username", "www.twitter.com")]
    [InlineData("https://user.www.instagram.com", "user.www.instagram.com")]
    public void TestGetDomain(string link, string expectedDomain)
    {
        // Arrange

        // Act
        string result = LinkUtils.GetDomain(link);

        // Assert
        Assert.Equal(expectedDomain, result);
    }

    [Theory]
    [InlineData("https://www.linkedin.com/in/username", "linkedin")]
    [InlineData("http://www.twitter.com/username", "twitter")]
    // test if the function can handle multiple subdomains and http
    [InlineData("http://mycool.site.saku.dev/", "saku")]
    // test if the function can handle no trailing slash
    [InlineData("https://user.www.instagram.com", "instagram")]
    public void TestGetBrand(string link, string expectedBrand)
    {
        // Arrange

        // Act
        string result = LinkUtils.GetBrand(link);

        // Assert
        Assert.Equal(expectedBrand, result);
    }
}

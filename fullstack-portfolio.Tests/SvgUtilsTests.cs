using fullstack_portfolio.Utils;
using Xunit;

namespace fullstack_portfolio.Tests;

public class SvgUtilsTests
{
    [Theory]
    [InlineData("https://www.linkedin.com/in/username", "wwwroot/icons/linkedin.svg")]
    [InlineData("https://github.com/username", "wwwroot/icons/github.svg")]
    [InlineData("https://www.twitter.com/username", "wwwroot/icons/twitter.svg")]
    [InlineData("https://user.www.instagram.com/username", "wwwroot/icons/instagram.svg")]
    public void TestGetSvgPath(string brandName, string expectedPath)
    {
        // Arrange

        // Act
        string result = SvgUtils.GetSvgPath(brandName);

        // Assert
        Assert.Equal(expectedPath, result);
    }

    [Fact] // no inline styles, since it was really unreadable with the long values
    public void TestRemoveFill()
    {
        // Arrange
        // with hashtag in the fill property
        string svgCode = "<svg fill=\"#000000\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\"/></svg>";
        string expectedCode = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\"/></svg>";
        // without hashtag in the fill property, fill in the path
        string svgCode1 = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\" fill=\"224488\"/></svg>";
        string expectedCode1 = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\" /></svg>";
        // both fill and fill in the path
        string svgCode2 = "<svg fill=\"#000000\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\" fill=\"224488\"/></svg>";
        string expectedCode2 = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\" /></svg>";
        // both, but first fill is written as "none"
        string svgCode3 = "<svg fill=\"none\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\" fill=\"224488\"/></svg>";
        string expectedCode3 = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\" /></svg>";

        // Act
        string result = SvgUtils.RemoveFill(svgCode);
        string result1 = SvgUtils.RemoveFill(svgCode1);
        string result2 = SvgUtils.RemoveFill(svgCode2);
        string result3 = SvgUtils.RemoveFill(svgCode3);

        // Assert
        Assert.Equal(expectedCode, result);
        Assert.Equal(expectedCode1, result1);
        Assert.Equal(expectedCode2, result2);
        Assert.Equal(expectedCode3, result3);
    }

    [Fact]
    public void TestAddFullWidthHeight()
    {
        // Arrange
        string svgCode = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" width=\"20\" height=\"43\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\"/></svg>";
        string expectedCode = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" width=\"100%\" height=\"100%\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\"/></svg>";
        // the width and height are already 100%
        string svgCode1 = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" width=\"100%\" height=\"100%\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\"/></svg>";
        string expectedCode1 = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" width=\"100%\" height=\"100%\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\"/></svg>";
        // the width and height are defined earlier
        string svgCode2 = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"222\" height=\"80%\" viewBox=\"0 0 24 24\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\"/></svg>";
        string expectedCode2 = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"100%\" height=\"100%\" viewBox=\"0 0 24 24\"><path d=\"M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z\"/></svg>";

        // Act
        string result = SvgUtils.AddFullWidthHeight(svgCode);
        string result1 = SvgUtils.AddFullWidthHeight(svgCode1);
        string result2 = SvgUtils.AddFullWidthHeight(svgCode2);

        // Assert
        Assert.Equal(expectedCode, result);
        Assert.Equal(expectedCode1, result1);
        Assert.Equal(expectedCode2, result2);
    }
}

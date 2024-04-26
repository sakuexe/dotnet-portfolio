using System.Text.RegularExpressions;

namespace fullstack_portfolio.Utils;

public static class LinkUtils
{
    public static string GetDomain(string link)
    {
        // first group is http:// or https://
        // second group is everything until the first /
        // not including it in the group
        string pattern = @"(^https?://)([^/]*)";
        Regex rg = new Regex(pattern);

        // get the second group of regex the domain
        // groups[0] - all, groups[1] - first match group
        return rg.Match(link).Groups[2].Value;
    }

    public static string GetBrand(string link)
    {
        string domain = GetDomain(link);
        string brand = domain.Split(".")[^2];
        return brand.ToLower();
    }
}

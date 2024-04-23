namespace fullstack_portfolio.Utils;

public static class ViewUtils
{
  public static string TrimPath(string fullpath)
  {
    if (!string.IsNullOrEmpty(fullpath) && fullpath.Contains("wwwroot"))
    {
      fullpath = fullpath.Split("wwwroot")[1];
    }
    return fullpath;
  }

}

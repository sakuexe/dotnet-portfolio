using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace fullstack_portfolio.Utils;

public static class ImageManipulator
{
    private static string DefaultImagePath = Path.Combine("wwwroot", "uploads");

    public static async Task<string?> GenerateThumbnail(IFormFile imgFile, int width)
    {
        try
        {
            using Image img = await Image.LoadAsync(imgFile.OpenReadStream());
            img.Mutate(x => x.Resize(width, 0));
            var thumbnailPath = GeneratePath(imgFile.FileName, "webp", "thumbnails");
            img.SaveAsWebp(thumbnailPath);
            return thumbnailPath;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error generating thumbnail: \n{e.Message}");
            return null;
        }
    }

    public static async Task<string?> SaveImage(IFormFile imgFile, int width)
    {
        string? generatedPath = GeneratePath(imgFile.FileName);
        if (generatedPath == null)
            throw new Exception("Generated path was null");
        using Image img = await Image.LoadAsync(imgFile.OpenReadStream());
        try
        {
            img.Mutate(x => x.Resize(width, 0));
            Task saveAsync = img.SaveAsWebpAsync(generatedPath);
        }
        catch
        {
            // if the file does not support resizing, like .svg
            // save the original file as is
            img.Save(generatedPath);
        }
        return generatedPath;
    }

    public static string? GeneratePath(string fileName, string suffix = "webp", string? path = null)
    {
        string generatedPath;
        string newFilename = Path.GetFileNameWithoutExtension(fileName) + "." + suffix;
        if (path == null)
            generatedPath = Path.Combine(DefaultImagePath, newFilename);
        else
            generatedPath = Path.Combine(DefaultImagePath, path, newFilename);

        // make sure that the directory exists
        string? directory = Path.GetDirectoryName(generatedPath);
        if (string.IsNullOrWhiteSpace(directory))
            throw new Exception($"No directory could be parsed from: {generatedPath}");
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        return generatedPath;
    }
}

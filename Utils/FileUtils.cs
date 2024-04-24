using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace fullstack_portfolio.Utils;

public static class FileUtils
{
    public static string DefaultPath = Path.Combine("wwwroot", "uploads");

    // Saves a file to disk and return the path if successful
    // on fail returns null
    public static async Task<string?> SaveFile(IFormFile file, string? path = null)
    {
        if (file == null)
            return null;
        if (path == null)
            path = Path.Combine(DefaultPath, file.FileName);

        // check if path exists and create it if it doesn't
        var directory = Path.GetDirectoryName(path);
        if (directory == null)
        {
            Debug.WriteLine($"No directory found for {path}");
            return null;
        }
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);
        return path;
    }

    // used for cleaning up unneeded files
    public static void DeleteFile(string path)
    {
        // get the root directory of the project
        var rootDirectory = Directory.GetCurrentDirectory();
        if (rootDirectory == null)
            throw new Exception("Root directory of the project was not found. WTF?");

        path = Path.Combine(rootDirectory, path);

        if (File.Exists(path))
            File.Delete(path);
    }

    // Resizes an image to the given width and height
    // returns true if successful, false if not
    // to maintain the aspect ratio pass only the width
    // you can also pass the height
    // more info here:
    // https://docs.sixlabors.com/articles/imagesharp/resize.html
    public static string? ResizeImage(string path, int width, int height = 0)
    {
        try {
            using Image img = Image.Load(path);
            img.Mutate(x => x.Resize(width, height));
            img.Save(path);
        } catch (Exception e) {
            Debug.WriteLine($"Error resizing image: {e.Message}");
            return e.Message;
        }
        return null;
    }
}

using System.ComponentModel.DataAnnotations;
using System.Reflection;
using fullstack_portfolio.Data;

namespace fullstack_portfolio.Utils;

public static class EditUtils
{
    public static bool IsFile(PropertyInfo property)
    {
        var dataTypeAttribute = property.GetCustomAttribute<DataTypeAttribute>();
        if (dataTypeAttribute == null)
            return false;
        bool isFile = dataTypeAttribute.DataType == DataType.ImageUrl;
        return isFile;
    }

    public static string JoinStringList(PropertyInfo property, IMongoModel model)
    {
        var list = property.GetValue(model) as List<string>;
        return string.Join(", ", list ?? new List<string>());
    }

    public static string TrimPath(PropertyInfo property, IMongoModel model)
    {
        var path = property.GetValue(model)?.ToString();
        if (!string.IsNullOrEmpty(path) && path.Contains("wwwroot"))
        {
            path = path.Split("wwwroot")[1];
        }
        return path ?? "";
    }

    public static bool IsTextArea(PropertyInfo property)
    {
        var dataTypeAttribute = property.GetCustomAttribute<DataTypeAttribute>();
        if (dataTypeAttribute == null)
            return false;
        return dataTypeAttribute.DataType == DataType.MultilineText;
    }

    public static bool IsPassword(PropertyInfo property)
    {
        var dataTypeAttribute = property.GetCustomAttribute<DataTypeAttribute>();
        if (dataTypeAttribute == null)
            return false;
        return dataTypeAttribute.DataType == DataType.Password;
    }
}

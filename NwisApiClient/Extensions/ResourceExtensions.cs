using System.Reflection;

namespace NwisApiClient.Extensions;

public static class ResourceExtensions
{
    public static async Task<Stream> GetResourceStream(this Assembly assembly, string fileName)
    {
        var resourcePath = fileName;
        // Format: "{Namespace}.{Folder}.{filename}.{Extension}"
        if (!fileName.StartsWith(nameof(NwisApi)))
        {
            resourcePath = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(fileName));
        }

        return assembly.GetManifestResourceStream(resourcePath)!;
    }
}
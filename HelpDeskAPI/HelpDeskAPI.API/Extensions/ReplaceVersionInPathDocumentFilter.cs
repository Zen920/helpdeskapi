using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HelpDeskAPI.Api.Extensions;

public class ReplaceVersionInPathDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument document, DocumentFilterContext context)
    {
        var paths = new OpenApiPaths();
        foreach (var (key, value) in document.Paths)
        {
            paths[key.Replace("{version}", document.Info.Version)] = value;
        }
        document.Paths = paths;
    }
}

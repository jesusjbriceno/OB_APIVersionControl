using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace APIVersionControl
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "My .Net Api restful",
                Version = description.ApiVersion.ToString(),
                Description = $"This is my first API versioning control.",
                Contact = new OpenApiContact()
                {
                    Email = "jesusj.briceno@gmail.com",
                    Name = "Jesús"
                }
            };

            if(description.IsDeprecated)
            {
                info.Description += "This version is deprecated";
            }

            return info;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // Add Swagger Documentation for each API version we have
            foreach(var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options) => Configure(options);
    }
}

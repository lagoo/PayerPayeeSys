using Application.Authentications.Query.GetAuthentication;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace WebAPI.Common.Filters
{
    public class AuthenticationSwaggerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var schemas = context.SchemaRepository.Schemas.Where(e => e.Key == nameof(GetAuthenticationQuery))
                                                          .ToList();

            schemas.ForEach(x => { context.SchemaRepository.Schemas.Remove(x.Key); });


            var nonMobileRoutes = swaggerDoc.Paths.Where(x => x.Key == "/auth")
                                                  .ToList();

            nonMobileRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
        }
    }
}

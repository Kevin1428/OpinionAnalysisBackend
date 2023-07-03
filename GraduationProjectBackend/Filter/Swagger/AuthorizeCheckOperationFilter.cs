using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GraduationProjectBackend.Filter.Swagger
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        private readonly EndpointDataSource _endpointDataSource;

        public AuthorizeCheckOperationFilter(EndpointDataSource endpointDataSource)
        {
            _endpointDataSource = endpointDataSource;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var descriptor = _endpointDataSource.Endpoints.FirstOrDefault(x =>
                x.Metadata.GetMetadata<ControllerActionDescriptor>() == context.ApiDescription.ActionDescriptor);

            var authorize = descriptor.Metadata.GetMetadata<AuthorizeAttribute>() != null;

            var allowAnonymous = descriptor.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;

            if (!authorize || allowAnonymous)
            {
                return;
            }

            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }
                ] = new List<string>()
            });
        }
    }
}

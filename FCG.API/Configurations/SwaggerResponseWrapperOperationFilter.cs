using FCG.Application.Wrappers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FCG.API.Configurations
{
    public class SwaggerResponseWrapperOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Responses.Clear();

            var schema = context.SchemaGenerator.GenerateSchema(
                typeof(ResponseWrapper<object>),
                context.SchemaRepository);

            operation.Responses.Add("200", new OpenApiResponse
            {
                Description = "Requisição bem sucedida.",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType { Schema = schema }
                }
            });

            operation.Responses.Add("201", new OpenApiResponse
            {
                Description = "Recurso criado com sucesso.",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType { Schema = schema }
                }
            });

            operation.Responses.Add("400", new OpenApiResponse
            {
                Description = "Erro na requisição ou regra de negócio.",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType { Schema = schema }
                }
            });

            operation.Responses.Add("401", new OpenApiResponse
            {
                Description = "Não autorizado.",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType { Schema = schema }
                }
            });

            operation.Responses.Add("404", new OpenApiResponse
            {
                Description = "Recurso não encontrado.",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType { Schema = schema }
                }
            });

            operation.Responses.Add("500", new OpenApiResponse
            {
                Description = "Erro interno no servidor.",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType { Schema = schema }
                }
            });
        }
    }
}

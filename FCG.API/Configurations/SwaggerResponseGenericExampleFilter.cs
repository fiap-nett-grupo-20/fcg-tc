using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FCG.API.Configurations
{
    public class SwaggerResponseGenericExampleFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var response in operation.Responses)
            {
                var example = response.Key switch
                {
                    "200" or "201" => GenerateSuccessExample(response.Key),
                    "400" => GenerateErrorExample(400, "Erro de regra de negócio ou requisição inválida."),
                    "401" => GenerateErrorExample(401, "Recurso não autorizado."),
                    "404" => GenerateErrorExample(404, "Recurso não encontrado."),
                    "500" => GenerateErrorExample(500, "Erro interno no servidor."),
                    _ => null
                };

                if (example != null)
                {
                    if (response.Value.Content.ContainsKey("application/json"))
                    {
                        response.Value.Content["application/json"].Examples = new Dictionary<string, OpenApiExample>
                    {
                        { "Example", example }
                    };
                    }
                }
            }
        }

        private OpenApiExample GenerateSuccessExample(string statusCode)
        {
            return new OpenApiExample
            {
                Summary = "Resposta de sucesso",
                Value = new OpenApiObject
                {
                    ["success"] = new OpenApiBoolean(true),
                    ["statusCode"] = new OpenApiInteger(Convert.ToInt32(statusCode)),
                    ["message"] = new OpenApiString("Operação realizada com sucesso."),
                    ["data"] = new OpenApiObject()
                }
            };
        }

        private OpenApiExample GenerateErrorExample(int statusCode, string message)
        {
            return new OpenApiExample
            {
                Summary = $"Erro {statusCode}",
                Value = new OpenApiObject
                {
                    ["success"] = new OpenApiBoolean(false),
                    ["statusCode"] = new OpenApiInteger(statusCode),
                    ["message"] = new OpenApiString(message),
                    ["data"] = new OpenApiNull()
                }
            };
        }
    }
}

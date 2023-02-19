using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace AzureFunctionConversaoTemperatura
{
    public class FunctionFahrenheitParaCelsius
    {
        private readonly ILogger<FunctionFahrenheitParaCelsius> _logger;

        public FunctionFahrenheitParaCelsius(ILogger<FunctionFahrenheitParaCelsius> log)
        {
            _logger = log;
        }

        [FunctionName("ConverterFahrenheitParaCelsius")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Convers�o" })]
        [OpenApiParameter(name: "fahrenheit", In = ParameterLocation.Path, Required = true, Type = typeof(double), Description = "O valor em **Fahrenheit** para convers�o em Celsius")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Retorna o valor em Celsius")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "ConverterFahrenheitParaCelsius/{fahrenheit}")] HttpRequest req,
            double fahrenheit)
        {
            _logger.LogInformation($"Par�metro recebido: {fahrenheit}", fahrenheit);

            var valorEmCelsius = (fahrenheit - 32) * 5 / 9;

            string mensagemResposta = $"O valor em Fahrenheit {fahrenheit} em Celsius �: {valorEmCelsius}";

            _logger.LogInformation($"Convers�o efetuada. Resultado: {valorEmCelsius}");

            return new OkObjectResult(mensagemResposta);
        }
    }
}


using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IgniteCSharpChallenge2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        // Usando List<string> para permitir modificação
        private List<string> Summaries = new List<string>
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            return Ok(new
            {
                sumaries = Summaries,
                sumariesCount = Summaries.Count
            });
        }

        // Esta rota agora pode adicionar um resumo à lista
        [HttpPost("sumerie")]
        public IActionResult Post([FromBody] string summary) // Esperando o resumo no corpo da requisição
        {
            if (!string.IsNullOrWhiteSpace(summary))
            {
                Summaries.Add(summary); // Adiciona o novo resumo à lista
                return Ok(new { Message = "Summary added successfully." });
            }
            return BadRequest("Summary cannot be empty.");
        }
    }
}

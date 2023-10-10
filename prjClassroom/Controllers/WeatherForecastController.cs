using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using prjClassroom.Models;

namespace prjClassroom.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost]
    [Route("/delete")]
    [Authorize]
    public dynamic deleteWeatherForecast(WeatherForecast weatherForecast)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        var rToken = JwtEntity.TokenValidate(identity);

        if(!rToken.success)
        {
            return rToken;
        }


        UserEntity user = rToken.result;

        if(user.Role != "Admin")
        {
            {
                return new
                {
                    message="You don't have permission for this task"
                };
            }
        }

        return new
        {
            message = "Deleted Weather Forecast"
        };
    }
}


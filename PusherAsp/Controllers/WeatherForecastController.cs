using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PusherServer;
using System.Net;

namespace PusherAsp.Controllers
{
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

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        [Route("HelloWorld")]
        public async Task<ActionResult> HelloWorld()
        {
            var options = new PusherOptions
            {
                Cluster = "us2",
                Encrypted = true
            };

            var pusher = new Pusher(
              "959730",
              "04016b8df0172af7d6fd",
              "0040d7c958bc43bc307a",
              options);

            var result = await pusher.TriggerAsync(
              "my-channel",
              "my-event",
              new { message = "hello world" });

            return new OkResult();
        }

        public Task<ActionResult> Login()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> ChatRoom()
        {
            throw new NotImplementedException();
        }
    }

    public class MyClass
    {

    }
}

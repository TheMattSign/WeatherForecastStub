using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TestWebApplication.Models;
using TestWebApplication.Services;
using TestWebApplication.Utils;

namespace TestWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : Controller
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastService _weatherForecastService;
        private readonly IConfiguration _configuration;
        private readonly EncryptionUtils _encryptionUtils;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
            WeatherForecastService weatherForecastService,
            IConfiguration configuration,
            EncryptionUtils encryptionUtils)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
            _configuration = configuration;
            _encryptionUtils = encryptionUtils;
        }

        public IActionResult Index()
        {
            return View(GetList());
        }

        [HttpGet("decyrpt")]
        public String decrypt(String textToDecrypt)
        {
            return _encryptionUtils.decrypt(textToDecrypt);
        }


        [HttpGet("encrypt")]
        public String encrypt(String someText)
        {
            return _encryptionUtils.encrypt(someText);
        }

        [HttpGet("roundtrip")]
        public String roundTrip(String someText)
        {
            String encrypted = _encryptionUtils.encrypt(someText);
            return _encryptionUtils.decrypt(encrypted);
        }

        [HttpGet("List")]
        public IEnumerable<WeatherForecast> GetList()
        {
            return _weatherForecastService.Get();
        }

        [HttpGet("Test/{id}")]
        public string Test(Guid id)
        {
            return _weatherForecastService.Test(id);
        }

        [HttpGet("{id}")]
        public WeatherForecast GetById(Guid id)
        {
            return _weatherForecastService.GetById(id);
        }

        [HttpPost]
        public ActionResult<WeatherForecast> Post(WeatherForecast weatherForecast)
        {
            if (weatherForecast != null)
            {
                weatherForecast = _weatherForecastService.Add(weatherForecast);
                return CreatedAtAction(nameof(GetById), new { weatherForecast.id }, weatherForecast);
            }

            return null;
        }

        [HttpPut("{id}")]
        public void Put(Guid id, WeatherForecast weatherForecast)
        {
            _weatherForecastService.Update(id, weatherForecast);
        }
    }
}

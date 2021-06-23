using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebApplication.Entities
{
    public class WeatherForecastEntity
    {

        public WeatherForecastEntity()
        {

        }

        public Guid ID { get; set; }

        public DateTime Date { get; set; }

        [Column("temperature_celsius")]
        public Decimal TemperatureCelsius { get; set; }

        public string Summary { get; set; }

        public string secureData { get; set; }
    }
}

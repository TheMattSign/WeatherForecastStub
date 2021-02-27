using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApplication.Entities
{
    public class WeatherForecastEntity
    {
        public Guid ID { get; set; }

        public DateTime Date { get; set; }

        [Column("temperature_celsius")]
        public Decimal TemperatureCelsius { get; set; }

        public string Summary { get; set; }
    }
}

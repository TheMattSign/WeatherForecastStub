using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TestWebApplication.Entities;
using TestWebApplication.Models;

namespace TestWebApplication.Services
{
    public class WeatherForecastService
    {
        private readonly WeatherDbContext _dbContext;
        private Mapper entityToModelMapper;
        private Mapper modelToEntityMapper;

        public WeatherForecastService(WeatherDbContext dbContext)
        {
            _dbContext = dbContext;

            entityToModelMapper = new Mapper(new MapperConfiguration(cfg =>
                    cfg.CreateMap<WeatherForecastEntity, WeatherForecast>().ForMember(dest => dest.TemperatureC, act => act.MapFrom(src => src.TemperatureCelsius))
                ));

            modelToEntityMapper = new Mapper(new MapperConfiguration(cfg =>
                    cfg.CreateMap<WeatherForecast, WeatherForecastEntity>().ForMember(dest => dest.TemperatureCelsius, act => act.MapFrom(src => src.TemperatureC))
            ));
        }

        public WeatherForecast Add(WeatherForecast weatherForecast)
        {
            var weatherForecastEntity = modelToEntityMapper.Map<WeatherForecastEntity>(weatherForecast);
            weatherForecastEntity.ID = Guid.NewGuid();
            _dbContext.Add<WeatherForecastEntity>(weatherForecastEntity);
            _dbContext.SaveChanges();

            return entityToModelMapper.Map<WeatherForecast>(weatherForecastEntity);
        }

        public List<WeatherForecast> Get()
        {
            List<WeatherForecastEntity> entities = _dbContext.WeatherForecasts.OrderBy(item => item.Date).ToList();
            List<WeatherForecast> weatherForecasts = new List<WeatherForecast>();

            foreach (var entity in entities)
            {
                weatherForecasts.Add(entityToModelMapper.Map<WeatherForecast>(entity));
            }

            return weatherForecasts;
        }

        public void Update(Guid id, WeatherForecast updateData)
        {
            var entity = GetEntityById(id);

            if (entity != null)
            {
                entity.TemperatureCelsius = updateData.TemperatureC;
                entity.Summary = updateData.Summary;
                _dbContext.Update<WeatherForecastEntity>(entity);
                _dbContext.SaveChanges();
            }
            else
            {
                // TODO: 204
            }
        }

        private WeatherForecastEntity GetEntityById(Guid id)
        {
            return _dbContext.WeatherForecasts.Find(id);
        }

        public WeatherForecast GetById(Guid id)
        {
            var entity = GetEntityById(id);

            if (entity != null)
            {
                return entityToModelMapper.Map<WeatherForecast>(entity);
            }
            else
            {
                return null; // 204
            }
        }
    }
}

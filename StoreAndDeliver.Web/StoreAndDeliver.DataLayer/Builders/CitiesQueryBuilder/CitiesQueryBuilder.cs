using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;
using System.Linq;

namespace StoreAndDeliver.DataLayer.Builders.CitiesQueryBuilder
{
    public class CitiesQueryBuilder : ICitiesQueryBuilder
    {
        private readonly StoreAndDeliverDbContext _dbContext;
        private IQueryable<City> _query;

        public CitiesQueryBuilder(StoreAndDeliverDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<City> Build()
        {
            IQueryable<City> result = _query;
            _query = null;
            return result;
        }

        public ICitiesQueryBuilder SetBaseCityInfo()
        {
            _query = _dbContext.Cities;
            return this;
        }

        public ICitiesQueryBuilder SetCityName(string cityName)
        {
            if (!string.IsNullOrEmpty(cityName))
            {
                _query = _query.Where(c => c.CityName.ToLower().Contains(cityName.ToLower()));
            }
            return this;
        }

        public ICitiesQueryBuilder SetCountryName(string countryName)
        {
            if (!string.IsNullOrEmpty(countryName))
            {
                _query = _query.Where(c => c.Country.ToLower().Contains(countryName.ToLower()));
            }
            return this;
        }
    }
}

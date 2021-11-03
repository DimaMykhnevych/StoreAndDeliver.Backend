using StoreAndDeliver.DataLayer.Models;

namespace StoreAndDeliver.DataLayer.Builders.CitiesQueryBuilder
{
    public interface ICitiesQueryBuilder : IQueryBuilder<City>
    {
        ICitiesQueryBuilder SetBaseCityInfo();
        ICitiesQueryBuilder SetCityName(string cityName);
        ICitiesQueryBuilder SetCountryName(string countryName);
    }
}

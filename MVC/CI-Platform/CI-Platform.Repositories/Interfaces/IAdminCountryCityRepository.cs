using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface IAdminCountryCityRepository
    {
        List<Country> GetCountry(string query, int recSkip, int recTake);

        int GetTotalCountryRecord(string query);

        void InsertCountry(Country country);

        void UpdateCountry(Country country);

        Country GetCountryById(long id);

        List<City> GetCity(string query, int recSkip, int recTake);

        int GetTotalCityRecord(string query);

        void InsertCity(City City);

        void UpdateCity(City City);

        City GetCityById(long id);
    }
}

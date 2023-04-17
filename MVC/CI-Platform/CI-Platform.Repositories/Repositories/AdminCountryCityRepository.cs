using CI_Platform.Data;
using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Repositories
{
    public class AdminCountryCityRepository : IAdminCountryCityRepository
    {
        private readonly CiPlatformContext _db;

        public AdminCountryCityRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<Country> GetCountry(string query, int recSkip, int recTake)
        {
            return _db.Countries.Where(x => x.Name.Contains(query) || x.Iso.Contains(query)).Skip(recSkip).Take(recTake).ToList();
        }

        public int GetTotalCountryRecord(string query)
        {
            return _db.Countries.Count(x => x.Name.Contains(query) || x.Iso.Contains(query));
        }

        public void InsertCountry(Country country)
        {
            _db.Countries.Add(country);
        }

        public void UpdateCountry(Country country)
        {
            _db.Countries.Update(country);
        }

        public Country GetCountryById(long id)
        {
            return _db.Countries.Where(x => x.CountryId == id).FirstOrDefault();
        }

        public List<City> GetCity(string query, int recSkip, int recTake)
        {
            return _db.Cities.Include(x => x.Country).Where(x => x.Name.Contains(query) || x.Country.Name.Contains(query)).Skip(recSkip).Take(recTake).ToList();
        }

        public int GetTotalCityRecord(string query)
        {
            return _db.Cities.Include(x => x.Country).Count(x => x.Name.Contains(query) || x.Country.Name.Contains(query));
        }

        public void InsertCity(City City)
        {
            _db.Cities.Add(City);
        }

        public void UpdateCity(City City)
        {
            _db.Cities.Update(City);
        }

        public City GetCityById(long id)
        {
            return _db.Cities.Where(x => x.CityId == id).FirstOrDefault();
        }
    }
}

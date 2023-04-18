using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class CountryCityController : Controller
    {
        private readonly IAdminCountryCityRepository _adminCountryCityRepository;
        private readonly ICommonRepository _commonRepository;

        public CountryCityController(IAdminCountryCityRepository adminCountryCityRepository, ICommonRepository commonRepository)
        {
            _adminCountryCityRepository = adminCountryCityRepository;
            _commonRepository = commonRepository;
        }

        public IActionResult Country(string id, int pg)
        {
            if (id == null)
            {
                id = "";
            }
            TempData["pg"] = pg;
            if (pg < 1)
            {
                pg = 1;
            }
            const int pageSize = 10;
            int recsCount = _adminCountryCityRepository.GetTotalCountryRecord(id);
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var Data = _adminCountryCityRepository.GetCountry(id, recSkip, pager.PageSize);
            if (Data.Count() == 0 && pg > 1)
            {
                return RedirectToAction("Country", "CountryCity", new { Area = "Admin", id = id, pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            ViewBag.query = id;
            return View(Data);
        }

        public IActionResult AddCountry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCountry(Country country)
        {
            if (ModelState.IsValid)
            {
                _adminCountryCityRepository.InsertCountry(country);
                _commonRepository.Save();
                TempData["msg"] = "Record Inserted Successfully!";
                return RedirectToAction("Country", "CountryCity", new { Area = "Admin", pg = TempData["pg"] });
            }
            return View(country);
        }

        public IActionResult EditCountry(long id)
        {
            var country = _adminCountryCityRepository.GetCountryById(id);
            if (country != null)
            {
                return View(country);
            }
            return RedirectToAction("Country", "CountryCity", new { Area = "Admin", pg = TempData["pg"] });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCountry(long id, Country country)
        {
            var newCountry = _adminCountryCityRepository.GetCountryById(id);
            if (ModelState.IsValid)
            {
                newCountry.UpdatedAt = DateTime.Now;
                newCountry.Name = country.Name;
                newCountry.Iso = country.Iso;
                _adminCountryCityRepository.UpdateCountry(newCountry);
                _commonRepository.Save();
                TempData["msg"] = "Record Edited Successfully!";
                return RedirectToAction("Country", "CountryCity", new { Area = "Admin", pg = TempData["pg"] });
            }
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCountry(long id)
        {
            var newCountry = _adminCountryCityRepository.GetCountryById(id);
            if (newCountry != null)
            {
                newCountry.DeletedAt = DateTime.Now;
                _adminCountryCityRepository.UpdateCountry(newCountry);
                _commonRepository.Save();
            }
            TempData["msg"] = "Record Deleted Successfully!";
            return RedirectToAction("Country", "CountryCity", new { Area = "Admin", pg = TempData["pg"] });
        }

        public IActionResult City(string id, int pg)
        {
            if (id == null)
            {
                id = "";
            }
            TempData["pg"] = pg;
            if (pg < 1)
            {
                pg = 1;
            }
            const int pageSize = 10;
            int recsCount = _adminCountryCityRepository.GetTotalCityRecord(id);
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var Data = _adminCountryCityRepository.GetCity(id, recSkip, pager.PageSize);
            if (Data.Count() == 0 && pg > 1)
            {
                return RedirectToAction("City", "CountryCity", new { Area = "Admin", id = id, pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            ViewBag.query = id;
            return View(Data);
        }

        public IActionResult AddCity()
        {
            ViewBag.country = _commonRepository.GetCountriesByNotDeleted();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCity(City city)
        {
            ViewBag.country = _commonRepository.GetCountriesByNotDeleted();
            ModelState.Remove("Country");
            if (ModelState.IsValid)
            {
                _adminCountryCityRepository.InsertCity(city);
                _commonRepository.Save();
                TempData["msg"] = "Record Inserted Successfully!";
                return RedirectToAction("City", "CountryCity", new { Area = "Admin", pg = TempData["pg"] });
            }
            return View(city);
        }

        public IActionResult EditCity(long id)
        {
            ViewBag.country = _commonRepository.GetCountriesByNotDeleted();
            var city = _adminCountryCityRepository.GetCityById(id);
            if (city != null)
            {
                return View(city);
            }
            return RedirectToAction("City", "CountryCity", new { Area = "Admin", pg = TempData["pg"] });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCity(long id, City city)
        {
            ViewBag.country = _commonRepository.GetCountriesByNotDeleted();
            var newCity = _adminCountryCityRepository.GetCityById(id);
            ModelState.Remove("Country");
            if (ModelState.IsValid)
            {
                newCity.UpdatedAt = DateTime.Now;
                newCity.Name = city.Name;
                newCity.CountryId = city.CountryId;
                _adminCountryCityRepository.UpdateCity(newCity);
                _commonRepository.Save();
                TempData["msg"] = "Record Edited Successfully!";
                return RedirectToAction("City", "CountryCity", new { Area = "Admin", pg = TempData["pg"] });
            }
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCity(long id)
        {
            var newCity = _adminCountryCityRepository.GetCityById(id);
            if (newCity != null)
            {
                newCity.DeletedAt = DateTime.Now;
                _adminCountryCityRepository.UpdateCity(newCity);
                _commonRepository.Save();
            }
            TempData["msg"] = "Record Deleted Successfully!";
            return RedirectToAction("City", "CountryCity", new { Area = "Admin", pg = TempData["pg"] });
        }
    }
}

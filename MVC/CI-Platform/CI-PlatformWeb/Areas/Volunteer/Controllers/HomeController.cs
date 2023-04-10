using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    public class HomeController : Controller
    {
        private readonly ICommonRepository _commonRepository;

        public HomeController(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }

        public IActionResult Privacy()
        {
            var privacy = _commonRepository.getAllPrivacy();
            foreach (var p in privacy)
            {
                p.Description = WebUtility.HtmlDecode(p.Description);
            }
            return View(privacy);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult GetCityByCountry(int country)
        {
            var results = _commonRepository.GetCitiesBycountry(country);
            return Json(results);
        }

    }
}
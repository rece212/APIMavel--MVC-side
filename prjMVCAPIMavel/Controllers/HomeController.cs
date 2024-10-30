using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using prjMVCAPIMavel.Models;
using prjMVCAPIMavel.Services;

namespace prjMVCAPIMavel.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ApiService _apiService;

        public HomeController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var avengers = await _apiService.GetAvengersAsync();
            return View(avengers);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TblAvenger avenger)
        {
            await _apiService.CreateAvengerAsync(avenger);
            return RedirectToAction("Index");
        }
        // New Delete action
        [HttpPost] // Ensure this is a POST request
        public async Task<IActionResult> Delete(string username)
        {
            try
            {
                await _apiService.DeleteAvengerAsync(username);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                ModelState.AddModelError(string.Empty, "The avenger cannot be deleted because " +
                    "there are contacts associated with this user.");
                var avengers = await _apiService.GetAvengersAsync();
                return View("Index", avengers); // Return to the Index view with the current list
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while trying " +
                    "to delete the avenger. "+ex.ToString());
                var avengers = await _apiService.GetAvengersAsync();
                return View("Index", avengers); // Return to the Index view with the current list
            }
        }
    }
}

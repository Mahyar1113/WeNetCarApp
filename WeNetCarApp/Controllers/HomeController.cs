using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeNetCarApp.Controllers
{
    
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string query, string location, decimal? minPrice, decimal? maxPrice)
        {
            // Create an instance of the CarRepository
            var carRepository = new CarRepository();

            // Retrieve all cars from the repository
            var cars = carRepository.GetAllCars();

            // Filter the cars based on the search query, location, and price range
            var matchedCars = cars.Where(c =>
                (string.IsNullOrEmpty(query) || c.Name.Contains(query)) &&
                (string.IsNullOrEmpty(location) || c.Location.Contains(location)) &&
                (!minPrice.HasValue || c.Price >= minPrice.Value) &&
                (!maxPrice.HasValue || c.Price <= maxPrice.Value)
            ).ToList();

            // Set the ViewBag values for MinPrice and MaxPrice
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            // Pass the search results to a view that displays the results
            return View(matchedCars);
        }

        public ActionResult Dashboard()
        {
            var carRepository = new CarRepository();

            var expensiveCars = carRepository.GetTopExpensiveCars(5);
            var carCountByLocation = carRepository.GetCarCountByLocation();
            var averagePriceByLocation = carRepository.GetAveragePriceByLocation();


            ViewBag.TopExpensiveCars = expensiveCars;
            ViewBag.CarCountByLocation = carCountByLocation;
            ViewBag.AveragePriceByLocation = averagePriceByLocation;

            return View();
        }
    }
}
using AspClientApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace AspClientApp.Controllers
{
    public class HomeController : Controller
    {
        
        public async  Task<IActionResult> Index()
        {
            var products = new List<ProductDTO>();

            using(var httpClint = new HttpClient())
            {
                using (var responsive =await httpClint.GetAsync("https://localhost:44370/api/products")) {

                   string apiResponse= await responsive.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<ProductDTO>>(apiResponse);
                }
            }


            return View(products);
        }

    }
}

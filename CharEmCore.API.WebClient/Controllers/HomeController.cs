using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CharEmCore.API.WebClient.Helpers;
using CharEmCore.API.WebClient.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace CharEmCore.API.WebClient.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var client = CharEmHttpClient.GetClient();
            var model = new ServiceTypeSearchViewModel();

            HttpResponseMessage serviceTypeResponse = await client.GetAsync("api/Service/Types");
            if (serviceTypeResponse.IsSuccessStatusCode)
            {
                string serviceTypesMessageContent = await serviceTypeResponse.Content.ReadAsStringAsync();
                var serviceTypes = JsonConvert.DeserializeObject<IEnumerable<ServiceType>>(serviceTypesMessageContent);
                model.ServiceTypes = serviceTypes.ToList();
            }

            else { return Content("An Error Occurred."); }

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

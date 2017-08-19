using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CharEmCore.API.WebClient.Helpers;
using System.Net.Http;
using CharEmCore.API.WebClient.Models;
using Newtonsoft.Json;
using CharEmCore.API.WebClient.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using System.Text;

namespace CharEmCore.API.WebClient.Controllers
{
    public class CountyController : Controller
    {
        private HttpClient _client;

        public CountyController()
        {
            _client = CharEmHttpClient.GetClient();
        }
        // GET: County
        public async Task<ActionResult> Index()
        {
            var model = new CountyViewModel();

            HttpResponseMessage requestResponse = await _client.GetAsync("api/county");


            if (requestResponse.IsSuccessStatusCode)
            {
                string requestContent = await requestResponse.Content.ReadAsStringAsync();
                var requestObject = JsonConvert.DeserializeObject<IEnumerable<County>>(requestContent);
                model.Counties = requestObject.ToList();
            }

            return View(model);
        }

        // GET: County/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = new CountyViewModel();

            HttpResponseMessage requestResponse = await _client.GetAsync("api/county/" + id);

            if (requestResponse.IsSuccessStatusCode)
            {
                string requestContent = await requestResponse.Content.ReadAsStringAsync();
                var requestObject = JsonConvert.DeserializeObject<IEnumerable<County>>(requestContent);
                model.Counties = requestObject.ToList();
            }

            return View(model);
        }

        // GET: County/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: County/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CountyViewModel county)
        {
            try
            {
                var serializedItem = JsonConvert.SerializeObject(county);
                HttpContent serializedContent = new StringContent(serializedItem, System.Text.Encoding.Unicode, "appliation/json");
                var response = await _client.PostAsync("api/county/", serializedContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("An error occurred");
                }
            }
            catch
            {
                return Content("An error occurred");
            }
        }

        // GET: County/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var response = await _client.GetAsync("api/county/" + id);
            string content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var model = JsonConvert.DeserializeObject<County>(content);
                return View(model);
            }

            return Content("An error occurred: " + content);

        }

        // POST: County/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, County county)
        {
            try
            {
                JsonPatchDocument<CountyDTO> patchDoc = new JsonPatchDocument<CountyDTO>();
                patchDoc.Replace(x => x.Id, county.Id);
                patchDoc.Replace(x => x.Name, county.Name);

                var serializedUpdate = JsonConvert.SerializeObject(patchDoc);
                var content = new StringContent(serializedUpdate, Encoding.Unicode, "application/json");

                var response = await _client.PatchAsync("api/county/" + id, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else return Content("An Error Occurred");
            }
            catch
            {
                return Content("An Error Occurred");
            }
        }

        // GET: County/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: County/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, IFormCollection collection)
        {
            try
            {
                var response = await _client.DeleteAsync("api/county/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                else return Content("An Error Occurred");
            }
            catch
            {
                return Content("An Error Occurred");
            }
        }
    }
}
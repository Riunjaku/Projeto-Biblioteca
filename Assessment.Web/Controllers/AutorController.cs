using Assessment.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Assessment.Web.Controllers
{
    [Authorize]
    public class AutorController : Controller
    {
        private HttpClient _client;

        public AutorController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44366/");
            //_client.BaseAddress = new Uri("http://localhost:28347/");
            _client.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }

        //  ImageService imageService = new ImageService();

        // GET: Autor
        public ActionResult GetAll()
        {
            var response = _client.GetAsync("/api/Autors").Result;

            if (response.IsSuccessStatusCode)
            {
                var JsonString = response.Content.ReadAsStringAsync().Result;
                var autores = JsonConvert.DeserializeObject<List<AutorViewModel>>(JsonString);

                return View(autores);

            }
            return View();
        }

        // GET: Autor/Details/5
        public ActionResult Details(int id)
        {
            var response = _client.GetAsync("/api/Autors/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var JsonString = response.Content.ReadAsStringAsync().Result;
                var autor = JsonConvert.DeserializeObject<AutorViewModel>(JsonString);

                return View(autor);

            }
            return View();
        }

        // GET: Autores/Create        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AutorViewModel autor)
        {

            var response = await _client.PostAsJsonAsync("/api/Autors", autor);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAll");
            }
            return View();
        }

        // GET: Autores/Edit/5
        public ActionResult Edit(int id)
        {

            var response = _client.GetAsync("/api/Autors/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var JsonString = response.Content.ReadAsStringAsync().Result;
                var autor = JsonConvert.DeserializeObject<AutorViewModel>(JsonString);

                return View(autor);

            }
            return View();
        }




        // POST: Autor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AutorViewModel autor)
        {
            var response = await _client.PutAsJsonAsync("/api/Autors/" + id, autor);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("GetAll");
            }

            return View();
        }

        // GET: Autor/Delete/5
        public ActionResult Delete(int id)
        {
            var response = _client.GetAsync("/api/Autors/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var JsonString = response.Content.ReadAsStringAsync().Result;
                var autor = JsonConvert.DeserializeObject<AutorViewModel>(JsonString);

                return View(autor);

            }
            return View();
        }

        // POST: Autor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            var response = await _client.DeleteAsync("/api/Autors/" + id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAll");
            }

            return View();

        }
    }
}

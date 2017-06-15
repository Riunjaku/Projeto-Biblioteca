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
    public class LivroController : Controller
    {
        private HttpClient _client;



        public LivroController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44366/");
            //_client.BaseAddress = new Uri("http://localhost:28347/");
            _client.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }

        //  ImageService imageService = new ImageService();

        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            var response = await _client.GetAsync("api/Livros");

            if (response.IsSuccessStatusCode)
            {
                var JsonString = await response.Content.ReadAsStringAsync();

                var LivroList = JsonConvert.DeserializeObject<List<LivroViewModel>>(JsonString);


                return View(LivroList);

            }
            else
            {
                return View();

            }


        }

        // GET: Livro/Details/5
        public ActionResult Details(int id)
        {
            var response = _client.GetAsync("api/Livros/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var String = response.Content.ReadAsStringAsync().Result;
                var Book = JsonConvert.DeserializeObject<LivroViewModel>(String);
                return View(Book);
            }
            return View();
        }

        // GET: Livro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Livro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LivroViewModel livro)
        {
            
            var response = await _client.PostAsJsonAsync("api/Livros", livro);

            if (response.IsSuccessStatusCode)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            return View();

        }

        // GET: Livro/Edit/5
        public ActionResult Edit(int id)
        {
            var response = _client.GetAsync("api/Livros/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var JsonString = response.Content.ReadAsStringAsync().Result;

                var Book = JsonConvert.DeserializeObject<LivroViewModel>(JsonString);


                return View(Book);
            }
            return View();
        }

        // POST: Livro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int Id, LivroViewModel livro)
        {

            var response = await _client.PutAsJsonAsync("api/Livros/" + Id, livro);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAll");
            }
            return View();
        }


        // GET: Livro/Delete/5
        public ActionResult Delete(int id)
        {
            var response = _client.GetAsync("api/Livros/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var JsonString = response.Content.ReadAsStringAsync().Result;

                var Livro = JsonConvert.DeserializeObject<LivroViewModel>(JsonString);


                return View(Livro);

            }
            else
            {
                return View();

            }
        }

        // POST: Livro/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            var response = await _client.DeleteAsync("api/Livros/" + id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAll");
            }

            return View();

        }

        public ActionResult AdicionarAutores(int id)
        {
            TempData["livroid"] = id;
            TempData.Keep();

            var livro = _client.GetAsync("api/Livros/" + id).Result;
            var response = _client.GetAsync("api/Autors").Result;

            if (response.IsSuccessStatusCode)
            {
                
                var JsonString = response.Content.ReadAsStringAsync().Result;
                var autores = JsonConvert.DeserializeObject<List<AutorViewModel>>(JsonString);
                var livroString = livro.Content.ReadAsStringAsync().Result;
                var livros = JsonConvert.DeserializeObject<LivroViewModel>(livroString);

                foreach (var item in livros.Autores)
                {
                    var autor = new AutorViewModel()
                    {
                        AutorId = item.AutorId,
                        Nome = item.Nome,
                        Selecionado = item.Selecionado,
                        Sobrenome = item.Sobrenome
                    };
                    foreach (var item2 in autores)
                    {
                        if (item2.AutorId == autor.AutorId)
                        {
                            item2.Selecionado = true;
                        }
                    }

                }

                TempData.Keep();

                return View(autores);

            }
            return View();
        }

        public async Task<ActionResult> FazRelacionamento(int id)
        {
            var livroid = (int)TempData["livroid"];

            var uri = "/api/Livros/?autorId=" + id + "&livroId=" + livroid;

            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                return View();
            }
        }

        public async Task<ActionResult> DesfazRelacionamento(int id)
        {
            var livroid = (int)TempData["livroid"];

            var uri = "/api/Livros/DesfazRelacionamento/?autorId=" + id + "&livroId=" + livroid + "&difere=" + livroid;

            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                return View();
            }
        }
    }
}


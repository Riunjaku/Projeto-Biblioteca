using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Asessment.API.Models;

namespace Asessment.API.Controllers
{
    public class AutorsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Autors
        public IList<Autor> GetAll()
        {
            var retorno = db.Autors.ToList();
            List<Autor> autores = new List<Autor>();
            foreach (var item in retorno)
            {
                var autor = new Autor()
                {
                    AutorId = item.AutorId,
                    Nome = item.Nome,
                    Sobrenome = item.Sobrenome,
                    DataNascimento = item.DataNascimento,
                    Email = item.Email,
                    Livros = new List<Livro>()
                };
                foreach (var item2 in item.Livros)
                {
                    item2.Autores = new List<Autor>();
                    autor.Livros.Add(item2);
                }
                autores.Add(autor);
            }

            return autores;
        }


        // GET: api/Autors/5
        [ResponseType(typeof(Autor))]
        public IHttpActionResult GetAutor(int id)
        {
            Autor busca = db.Autors.Find(id);
            if (busca == null)
            {
                return NotFound();
            }
            Autor autor = new Autor()
            {
                AutorId = busca.AutorId,
                Nome = busca.Nome,
                Sobrenome = busca.Sobrenome,
                Email = busca.Email,
                DataNascimento = busca.DataNascimento,
                Livros = new List<Livro>()
            };


            return Ok(autor);
        }

        // PUT: api/Autors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAutor(int id, Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != autor.AutorId)
            {
                return BadRequest();
            }

            db.Entry(autor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Autors
        [ResponseType(typeof(Autor))]
        public async Task<IHttpActionResult> PostAutor(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Autors.Add(autor);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = autor.AutorId }, autor);
        }

        // DELETE: api/Autors/5
        [ResponseType(typeof(Autor))]
        public async Task<IHttpActionResult> DeleteAutor(int id)
        {
            Autor autor = await db.Autors.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }

            db.Autors.Remove(autor);
            await db.SaveChangesAsync();

            return Ok(autor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AutorExists(int id)
        {
            return db.Autors.Count(e => e.AutorId == id) > 0;
        }
    }
}
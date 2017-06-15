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
    public class LivrosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Livros
        public IList<Livro> GetLivros()
        {
            var retorno = db.Livros.ToList();
            List<Livro> livros = new List<Livro>();
            foreach (var item in retorno)
            {
                var livro = new Livro()
                {
                    LivroId = item.LivroId,
                    Titulo = item.Titulo,
                    Edicao = item.Edicao,
                    Editora = item.Editora,
                    Comentario = item.Comentario,
                    Ano = item.Ano,
                    Disponibilidade = item.Disponibilidade,
                    Autores = new List<Autor>()
                };
                foreach (var item2 in item.Autores)
                {
                    item2.Livros = new List<Livro>();
                    livro.Autores.Add(item2);
                }
                livros.Add(livro);
            }

            return livros;
        }

        // GET: api/Livros/5
        [ResponseType(typeof(Livro))]
        public IHttpActionResult GetLivro(int id)
        {
            Livro busca = db.Livros.Find(id);
            if (busca == null)
            {
                return NotFound();
            }
            var livro = new Livro()
            {
                LivroId = busca.LivroId,
                Titulo = busca.Titulo,
                Edicao = busca.Edicao,
                Editora = busca.Editora,
                Comentario = busca.Comentario,
                Ano = busca.Ano,
                Disponibilidade = busca.Disponibilidade,
                Autores = new List<Autor>(),
            };


           
                return Ok(busca);
        }

        // PUT: api/Livros/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLivro(int id,Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (id != livro.LivroId)
            {
                return BadRequest();
            }

            db.Entry(livro).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(id))
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

    
        // POST: api/Livros
        [ResponseType(typeof(Livro))]
        public async Task<IHttpActionResult> PostLivro(Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Livros.Add(livro);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = livro.LivroId }, livro);
        }

        // DELETE: api/Livros/5
        [ResponseType(typeof(Livro))]
        public async Task<IHttpActionResult> DeleteLivro(int id)
        {
            Livro livro = await db.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            db.Livros.Remove(livro);
            await db.SaveChangesAsync();

            return Ok(livro);
        }

        [HttpGet]
        
        public void FazRelacionamento(int autorId, int livroId)
        {
            var autor = db.Autors.Where(a => a.AutorId == autorId).FirstOrDefault();
            var livro = db.Livros.Where(b => b.LivroId == livroId).FirstOrDefault();

            autor.Livros.Add(livro);
            livro.Autores.Add(autor);
            db.SaveChanges();


        }

        [HttpGet]
        public IHttpActionResult DesfazRelacionamento(int autorId, int livroId, int difere)
        {
            var autor = db.Autors.Where(a => a.AutorId == autorId).FirstOrDefault();
            var livro = db.Livros.Where(b => b.LivroId == livroId).FirstOrDefault();

            autor.Livros.Remove(livro);
            livro.Autores.Remove(autor);
            db.SaveChangesAsync();

            return Ok();


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LivroExists(int id)
        {
            return db.Livros.Count(e => e.LivroId == id) > 0;
        }
    }
}
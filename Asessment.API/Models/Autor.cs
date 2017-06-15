using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asessment.API.Models
{
    public class Autor
    {
        public int AutorId { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public bool Selecionado { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Livro> Livros { get; set; }
    }
}
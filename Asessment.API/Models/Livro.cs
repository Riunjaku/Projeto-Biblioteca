using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Asessment.API.Models
{
    public class Livro
    {
        [Key]
        public int LivroId { get; set; }

        public string Titulo { get; set; }

        public string Editora { get; set; }

        public int Ano { get; set; }

        public string Edicao { get; set; }

        public bool Disponibilidade { get; set; }

        public string Comentario { get; set; }

        public virtual ICollection<Autor> Autores { get; set; }

    }
}
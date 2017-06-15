using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assessment.Web.Models
{
    public class LivroViewModel
    {
        [Display(Name = "Id")]
        public int LivroId { get; set; }

        public string Titulo { get; set; }

        public string Editora { get; set; }

        public int Ano { get; set; }

        [Display(Name = "Edição")]
        public string Edicao { get; set; }

        public bool Disponibilidade { get; set; }

        public string Comentario { get; set; }

        public virtual ICollection<AutorViewModel> Autores { get; set; }

        
    }
}
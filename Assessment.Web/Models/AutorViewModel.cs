using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assessment.Web.Models
{
    public class AutorViewModel
    {
        public int AutorId { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; }
        [Required]
        [StringLength(150)]
        public string Sobrenome { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Data de nascimento")]
        [DataType(DataType.DateTime, ErrorMessage = "coloque a data no modelo dd/mm/yyyy")]
        public DateTime DataNascimento { get; set; }

        public bool Selecionado { get; set; }
    }
}
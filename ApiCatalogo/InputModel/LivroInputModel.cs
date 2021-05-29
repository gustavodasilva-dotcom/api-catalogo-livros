using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.InputModel
{
    public class LivroInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do livro deve conter de 3 a 100 caracteres.")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do autor deve conter de 3 a 100 caracteres.")]
        public string Autor { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da editora deve conter de 3 a 100 caracteres.")]
        public string Editora { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "A edição do livro deve estar entre 1 e 100 edições.")]
        public int Edicao { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "A quantidade de página do livro deve ser entre 1 e 10000 páginas.")]
        public int Paginas { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "O preço do livro deve ser entre 1 e 1000 reais.")]
        public double Preco { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Exceptions
{
    public class LivroNaoCadastradoException : Exception
    {
        public LivroNaoCadastradoException() : base("Esse livro não está cadastrado.") { }

    }
}

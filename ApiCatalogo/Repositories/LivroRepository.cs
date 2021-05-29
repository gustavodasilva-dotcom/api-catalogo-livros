using ApiCatalogo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private static Dictionary<Guid, Livro> livros = new Dictionary<Guid, Livro>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Livro{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Harry Potter e o enigma do princípe", Autor = "J. K. Rowling", Editora = "Rocco", Edicao = 3, Paginas = 500, Preco = 80} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Livro{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "1984", Autor = "George Orwell", Editora = "Companhia das Letras", Edicao = 5, Paginas = 300, Preco = 50} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Livro{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "A metamorfose", Autor = "Franz Kafka", Editora = "Antofagia", Edicao = 3, Paginas = 150, Preco = 70} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Livro{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Memórias Postumas de Brás Cubas", Autor = "Machado de Assim", Editora = "Panda", Edicao = 6, Paginas = 150, Preco = 60} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Livro{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "A Vida Como Ela É...", Autor = "Nelson Rodrigues", Editora = "Companhia das Letras", Edicao = 4, Paginas = 250, Preco = 50} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Livro{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "À sombra das chuteiras imortais", Autor = "Nelson Rodrigues", Editora = "Companhia das Letras", Edicao = 2, Paginas = 150, Preco = 60} }
        };


        public Task Atualizar(Livro livro)
        {
            livros[livro.Id] = livro;

            return Task.CompletedTask;
        }


        public Task Atualizar(Livro livro, Guid id)
        {
            livros[livro.Id] = livro;

            return Task.CompletedTask;
        }


        public void Dispose()
        {
            // Fechar conexão com o banco de dados.
        }


        public Task Inserir(Livro livro)
        {
            livros.Add(livro.Id, livro);

            return Task.CompletedTask;
        }


        public Task<List<Livro>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(livros.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }


        public Task<Livro> Obter(Guid id)
        {
            if (!livros.ContainsKey(id))
                return null;

            return Task.FromResult(livros[id]);
        }


        public Task<List<Livro>> Obter(string nome, int edicao)
        {
            return Task.FromResult(livros.Values.Where(livro => livro.Nome.Equals(nome) && livro.Edicao.Equals(edicao)).ToList());
        }


        public Task<List<Livro>> ObterSemLambda(string nome, int edicao)
        {
            var retorno = new List<Livro>();

            foreach (var livro in livros.Values)
            {
                if (livro.Nome.Equals(nome) && livro.Edicao.Equals(edicao))
                    retorno.Add(livro);
            }

            return Task.FromResult(retorno);
        }


        public Task Remover(Guid id)
        {
            livros.Remove(id);

            return Task.CompletedTask;
        }
    }
}

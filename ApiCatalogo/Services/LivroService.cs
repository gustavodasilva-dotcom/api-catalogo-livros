using ApiCatalogo.Entities;
using ApiCatalogo.Exceptions;
using ApiCatalogo.InputModel;
using ApiCatalogo.Repositories;
using ApiCatalogo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task Atualizar(Guid id, LivroInputModel livro)
        {
            var entidadeLivro = await _livroRepository.Obter(id);

            if (entidadeLivro == null)
                throw new LivroNaoCadastradoException();

            entidadeLivro.Nome = livro.Nome;
            entidadeLivro.Autor = livro.Autor;
            entidadeLivro.Editora = livro.Editora;
            entidadeLivro.Edicao = livro.Edicao;
            entidadeLivro.Paginas = livro.Paginas;
            entidadeLivro.Preco = livro.Preco;

            await _livroRepository.Atualizar(entidadeLivro, id);
        }


        public async Task AtualizarNome(Guid id, string nome)
        {
            var entidadeLivro = await _livroRepository.Obter(id);

            if (entidadeLivro == null)
                throw new LivroNaoCadastradoException();

            entidadeLivro.Nome = nome;

            await _livroRepository.Atualizar(entidadeLivro);
        }


        public async Task AtualizarAutor(Guid id, string autor)
        {
            var entidadeLivro = await _livroRepository.Obter(id);

            if (entidadeLivro == null)
                throw new LivroNaoCadastradoException();

            entidadeLivro.Autor = autor;

            await _livroRepository.Atualizar(entidadeLivro);
        }


        public async Task AtualizarEditora(Guid id, string editora)
        {
            var entidadeLivro = await _livroRepository.Obter(id);

            if (entidadeLivro == null)
                throw new LivroNaoCadastradoException();

            entidadeLivro.Editora = editora;

            await _livroRepository.Atualizar(entidadeLivro);
        }


        public async Task AtualizarEdicao(Guid id, int edicao)
        {
            var entidadeLivro = await _livroRepository.Obter(id);

            if (entidadeLivro == null)
                throw new LivroNaoCadastradoException();

            entidadeLivro.Edicao = edicao;

            await _livroRepository.Atualizar(entidadeLivro);
        }


        public async Task AtualizarPaginas(Guid id, int paginas)
        {
            var entidadeLivro = await _livroRepository.Obter(id);

            if (entidadeLivro == null)
                throw new LivroNaoCadastradoException();

            entidadeLivro.Paginas = paginas;

            await _livroRepository.Atualizar(entidadeLivro);
        }


        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeLivro = await _livroRepository.Obter(id);

            if (entidadeLivro == null)
                throw new LivroNaoCadastradoException();

            entidadeLivro.Preco = preco;

            await _livroRepository.Atualizar(entidadeLivro);
        }


        public async Task<LivroViewModel> Inserir(LivroInputModel livro)
        {
            var entidadeLivro = await _livroRepository.Obter(livro.Nome, livro.Edicao);

            if (entidadeLivro.Count > 0)
                throw new LivroJaCadastradoException();

            var livroInsert = new Livro
            {
                Id = Guid.NewGuid(),
                Nome = livro.Nome,
                Autor = livro.Autor,
                Editora = livro.Editora,
                Edicao = livro.Edicao,
                Paginas = livro.Paginas,
                Preco = livro.Preco
            };

            await _livroRepository.Inserir(livroInsert);

            return new LivroViewModel
            {
                Id = livroInsert.Id,
                Nome = livroInsert.Nome,
                Autor = livroInsert.Autor,
                Editora = livroInsert.Editora,
                Edicao = livroInsert.Edicao,
                Paginas = livroInsert.Paginas,
                Preco = livroInsert.Preco
            };
        }

        public async Task<List<LivroViewModel>> Obter(int pagina, int quantidade)
        {
            var livros = await _livroRepository.Obter(pagina, quantidade);

            return livros.Select(livro => new LivroViewModel
            {
                Id = livro.Id,
                Nome = livro.Nome,
                Autor = livro.Autor,
                Editora = livro.Editora,
                Edicao = livro.Edicao,
                Paginas = livro.Paginas,
                Preco = livro.Preco
            }).ToList();
        }

        public async Task<LivroViewModel> Obter(Guid id)
        {
            var livro = await _livroRepository.Obter(id);

            if (livro == null)
                return null;

            return new LivroViewModel
            {
                Id = livro.Id,
                Nome = livro.Nome,
                Autor = livro.Autor,
                Editora = livro.Editora,
                Edicao = livro.Edicao,
                Paginas = livro.Paginas,
                Preco = livro.Preco
            };
        }

        public async Task Remover(Guid id)
        {
            var livro = await _livroRepository.Obter(id);

            if (livro == null)
                throw new LivroNaoCadastradoException();

            await _livroRepository.Remover(id);
        }

        public void Dispose()
        {
            _livroRepository?.Dispose();
        }
    }
}

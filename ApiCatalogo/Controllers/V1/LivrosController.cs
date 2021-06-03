using ApiCatalogo.Exceptions;
using ApiCatalogo.InputModel;
using ApiCatalogo.Services;
using ApiCatalogo.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }


        /// <summary>
        /// Esta funcionalidade retorna todos os livros de forma paginada.
        /// </summary>
        /// <remarks>
        /// Não é possível retornar livros sem paginação.
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada (mínimo 1).</param>
        /// <param name="quantidade">Indica a quantidade de registros retornardos na página (máximo 50)</param>
        /// <response code="200">Caso haja livros cadastrados.</response>
        /// <response code="204">Caso não haja livros cadastrados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroViewModel>>> Obter
            ([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var livros = await _livroService.Obter(pagina, quantidade);

            if (livros.Count() == 0)
                return NoContent();

            return Ok(livros);
        }


        /// <summary>
        /// Esta funcionalidade retorna um único livro com base no código identificador (Id) fornecido.
        /// </summary>
        /// <remarks>
        /// Não é possível retornar um livro sem o Id.
        /// </remarks>
        /// <param name="idLivro">O Id do livro desejado.</param>
        /// <response code="200">Caso o livro esteja cadastrado.</response>
        /// <response code="204">Caso o livro não esteja cadastrado.</response>
        [HttpGet("{idLivro:guid}")]
        public async Task<ActionResult<LivroViewModel>> Obter([FromRoute] Guid idLivro)
        {
            var livro = await _livroService.Obter(idLivro);

            if (livro == null)
                return NoContent();

            return Ok(livro);
        }


        /// <summary>
        /// Esta funcionalidade permite cadastrar livros.
        /// </summary>
        /// <remarks>
        /// O livro só será cadastrado se todas as informações obrigatórias forem preenchidas.
        /// </remarks>
        /// <param name="livroInputModel">Os dados do livro a ser cadastrado.</param>
        /// <response code="200">Caso o livro seja cadastrado com sucesso.</response>
        /// <response code="422">Caso o livro a ser cadastrado já esteja cadastrado.</response>
        [HttpPost]
        public async Task<ActionResult<LivroViewModel>> InserirLivro([FromBody] LivroInputModel livroInputModel)
        {
            try
            {
                var livro = await _livroService.Inserir(livroInputModel);

                return Ok(livro);
            }
            catch(LivroJaCadastradoException ex)
            {
                return UnprocessableEntity("Esse livro já está cadastrado.");
            }
        }


        /// <summary>
        /// Esta funcionalidade permite atualizar todos os dados de um livro.
        /// </summary>
        /// <remarks>
        /// O livro precisa estar cadastrado.
        /// </remarks>
        /// <param name="idLivro">O Id do livro a ser atualizado.</param>
        /// <param name="livroInputModel">Dados a serem atualizados no livro.</param>
        /// <response code="200">Caso o livro esteja cadastrado.</response>
        /// <response code="404">Caso o livro não esteja cadastrado.</response>
        [HttpPut("{idLivro:guid}")]
        public async Task<ActionResult> AtualizarLivro([FromRoute] Guid idLivro, [FromBody] LivroInputModel livroInputModel)
        {
            try
            {
                await _livroService.Atualizar(idLivro, livroInputModel);

                return Ok();
            }
            catch(LivroNaoCadastradoException ex)
            {
                return NotFound("Esse livro não está cadastrado.");
            }
        }


        /// <summary>
        /// Esta funcionalidade permite atualizar o recurso de nome em um livro.
        /// </summary>
        /// <remarks>
        /// O livro precisa estar cadastrado.
        /// </remarks>
        /// <param name="idLivro">O Id do livro a ser atualizado.</param>
        /// <param name="nome">Nome a ser atualizado no livro.</param>
        /// <response code="200">Caso o livro esteja cadastrado.</response>
        /// <response code="404">Caso o livro não esteja cadastrado.</response>
        [HttpPatch("{idLivro:guid}/nome/{nome}")]
        public async Task<ActionResult> AtualizarLivroNome([FromRoute] Guid idLivro, [FromRoute] string nome)
        {
            try
            {
                await _livroService.AtualizarNome(idLivro, nome);

                return Ok();
            }
            catch(LivroNaoCadastradoException ex)
            {
                return NotFound("Esse livro não está cadastrado.");
            }
        }


        /// <summary>
        /// Esta funcionalidade permite atualizar o recurso de autor em um livro.
        /// </summary>
        /// <remarks>
        /// O livro precisa estar cadastrado.
        /// </remarks>
        /// <param name="idLivro">O Id do livro a ser atualizado.</param>
        /// <param name="autor">Autor a ser atualizado no livro.</param>
        /// <response code="200">Caso o livro esteja cadastrado.</response>
        /// <response code="404">Caso o livro não esteja cadastrado.</response>
        [HttpPatch("{idLivro:guid}/autor/{autor}")]
        public async Task<ActionResult> AtualizarLivroAutor([FromRoute] Guid idLivro, [FromRoute] string autor)
        {
            try
            {
                await _livroService.AtualizarAutor(idLivro, autor);

                return Ok();
            }
            catch(LivroNaoCadastradoException ex)
            {
                return NotFound("Esse livro não está cadastrado.");
            }
        }


        /// <summary>
        /// Esta funcionalidade permite atualizar o recurso de editora em um livro.
        /// </summary>
        /// <remarks>
        /// O livro precisa estar cadastrado.
        /// </remarks>
        /// <param name="idLivro">O Id do livro a ser atualizado.</param>
        /// <param name="editora">Editora a ser atualizada no livro.</param>
        /// <response code="200">Caso o livro esteja cadastrado.</response>
        /// <response code="404">Caso o livro não esteja cadastrado.</response>
        [HttpPatch("{idLivro:guid}/editora/{editora}")]
        public async Task<ActionResult> AtualizarLivroEditora([FromRoute] Guid idLivro, [FromRoute] string editora)
        {
            try
            {
                await _livroService.AtualizarEditora(idLivro, editora);

                return Ok();
            }
            catch(LivroNaoCadastradoException ex)
            {
                return NotFound("Esse livro não está cadastrado.");
            }
        }


        /// <summary>
        /// Esta funcionalidade permite atualizar o recurso de edição em um livro.
        /// </summary>
        /// <remarks>
        /// O livro precisa estar cadastrado.
        /// </remarks>
        /// <param name="idLivro">O Id do livro a ser atualizado.</param>
        /// <param name="edicao">Número da edição a ser atualizada no livro.</param>
        /// <response code="200">Caso o livro esteja cadastrado.</response>
        /// <response code="404">Caso o livro não esteja cadastrado.</response>
        [HttpPatch("{idLivro:guid}/edicao/{edicao:int}")]
        public async Task<ActionResult> AtualizarLivroEdicao([FromRoute] Guid idLivro, [FromRoute] int edicao)
        {
            try
            {
                await _livroService.AtualizarEdicao(idLivro, edicao);

                return Ok();
            }
            catch(LivroNaoCadastradoException ex)
            {
                return NotFound("Esse livro não está cadastrado.");
            }
        }


        /// <summary>
        /// Esta funcionalidade permite atualizar o recurso de paginas em um livro.
        /// </summary>
        /// <remarks>
        /// O livro precisa estar cadastrado.
        /// </remarks>
        /// <param name="idLivro">O Id do livro a ser atualizado.</param>
        /// <param name="paginas">Número de páginas a serem atualizadas no livro.</param>
        /// <response code="200">Caso o livro esteja cadastrado.</response>
        /// <response code="404">Caso o livro não esteja cadastrado.</response>
        [HttpPatch("{idLivro:guid}/paginas/{paginas:int}")]
        public async Task<ActionResult> AtualizarLivroPaginas([FromRoute] Guid idLivro, [FromRoute] int paginas)
        {
            try
            {
                await _livroService.AtualizarPaginas(idLivro, paginas);

                return Ok();
            }
            catch(LivroNaoCadastradoException ex)
            {
                return NotFound("Esse livro não está cadastrado.");
            }
        }


        /// <summary>
        /// Esta funcionalidade permite atualizar o recurso de preço em um livro.
        /// </summary>
        /// <remarks>
        /// O livro precisa estar cadastrado.
        /// </remarks>
        /// <param name="idLivro">O Id do livro a ser atualizado.</param>
        /// <param name="preco">Preço a ser atualizado no livro.</param>
        /// <response code="200">Caso o livro esteja cadastrado.</response>
        /// <response code="404">Caso o livro não esteja cadastrado.</response>
        [HttpPatch("{idLivro:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarLivro([FromRoute] Guid idLivro, [FromRoute] double preco)
        {
            try
            {
                await _livroService.Atualizar(idLivro, preco);

                return Ok();
            }
            catch(LivroNaoCadastradoException ex)
            {
                return NotFound("Esse livro não está cadastrado.");
            }
        }


        /// <summary>
        /// Esta funcionalidade permite apagar o registro de um livro.
        /// </summary>
        /// <remarks>
        /// O livro precisa estar cadastrado.
        /// </remarks>
        /// <param name="idLivro">O Id do livro a ser apagado.</param>
        /// <response code="200">Caso o livro esteja cadastrado.</response>
        /// <response code="404">Caso o livro não esteja cadastrado.</response>
        [HttpDelete("{idLivro:guid}")]
        public async Task<ActionResult> ApagarLivro([FromRoute] Guid idLivro)
        {
            try
            {
                await _livroService.Remover(idLivro);

                return Ok();
            }
            catch(LivroNaoCadastradoException ex)
            {
                return NotFound("Esse livro não está cadastrado.");
            }
        }
    }
}

using ApiCatalogo.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Repositories
{
    public class LivroSqlServerRepository : ILivroRepository
    {
        private readonly SqlConnection sqlConnection;

        public LivroSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }


        public async Task Atualizar(Livro livro)
        {
            var comando = $"UPDATE Livros SET Nome = '{livro.Nome}', Autor = '{livro.Autor}', Editora = '{livro.Editora}', Edicao = {livro.Edicao}, Paginas = {livro.Paginas}, Preco = {livro.Preco.ToString().Replace(",", ".")} WHERE Id = '{livro.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }


        public async Task Atualizar(Livro livro, Guid id)
        {
            var comando = $"UPDATE Livros SET Nome = '{livro.Nome}', Autor = '{livro.Autor}', Editora = '{livro.Editora}', Edicao = {livro.Edicao}, Paginas = {livro.Paginas}, Preco = {livro.Preco.ToString().Replace(",", ".")} WHERE Id = '{livro.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }


        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }


        public async Task Inserir(Livro livro)
        {
            var comando = $"INSERT Livros (Id, Nome, Autor, Editora, Edicao, Paginas, Preco) values ('{livro.Id}', '{livro.Nome}', '{livro.Autor}', '{livro.Editora}', {livro.Edicao}, {livro.Paginas}, {livro.Preco.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            await sqlConnection.CloseAsync();
        }


        public async Task<List<Livro>> Obter(int pagina, int quantidade)
        {
            var livros = new List<Livro>();

            var comando = $"SELECT * FROM Livros ORDER BY Id Offset {((pagina - 1) * quantidade)} ROWS FETCH NEXT {quantidade} ROWS ONLY";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                livros.Add(new Livro
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Autor = (string)sqlDataReader["Autor"],
                    Editora = (string)sqlDataReader["Editora"],
                    Edicao = (int)sqlDataReader["Edicao"],
                    Paginas = (int)sqlDataReader["Paginas"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return livros;
        }


        public async Task<Livro> Obter(Guid id)
        {
            Livro livro = null;

            var comando = $"SELECT * FROM Livros WHERE Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                livro = new Livro
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Autor = (string)sqlDataReader["Autor"],
                    Editora = (string)sqlDataReader["Editora"],
                    Edicao = (int)sqlDataReader["Edicao"],
                    Paginas = (int)sqlDataReader["Paginas"],
                    Preco = (double)sqlDataReader["Preco"]
                };
            }

            await sqlConnection.CloseAsync();

            return livro;
        }

        public async Task<List<Livro>> Obter(string nome, int edicao)
        {
            var livros = new List<Livro>();

            var comando = $"SELECT * FROM Livros WHERE Nome = '{nome}' AND Edicao = {edicao}";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                livros.Add(new Livro
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Autor = (string)sqlDataReader["Autor"],
                    Editora = (string)sqlDataReader["Editora"],
                    Edicao = (int)sqlDataReader["Edicao"],
                    Paginas = (int)sqlDataReader["Paginas"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return livros;
        }


        public async Task Remover(Guid id)
        {
            var comando = $"DELETE FROM Livros WHERE Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            await sqlConnection.CloseAsync();
        }
    }
}

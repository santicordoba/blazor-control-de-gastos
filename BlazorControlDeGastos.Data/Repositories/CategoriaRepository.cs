using BlazorControlDeGastos.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorControlDeGastos.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private SqlConfiguration _connectionString;

        public CategoriaRepository(SqlConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteCategoria(int id)
        {
            var db = dbConnection();
            var sql = @"DELETE FROM Categorias WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }

        public async Task<Categoria> GetCategoria(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT Id, DescCategoria FROM Categorias WHERE Id = @Id";
            return await db.QueryFirstOrDefaultAsync<Categoria>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Categoria>> GetAllCategorias()
        {
            var db = dbConnection();
            var sql = @"SELECT Id, DescCategoria FROM Categorias";
            return await db.QueryAsync<Categoria>(sql, new {});
        }

        public async Task<bool> InsertarCategoria(Categoria categoria)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO Categorias (DescCategoria) VALUES (@DescCategoria)";
            var result = await db.ExecuteAsync(sql, new { categoria.DescCategoria });
            return result > 0;
        }

        public async Task<bool> UpdateCategoria(Categoria categoria)
        {
            var db = dbConnection();
            var sql = @"UPDATE Categorias SET DescCategoria = @DescCategoria WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql, new { categoria.DescCategoria, categoria.Id });
            return result > 0;
        }
    }
}

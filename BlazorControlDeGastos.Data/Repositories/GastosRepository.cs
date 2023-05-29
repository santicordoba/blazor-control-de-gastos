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
    public class GastosRepository : IGastosRepository
    {
        private SqlConfiguration _connectionString;

        public GastosRepository(SqlConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> AddGasto(Gastos gasto)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO Gastos (Monto, CategoriaId, TipoGasto, Fecha) VALUES (@Monto, @CategoriaId, @TipoGasto, @Fecha)";
            var result = await db.ExecuteAsync(sql, new { gasto.Monto, gasto.CategoriaId, gasto.TipoGasto, gasto.Fecha });
            return result > 0;
        }

        public async Task<bool> DeleteGasto(int id)
        {
            var db = dbConnection();
            var sql = @"DELETE FROM Gastos WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }

        public async Task<IEnumerable<Gastos>> GetAllGastos()
        {
            var db = dbConnection();
            var sql = @"SELECT g.Id, Monto, CategoriaId, TipoGasto, Fecha, c.Id, c.DescCategoria FROM Gastos as g INNER JOIN Categorias as c ON (g.CategoriaId = c.Id)
                        ORDER BY g.Fecha";
            var result = await db.QueryAsync<Gastos, Categoria, Gastos>(sql,
            ((gasto, categoria) =>
            {
                gasto.Categoria = categoria;
                return gasto;
            }), new { }, splitOn:"Id");
            return result;
        }

        public async Task<Gastos> GetOneGasto(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM Gastos WHERE Id= @Id";
            return await db.QueryFirstOrDefaultAsync<Gastos>(sql, new { Id = id });
        }

        public async Task<bool> UpdateGasto(Gastos gasto)
        {
            var db = dbConnection();
            var sql = @"UPDATE Gastos SET Monto = @Monto, CategoriaId = @CategoriaId, TipoGasto = @TipoGasto, Fecha = @Fecha WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql, new { gasto.Monto, gasto.CategoriaId, gasto.TipoGasto, gasto.Fecha, gasto.Id });
            return result > 0;
        }
    }
}

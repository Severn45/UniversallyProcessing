using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversallyProcessing.Core.Models;
using Dapper;

namespace UniversallyProcessing.Core.Repository
{
    public class MakeRepository : IRepository<Make, int>
    {
        private readonly string _connectionString;
        public MakeRepository(string connectioString)
        {
            _connectionString = connectioString;
        }

        public async Task<IEnumerable<Make>> Get()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<Make>("SELECT * FROM dbo.CarMakes").ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Make>> GetAllMakes()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var result = await db.QueryAsync<Make>("SELECT * FROM dbo.CarMakes");
                return result.ToList();
            }
        }

        public async Task<Make> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<Make>("SELECT * FROM dbo.CarMakes where Id = @id", new { id }).ConfigureAwait(false);
            }
        }

        public async Task<Make> Create(Make task)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                const string query = "Insert into dbo.CarMakes (Name) output inserted.* VALUES (@Name)";
                return await db.QueryFirstOrDefaultAsync<Make>(query, task).ConfigureAwait(false);
            }
        }

        public async Task<Make> Update(Make task)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                const string query = "UPDATE dbo.CarMakes SET Name = @Name output inserted.* WHERE Id = @Id";
                return await db.QueryFirstOrDefaultAsync<Make>(query, task).ConfigureAwait(false);
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                const string sqlQuery = "DELETE FROM dbo.CarMakes WHERE Id = @id";
                await db.ExecuteAsync(sqlQuery, new { id }).ConfigureAwait(false);
            }
        }

        public Task<IEnumerable<Make>> Get(string userId)
        {
            return Get();
        }
    }
}

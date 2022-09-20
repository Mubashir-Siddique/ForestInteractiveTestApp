using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ForestInteractiveTestApp.IRepository;

namespace ForestInteractiveTestApp.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        IConnection _connection;
        public GenericRepository(IConnection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Model> Search<Model>(object parameters, string query)
        {
            using (SqlConnection conn = new SqlConnection(_connection.ConnectionString))
            {
                conn.Open();
                return conn.Query<Model>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public IEnumerable<Model> ExecuteQuery<Model>(object parameters, string query)
        {
            using (SqlConnection conn = new SqlConnection(_connection.ConnectionString))
            {
                conn.Open();
                return conn.Query<Model>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

    }
}

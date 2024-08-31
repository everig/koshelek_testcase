using Npgsql;
using System.Data;
using System.Data.Common;

namespace Infrastructure.DB
{
    public class DbConnectionFactory
    {
        public static async Task<DbConnection> GetDbConnection(string connectionString)
        {
            DbConnection connection = null;

            if (connectionString != null)
            {
                connection = new NpgsqlConnection(connectionString);
            }
            await connection.OpenAsync();
            await connection.CheckTables();
            return connection;
        } 
    }
}

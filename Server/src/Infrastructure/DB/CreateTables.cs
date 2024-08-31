using System.Data;
using System.Data.Common;

namespace Infrastructure.DB
{
    public static class CreateTables
    {
        public static async Task CheckTables(this DbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = $"CREATE TABLE IF NOT EXISTS Messages (" +
                $"Id INT NOT NULL PRIMARY KEY," +
                $"DateLabel TIMESTAMP NOT NULL," +
                $"Text TEXT)";
            await command.ExecuteNonQueryAsync();
        }
    }
}

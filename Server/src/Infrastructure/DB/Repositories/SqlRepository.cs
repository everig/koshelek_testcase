using Application.Common.Interfaces;
using Domain.Entities;
using System.Data;
using System.Data.Common;

namespace Infrastructure.DB.Repositories
{
    public abstract class SqlRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private string _connectionString;

        public SqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<DbConnection> GetOpenedConnection()
        {
            return await DbConnectionFactory.GetDbConnection(_connectionString);
        }

        public abstract Task Delete(int id);
        public abstract Task<TEntity> Get(int id);
        public abstract Task<IEnumerable<TEntity>> GetAll();
        public abstract Task Insert(TEntity entity);
        public abstract Task Update(TEntity entity);
    }
}

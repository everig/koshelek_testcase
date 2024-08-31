using Application.Common.Interfaces;
using Domain.Entities;
using Application.Common.Extensions;
using AutoMapper;


namespace Infrastructure.DB.Repositories
{
    public class MessageRepository : SqlRepository<Message>, IMessageRepository
    {
        private IMapper _mapper;
        public MessageRepository(string connectionString, IMapper mapper) : base(connectionString) 
        { 
            _mapper = mapper;
        }

        public override async Task Delete(int id)
        {
            using (var conn = await GetOpenedConnection())
            {
                var command = conn.CreateCommand();
                command.CommandText = $"DELETE * FROM Messages WHERE Id=@Id";
                command.AddParameter<int>($"@Id", id);
                await command.ExecuteNonQueryAsync();
            }
        }

        public override async Task<Message> Get(int id)
        {
            using (var conn = await GetOpenedConnection())
            {
                var command = conn.CreateCommand();
                command.CommandText = $"SELECT * FROM Messages WHERE Id=@Id";
                command.AddParameter<int>($"@Id", id);
                var reader = await command.ExecuteReaderAsync();
                return _mapper.Map<Message>(reader);
            }
        }

        public override async Task<IEnumerable<Message>> GetAll()
        {
            using (var conn = await GetOpenedConnection())
            {
                var result = new List<Message>();
                var command = conn.CreateCommand();
                command.CommandText = $"SELECT * FROM Messages;";
                var reader = await command.ExecuteReaderAsync();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(_mapper.Map<Message>(reader));
                    }
                    reader.Close();
                }                       
                return result; 
            }
        }

        public override async Task Insert(Message message)
        {
            using (var conn = await GetOpenedConnection())
            {
                var command = conn.CreateCommand();
                command.CommandText = $"INSERT INTO Messages (Id, DateLabel, Text) VALUES (@Id, @DateLabel, @Text) " +
                    $"ON CONFLICT (Id) DO UPDATE " +
                    $"SET DateLabel = @DateLabel, Text = @Text";
                command.AddParameter<int>($"@Id", message.No);
                command.AddParameter<DateTime>($"@DateLabel", message.DateLabel);
                command.AddParameter<string>($"@Text", message.Text);
                await command.ExecuteNonQueryAsync();
            }
        }

        public override async Task Update(Message message)
        {
            using (var conn = await GetOpenedConnection())
            {
                var command = conn.CreateCommand();
                command.CommandText = $"UPDATE Messages SET (DateLabel = @DateLabel, Text=@Text) WHERE Id=@Id";
                command.AddParameter<int>($"@Id", message.No);
                command.AddParameter<DateTime>($"@DateLabel", message.DateLabel);
                command.AddParameter<string>($"@Text", message.Text);
                await  command.ExecuteNonQueryAsync();
            }
        }
    }
}

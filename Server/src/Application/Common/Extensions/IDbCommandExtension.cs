using System.Data;

namespace Application.Common.Extensions
{
    public static class IDbCommandExtension
    {
        public static void AddParameter<T>(this IDbCommand command, string name, T value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}

using Application.Services.Messages.Queries.GetMessage;

namespace TestCase.SignalR
{
    public interface ITypedHubClient
    {
        Task Recieve(int messageNo, DateTime messageDate, string messageText);

    }
}

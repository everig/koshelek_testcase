using Microsoft.AspNetCore.SignalR;

namespace TestCase.SignalR
{
    public class MessageHub : Hub<ITypedHubClient>   
    {

        public async Task Recieve(int messageNo, DateTime messageDate, string messageText)
        {
            await Clients.All.Recieve(messageNo, messageDate, messageText);
        }
    }
}

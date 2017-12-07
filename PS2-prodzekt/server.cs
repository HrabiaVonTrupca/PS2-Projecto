using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;

namespace PS2_prodzekt
{
    public class ChatHub : Hub
    {
        public void Send(string message, string conID)
        {
            string userID = Context.ConnectionId;

            Clients.Client(conID).broadcastMessage(message);

            Debug.WriteLine("User " + userID + "send message: " + message);

        }

        public override Task OnConnected()
        {
            var name = Context.ConnectionId;
            Debug.WriteLine(name.ToString() + "  connected");

            return base.OnConnected();
        }
    }
}

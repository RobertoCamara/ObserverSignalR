using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace GatewayCommunication.Hubs
{
    public class GatewayCommunicationHub : Hub
    {
        /// <summary>
        /// Add a connection to the specified group
        /// </summary>
        /// <param name="groupName">Ex: tcp/udp</param>
        /// <returns>Task IGroupManager</returns>
        public Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// Remove a connection from the specified group
        /// </summary>
        /// <param name="groupName">Ex: tcp/udp</param>
        /// <returns>Task IGroupManager</returns>
        public Task LeaveGroup(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }

        public void SendMessage(string groupName, string message)
        {
            Clients.Group(groupName).sendMessage(message);
        }

        public void SendMessageOther(string groupName, string message)
        {
            Clients.Group(groupName).SendMessageOther(message);
        }
    }
}

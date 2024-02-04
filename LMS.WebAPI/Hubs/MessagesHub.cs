using LMS.Application.Common.HubMessagesModel;
using Microsoft.AspNetCore.SignalR;

namespace LMS.WebAPI.Hubs
{
    public static class HubConnections
    {
        public static Dictionary<string, List<string>> ChatUsers = new Dictionary<string, List<string>>();

        public static List<string> GetChatUserId(string name)
        {
            return ChatUsers[name];
        }
    }

    public class ChatHub : Hub
    {
        public ChatHub()
        {

        }

        public async Task SendNewMessageRefresh(MessageModel messageModel)
        {
            await Clients.User(messageModel.RecipientId.ToString()).SendAsync("SendNewMessageRefresh", messageModel);
        }

        public async Task UnreadchattingMessages(int recipientId)
        {
            await Clients.User(recipientId.ToString()).SendAsync("UnreadchattingMessages");
        }

        public async Task ApprovalsBooksOrders(List<string> usersId)
        {
            if (usersId.Count > 0)
            {
                await Clients.Users(usersId).SendAsync("ApprovalsBooksOrders");
            }
        }

        public override Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated
                && HubConnections.ChatUsers.ContainsKey(Context.User.Identity.Name)
                && !HubConnections.ChatUsers[Context.User.Identity.Name].Contains(Context.ConnectionId))
                HubConnections.ChatUsers[Context.User.Identity.Name].Add(Context.ConnectionId);
            else if (!string.IsNullOrEmpty(Context.User.Identity.Name))
                HubConnections.ChatUsers.Add(Context.User.Identity.Name, new List<string> { Context.ConnectionId });

            if (Context.User.IsInRole("Admin"))
            {
                Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
            }

            if (Context.User.IsInRole("Librarian"))
            {
                Groups.AddToGroupAsync(Context.ConnectionId, "Librarian");
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (!string.IsNullOrEmpty(Context.User.Identity.Name))
                HubConnections.ChatUsers.Remove(Context.User.Identity.Name);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendNewMessage(MessageModel chatMessageModel)
        {
            await Clients.User(chatMessageModel.RecipientId.ToString()).SendAsync("MessageReceived", chatMessageModel);
            await Clients.User(chatMessageModel.CreatedBy.ToString()).SendAsync("MessageReceived", chatMessageModel);
        }
        public async Task<string> GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}

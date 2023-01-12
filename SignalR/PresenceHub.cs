using ecommerceApi.Extensions;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ecommerceApi.SignalR
{
    public class PresenceHub : Hub
    {
        //TODO: Not yet fully implemented
        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUsername());
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUsername());
            await base.OnDisconnectedAsync(exception);
        }
    }
}

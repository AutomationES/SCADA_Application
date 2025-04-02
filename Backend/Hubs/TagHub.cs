using Microsoft.AspNetCore.SignalR;
using SCADA.Common.Models;

namespace SCADA.Backend.Hubs;

public class TagHub : Hub
{
    public async Task SubscribeToTag(int tagId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"tag-{tagId}");
    }
}
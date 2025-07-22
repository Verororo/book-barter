using BookBarter.Application.Auth.Constants;
using Microsoft.AspNetCore.SignalR;

namespace BookBarter.API.Hubs.UserIdProvider;

public class UserIdProvider : IUserIdProvider
{
    public virtual string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst(ClaimsNames.Id)?.Value.ToString()!;
    }
}

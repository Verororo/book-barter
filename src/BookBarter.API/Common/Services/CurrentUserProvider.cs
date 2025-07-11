using BookBarter.Application.Common.Interfaces;
using BookBarter.Domain.Exceptions;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace BookBarter.API.Common.Services;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated != true)
                return null;

            var idString = user.FindFirstValue("id");
            if (idString == null)
                return null;

            return int.Parse(idString);
        }
    }
}

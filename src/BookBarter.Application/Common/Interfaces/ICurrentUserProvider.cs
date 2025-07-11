
namespace BookBarter.Application.Common.Interfaces;

public interface ICurrentUserProvider
{
    int? UserId { get; }
}

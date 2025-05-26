

namespace BookBarter.Domain.Exceptions;
public class RepoMemberAbsentException : Exception
{
    public RepoMemberAbsentException(string message) : base(message) { }
}

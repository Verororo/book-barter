

namespace BookBarter.Domain.Exceptions;
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message) { }
    public EntityNotFoundException(string entityName, int id) : 
        base("Name: " + entityName + ", Id: " + id) { }
}

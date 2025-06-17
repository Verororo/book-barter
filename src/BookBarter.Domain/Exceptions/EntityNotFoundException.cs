

namespace BookBarter.Domain.Exceptions;
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message) { }
    public EntityNotFoundException(string entityName, int id) : 
        base("Entity instance not found. Name: " + entityName + ", Id: " + id) { }
    public EntityNotFoundException(string entityName, List<int> ids) :
        base("Multiple entity instances not found. Name: " + entityName + ", Ids: " + ids.ToString()) { }
}

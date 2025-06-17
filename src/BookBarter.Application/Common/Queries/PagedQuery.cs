
namespace BookBarter.Application.Common.Models;

public abstract class PagedQuery
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string OrderByProperty { get; set; } = default!;
    public string OrderDirection { get; set; } = default!;
}

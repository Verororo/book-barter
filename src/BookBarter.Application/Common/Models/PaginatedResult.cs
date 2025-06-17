using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Application.Common.Models;

public class PaginatedResult<T> where T : class
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public List<T> Items { get; set; } = [];
}

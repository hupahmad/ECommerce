using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class QueryObject
    {
        public string? SortBy { get; set; } = null;

        public bool IsDecSending { get; set; } = false;

        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
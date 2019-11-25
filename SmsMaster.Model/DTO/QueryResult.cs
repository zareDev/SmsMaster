using System;
using System.Collections.Generic;
using System.Text;

namespace SmsMaster.Model.DTO
{
    public class QueryResult<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}

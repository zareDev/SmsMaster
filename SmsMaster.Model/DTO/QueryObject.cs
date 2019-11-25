using System;
using System.Collections.Generic;
using System.Text;

namespace SmsMaster.Model.DTO
{
    public class QueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}

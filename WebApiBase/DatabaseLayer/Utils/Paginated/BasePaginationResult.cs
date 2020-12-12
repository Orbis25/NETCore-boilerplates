using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Utils.Paginated
{
    public class BasePaginationResult<T>
    {
        public int ActualPage { get; set; } = 1;
        public int Qyt { get; set; }
        public int PageTotal { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}

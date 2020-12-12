using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.ViewModels.Commons.Paginated
{
    public class BasePaginated
    {
        /// <summary>
        /// Actual page
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Quantity by page
        /// </summary>
        public int Qyt { get; set; } = 10;
    }
}

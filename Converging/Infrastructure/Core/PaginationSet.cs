using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Converging.Infrastructure.Core
{
    public class PaginationSet<T>
    {
        public int Page { set; get; }

        public int TotalPages { set; get; }

        public int Count
        {
            get
            {
                return (Items != null) ? Items.Count() : 0;
            }

        }

        public int TotalCount { set; get; }

        public IEnumerable<T> Items { set; get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodPlan.Core
{
    public class QueryParameters
    {
        private int _pageIndex;
        private int _pageSize;

        public virtual int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value >= 0 ? value : 0;
        }
        public virtual int PagSize
        {
            get => _pageSize;
            set => _pageSize = value >= 1 ? value : 100;
        }
        public string Ordays { get; set; }
        public string OrdayOk { get; set; }

        public string Fields { get; set; }
        public string FieldOk { get; set; }
    }
}

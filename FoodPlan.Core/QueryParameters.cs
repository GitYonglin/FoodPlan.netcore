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
            set => _pageIndex = value >= 1 ? value : 1;
        }
        public virtual int PageSize
        {
            get => _pageSize;
            set => _pageSize = value >= 1 ? value : 100;
        }
        public string Ordays { get; set; }
        public string OrdayOk { get; set; }

        public string Fields { get; set; }
        public string FieldOk { get; set; }
        public string Search { get; set; }
    }
}

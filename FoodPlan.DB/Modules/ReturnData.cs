using System;
using System.Collections.Generic;
using System.Text;

namespace FoodPlan.DB.Modules
{
    public class ReturnData<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = false;
        public string Error { get; set; }
    }
    public class PageReturnData<T>: ReturnData<T>
    {
        public long Count { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FoodPlan.Core.Service
{
    public class TypeHelperReturnData<T>
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public T Ok { get; set; }
    }
}

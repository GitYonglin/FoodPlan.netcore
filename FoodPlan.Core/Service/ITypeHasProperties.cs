using System;
using System.Collections.Generic;
using System.Text;

namespace FoodPlan.Core.Service
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
        TypeHelperReturnData<string> Orday<T>(string orday);
        TypeHelperReturnData<string> Projections<T>(string projections);
    }

}

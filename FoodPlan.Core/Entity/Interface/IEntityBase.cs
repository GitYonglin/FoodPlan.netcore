using System;
using System.Collections.Generic;
using System.Text;

namespace FoodPlan.Core.Entity
{
    public interface IEntityBase
    {
        // ReSharper disable once InconsistentNaming
        Guid _id { get; set; }
    }
}

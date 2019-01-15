using System;
using System.Collections.Generic;
using System.Text;

namespace FoodPlan.Core.Entity
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }
}

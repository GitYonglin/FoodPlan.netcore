using FoodPlan.Core;
using FoodPlan.Core.Entity;
using FoodPlan.DB.Mongo.IRepository;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable once IdentifierTypo
namespace FoodPlan.DB.Mongo.Repository
{
    public class SetMealsRepository: BaseRepository<SetMeals>, ISetMealsRepository
    {
        public SetMealsRepository(IOptions<DBSettings> dBSettings) : base(dBSettings)
        {
            Context = Datebase.ContactSetMeals;
        }
    }
}

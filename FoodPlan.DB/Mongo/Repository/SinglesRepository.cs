using FoodPlan.Core;
using FoodPlan.DB.Mongo.IRepository;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlan.DB.Mongo.Repository
{
    public class SinglesRepository: BaseRepository<Core.Entity.Single>, ISinglesRepository
    {
        public SinglesRepository(IOptions<DBSettings> dBSettings) : base(dBSettings)
        {
            _context = _datebase.ContactSingles;
        }
    }
}

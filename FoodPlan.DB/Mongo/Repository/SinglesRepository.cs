using FoodPlan.Core;
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
    public class SinglesRepository: BaseRepository<Core.Entity.Single>, ISinglesRepository
    {
        public SinglesRepository(IOptions<DBSettings> dBSettings) : base(dBSettings)
        {
            Context = Datebase.ContactSingles;
        }
    }
}

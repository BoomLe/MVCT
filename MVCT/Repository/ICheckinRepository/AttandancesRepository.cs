using MVCT.Data;
using MVCT.IRepository;
using MVCT.Models.Attandance;

namespace MVCT.Repository.ICheckinRepository
{
    public class AttandancesRepository : Repository<Attandances>, IAttandancesRepository
    {
        private readonly ApplicationDbContext _db;
        public AttandancesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public  void Update(Attandances obj)
        {
             _db.Attandances.Update(obj);
        }
    }
}

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

   

        public async Task<Attandances> UpdatedAsync(Attandances attandances)
        {
            dbSet.Update(attandances);
            await SaveAsync();

            return attandances;
        }
    }
}

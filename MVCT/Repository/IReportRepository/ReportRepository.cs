using MVCT.Data;
using MVCT.IRepository;
using MVCT.Models.UserReport;

namespace MVCT.Repository.IReportRepository
{
    public class ReportRepository : Repository<UserReports> , IReportRepository
    {
        private readonly ApplicationDbContext _db;
        public ReportRepository(ApplicationDbContext db) : base (db)
        {
            
        }

        public async Task<UserReports> Update(UserReports userReports)
        {
            _db.Update(userReports);
          await  _db.SaveChangesAsync();
            return userReports;
        }
    }
}

using MVCT.IRepository;
using MVCT.Models.Attandance;
using MVCT.Models.UserReport;

namespace MVCT.Repository.IReportRepository
{
    public interface IReportRepository : IRepository<UserReports>
    {
        Task<UserReports> Update(UserReports userReports);
    }
}

using MVCT.IRepository;
using MVCT.Models.Attandance;

namespace MVCT.Repository.ICheckinRepository
{
 
    public interface IAttandancesRepository : IRepository<Attandances>
    {
        void Update(Attandances obj);


    }
}

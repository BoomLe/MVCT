using Microsoft.EntityFrameworkCore;
using MVCT.Data;
using MVCT.IRepository;
using MVCT.Models.Attandance;

using MVCT.Services.ICheckinservice;

namespace MVCT.Services
{
    public class CheckinService : ICheckinService
    {
        private readonly IRepository<Attandances> _repository;
        private readonly ApplicationDbContext _db;

        public CheckinService(IRepository<Attandances> repository)
        {
            _repository = repository;
        }
        public async Task<List<Attandances>> GetCheckins()
        {
            return await _repository.GetAll()
                .Include(c => c.User)
                    //.ThenInclude(c => c.Memberships)
                .Where(x => x.CreatedAt.Date == DateTime.Today)
                .ToListAsync();
        }

        public async Task<Attandances> SaveAsync(Attandances checkin)
        {
            checkin.UpdateAt = DateTime.Now;
            checkin.CreatedAt = DateTime.Now;

            return await _repository.CreateAsync(checkin);
        }
        public async Task<bool> AlreadyCheckedIn(int memberId)
        {
            return await _repository.GetAll().AnyAsync(x => x.Id == memberId && x.CreatedAt.Date == DateTime.Today);
        }
        public async Task<Attandances?> GetById(int id)
        {
            return await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool?> UpdateAsync(int id, Attandances checkin)
        {
            var exisingCheckin = await _repository.GetSingleAsync(id);
            if (exisingCheckin is null)
            {
                return null;
            }
            exisingCheckin.UpdateAt = DateTime.Now;
            exisingCheckin.Id = checkin.Id;
            //exisingCheckin.Status = checkin.Status;
            exisingCheckin.DataTime = checkin.DataTime;
            return await _repository.UpdateAsync(exisingCheckin);
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            var exisingCheckin = await _repository.GetSingleAsync(id);
            if (exisingCheckin is null)
            {
                return null;
            }
            return await _repository.DeleteAsync(exisingCheckin);
        }
    }
}

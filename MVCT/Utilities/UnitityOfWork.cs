using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MVCT.Data;
using MVCT.Repository.ICheckinRepository;

namespace Book.Unitity
{
    public class UnitityOfWork : IUnitityOfWork
    {
        private readonly ApplicationDbContext _db;

      
        public IAttandancesRepository AttandancesRepo { get; private set;}
        public UnitityOfWork(ApplicationDbContext db)
        {
            _db = db;
           
            AttandancesRepo = new AttandancesRepository(_db);
        }

        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
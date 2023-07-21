using MVCT.Repository.ICheckinRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Book.Unitity
{
    public interface IUnitityOfWork
    {
        IAttandancesRepository AttandancesRepo {get;}
       
        void Save();
    }
}
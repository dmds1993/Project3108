using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IPBIRepository
    {
        Task<PBIEntity> GetByIdAsync(int pBIId);
        Task<PBIEntity> GetByNameAsync(string name);
        Task<PBIEntity> InsertAsync(PBIEntity pbi);
        Task<int> SaveChangesAsync();
        Task UpdateAsync(PBIEntity existingPBI);
        void Delete(PBIEntity pbi);
        Task<IEnumerable<PBIEntity>> GetAllAsync();
    }
}

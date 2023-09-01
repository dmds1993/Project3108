using Domain.Entities;
using Domain.Infra.SqlServer.DependecyInjection;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infra.SqlServer.Repository
{
    public class PBIRepository : IPBIRepository
    {
        private readonly AppDbContext _dbContext;

        public PBIRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PBIEntity> InsertAsync(PBIEntity pbi)
        {
            _dbContext.PBIs.Add(pbi);
            return pbi;
        }

        public async Task UpdateAsync(PBIEntity pbi)
        {
            _dbContext.PBIs.Update(pbi);
        }

        public async Task<PBIEntity> GetByNameAsync(string name)
        {
            return await _dbContext.PBIs
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        public List<PBIEntity> GetAllPBIs()
        {
            return _dbContext.PBIs.ToList();
        }

        public PBIEntity GetPBIById(int id)
        {
            return _dbContext.PBIs.FirstOrDefault(pbi => pbi.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PBIEntity> GetByIdAsync(int pBIId)
        {
            return await _dbContext.PBIs
                .Include(pbi => pbi.Tasks)
                .FirstOrDefaultAsync(pbi => pbi.Id == pBIId);
        }

        public void Delete(PBIEntity pbi)
        {
            _dbContext.PBIs.Remove(pbi);       
        }

        public async Task<IEnumerable<PBIEntity>> GetAllAsync()
        {
            return await _dbContext.PBIs.Include(pbi => pbi.Tasks).ToListAsync();
        }
    }
}
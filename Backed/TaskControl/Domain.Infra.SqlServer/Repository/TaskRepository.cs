using Domain.Entities;
using Domain.Infra.SqlServer.DependecyInjection;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Domain.Infra.SqlServer.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _dbContext;

        public TaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Insert(TaskEntity task)
        {
            await _dbContext.Tasks.AddAsync(task);
        }

        public async Task<TaskEntity> GetByIdAsync(int taskId)
        {
            return await _dbContext.Tasks.FindAsync(taskId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Update(TaskEntity task)
        {
            _dbContext.Tasks.Update(task);
        }
        public async Task<TaskEntity> GetTaskByIdAsync(int taskId)
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(task => task.Id == taskId);
        }

        public void Delete(TaskEntity taskEntity)
        {
            _dbContext.Tasks.Remove(taskEntity);
        }
    }
}
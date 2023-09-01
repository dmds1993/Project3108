using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface ITaskRepository
    {
        Task<TaskEntity> GetTaskByIdAsync(int taskId);
        Task Insert(TaskEntity taskEntity);
        Task<int> SaveChangesAsync();
        void Delete(TaskEntity existingTask);
        void Update(TaskEntity task);
        Task<TaskEntity> GetByIdAsync(int taskId);
    }
}

using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface ITaskService
    {
        Task<ServiceResponse<bool>> CreateAsync(List<TaskModel> taskModels);
        Task<ServiceResponse<bool>> UpdateAsync(int taskId, TaskModel taskModel);
        Task<ServiceResponse<bool>> DeleteAsync(int taskId);
        Task<TaskModel> GetByIdAsync(int id);
    }
}

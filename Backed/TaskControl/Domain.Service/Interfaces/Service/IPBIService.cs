using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.Service
{
    public interface IPBIService
    {
        Task<ServiceResponse<bool>> CreatePbi(PBIModel pbiModel);
        Task<ServiceResponse<bool>> CreateWithTasks(PBIModel pbiModel, List<TaskModel> taskModels);
        Task<PBIModel> GetByIdAsync(int id);
        Task<ServiceResponse<bool>> RemoveAsync(int id);
        Task<ServiceResponse<bool>> UpdateAsync(int id, PBIModel pbiModel);
        Task<List<PBIModel>> GetPBIsWithTasksAsync();
    }
}

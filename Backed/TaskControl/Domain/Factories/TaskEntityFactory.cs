using Domain.Entities;
using Domain.Models;

namespace Domain.Factories
{
    public static class TaskEntityFactory
    {
        public static TaskEntity CreateFromModel(TaskModel taskModel)
        {
            var taskEntity = new TaskEntity
            {
                Name = taskModel.Name,
                Description = taskModel.Description,
                Deadline = taskModel.Deadline,
                Image = taskModel.Image,
                PBIId = taskModel.PBIId,
            };

            return taskEntity;
        }
    }
}

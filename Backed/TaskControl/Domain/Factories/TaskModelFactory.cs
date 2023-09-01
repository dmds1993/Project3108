using Domain.Entities;
using Domain.Models;

namespace Domain.Factories
{
    public static class TaskModelFactory
    {
        public static TaskModel CreateFromEntity(TaskEntity entity)
        {
            var model = new TaskModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Deadline = entity.Deadline,
                Image = entity.Image
            };

            return model;
        }
    }
}

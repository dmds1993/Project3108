using Domain.Entities;
using Domain.Models;
using System.Linq;

namespace Domain.Factories
{
    public static class PBIModelFactory
    {
        public static PBIModel CreateFromEntity(PBIEntity entity)
        {
            var model = new PBIModel
            {
                Id = entity.Id,
                Name = entity.Name
            };

            if (entity.Tasks != null && entity.Tasks.Any())
            {
                model.TaskModels = entity.Tasks.Select(taskEntity => TaskModelFactory.CreateFromEntity(taskEntity));
            }

            return model;
        }
    }
}

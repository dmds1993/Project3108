using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;

namespace Domain.Factories
{
    public static class PBIEntityFactory
    {
        public static PBIEntity CreateFromModel(PBIModel pbiModel)
        {
            var pbiEntity = new PBIEntity
            {
                Name = pbiModel.Name,
                Tasks = new List<TaskEntity>()
            };

            if (pbiModel.TaskModels != null)
            {
                foreach (var taskModel in pbiModel.TaskModels)
                {
                    pbiEntity.Tasks.Add(TaskEntityFactory.CreateFromModel(taskModel));
                }
            }

            return pbiEntity;
        }

        public static PBIEntity CreateWithoutTasksFromModel(PBIModel pbiModel)
        {
            var pbiEntity = new PBIEntity
            {
                Name = pbiModel.Name,
                Tasks = new List<TaskEntity>()
            };

            return pbiEntity;
        }
    }
}

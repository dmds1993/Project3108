using Domain.Factories;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPBIRepository _pbiRepository;

        public TaskService(ITaskRepository taskRepository, IPBIRepository pbiRepository)
        {
            _taskRepository = taskRepository;
            _pbiRepository = pbiRepository;
        }

        public async Task<ServiceResponse<bool>> CreateAsync(List<TaskModel> taskModels)
        {
            var response = new ServiceResponse<bool>();

            foreach (var taskModel in taskModels)
            {
                if (!ValidateTaskModel(taskModel, response))
                {
                    return response;
                }

                var pbi = await _pbiRepository.GetByIdAsync(taskModel.PBIId);
                if (pbi == null)
                {
                    response.Success = false;
                    response.Message = $"PBI with ID {taskModel.PBIId} not found.";
                    return response;
                }

                var taskEntity = TaskEntityFactory.CreateFromModel(taskModel);
                await _taskRepository.Insert(taskEntity);
            }

            await _taskRepository.SaveChangesAsync();

            response.Success = true;
            response.Message = "Tasks created successfully";
            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateAsync(int taskId, TaskModel taskModel)
        {
            var response = new ServiceResponse<bool>();

            var existingTask = await _taskRepository.GetTaskByIdAsync(taskId);
            if (existingTask == null)
            {
                response.Success = false;
                response.Message = $"Task with ID {taskId} not found.";
                return response;
            }

            if (!ValidateTaskModel(taskModel, response))
            {
                return response;
            }

            existingTask.Name = taskModel.Name;
            existingTask.Description = taskModel.Description;
            existingTask.Deadline = taskModel.Deadline;
            existingTask.Image = taskModel.Image;

            await _taskRepository.SaveChangesAsync();

            response.Success = true;
            response.Message = "Task updated successfully";
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int taskId)
        {
            var response = new ServiceResponse<bool>();

            var existingTask = await _taskRepository.GetTaskByIdAsync(taskId);
            if (existingTask == null)
            {
                response.Success = false;
                response.Message = $"Task with ID {taskId} not found.";
                return response;
            }

            _taskRepository.Delete(existingTask);
            await _taskRepository.SaveChangesAsync();

            response.Success = true;
            response.Message = "Task deleted successfully";
            return response;
        }

        private bool ValidateTaskModel(TaskModel model, ServiceResponse<bool> response)
        {
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(model, new ValidationContext(model), validationResults, true);

            if (!isValid)
            {
                response.Success = false;
                response.Message = "Validation error in Task model";
                response.Errors = validationResults;
            }

            return isValid;
        }

        public async Task<TaskModel> GetByIdAsync(int id)
        {
            var taskEntity = await _taskRepository.GetByIdAsync(id);

            if (taskEntity != null)
            {
                return TaskModelFactory.CreateFromEntity(taskEntity);
            }

            return null;
        }
    }
}

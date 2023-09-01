using Domain.Factories;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Domain.Services
{
    public class PBIService : IPBIService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPBIRepository pBIRepository;
        public PBIService(IUnitOfWork unitOfWork, IPBIRepository pBIRepository)
        {
            this.unitOfWork = unitOfWork;
            this.pBIRepository = pBIRepository;
        }

        public async Task<ServiceResponse<bool>> CreatePbi(PBIModel pbiModel)
        {
            var response = new ServiceResponse<bool>();

            if (pbiModel == null || string.IsNullOrWhiteSpace(pbiModel.Name))
            {
                response.Success = false;
                response.Message = "PBI model is null";
                return response;
            }

            var existingPBI = await pBIRepository.GetByNameAsync(pbiModel.Name);
            if (existingPBI != null)
            {
                response.Success = false;
                response.Message = "PBI with the same name already exists";
                return response;
            }

            var pbiEntity = PBIEntityFactory.CreateWithoutTasksFromModel(pbiModel);

            await pBIRepository.InsertAsync(pbiEntity);
            await pBIRepository.SaveChangesAsync();

            response.Success = true;
            response.Message = "PBI created successfully";
            response.Data = true;
            return response;
        }

        public async Task<List<PBIModel>> GetPBIsWithTasksAsync()
        {
            var pbis = await pBIRepository.GetAllAsync();
            var pbiWithTasksList = new List<PBIModel>();

            foreach (var pbiEntity in pbis)
            {
                var pbiWithTasks = new PBIModel
                {
                    Id = pbiEntity.Id,
                    Name = pbiEntity.Name,
                    TaskModels = pbiEntity.Tasks.Select(taskEntity => TaskModelFactory.CreateFromEntity(taskEntity)).ToList()
                };

                pbiWithTasksList.Add(pbiWithTasks);
            }

            return pbiWithTasksList;
        }



        public async Task<ServiceResponse<bool>> CreateWithTasks(PBIModel pbiModel, List<TaskModel> taskModels)
        {
            var response = new ServiceResponse<bool>();

            if (!ValidatePBIModel(pbiModel, response))
            {
                return response;
            }
            var pbiEntity = PBIEntityFactory.CreateFromModel(pbiModel);

            var success = await unitOfWork.UpInsertAsync(pbiEntity);

            response.Success = success;
            if (success)
            {
                response.Message = "PBI and tasks created successfully";
            }
            else
            {
                response.Message = "Failed to create PBI and tasks";
            }

            return response;
        }

        public async Task<PBIModel> GetByIdAsync(int id)
        {
            var pbiEntity = await pBIRepository.GetByIdAsync(id);

            if (pbiEntity != null)
            {
                return PBIModelFactory.CreateFromEntity(pbiEntity);
            }

            return null;
        }

        public async Task<ServiceResponse<bool>> RemoveAsync(int id)
        {
            var response = new ServiceResponse<bool>();

            if (id <= 0)
            {
                response.Data = false;
                response.Message = "Invalid PBI ID";
                return response;
            }

            var existingPBI = await pBIRepository.GetByIdAsync(id);
            if (existingPBI == null)
            {
                response.Data = false;
                response.Message = "PBI not found";
                return response;
            }

            if (existingPBI.Tasks != null && existingPBI.Tasks.Any())
            {
                response.Data = false;
                response.Message = "Cannot remove PBI with associated tasks";
                return response;
            }

            pBIRepository.Delete(existingPBI);

            await pBIRepository.SaveChangesAsync();

            response.Success = true;

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdateAsync(int id, PBIModel pbiModel)
        {
            var response = new ServiceResponse<bool>();

            var existingPBI = await pBIRepository.GetByIdAsync(id);

            if (existingPBI == null)
            {
                response.Message = $"PBI with ID {id} not found";
                return response;
            }

            existingPBI.Name = pbiModel.Name;

            await pBIRepository.UpdateAsync(existingPBI);

            await pBIRepository.SaveChangesAsync();

            response.Data = true;
            response.Message = "PBI updated successfully";
            return response;
        }


        private bool ValidatePBIModel(PBIModel model, ServiceResponse<bool> response)
        {
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(model, new ValidationContext(model), validationResults, true);

            if (!isValid)
            {
                response.Data = false;
                response.Message = "Validation error in PBI model";
                response.Errors = validationResults;
            }

            return isValid;
        }
    }
}

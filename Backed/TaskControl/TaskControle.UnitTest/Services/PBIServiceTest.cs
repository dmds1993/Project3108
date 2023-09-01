using Domain.Entities;
using Domain.Factories;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.Models;
using Domain.Services;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests.Services
{
    public class PBIServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IPBIRepository> _pbiRepositoryMock = new();
        private readonly IPBIService _pbiService;

        public PBIServiceTests()
        {
            _pbiService = new PBIService(_unitOfWorkMock.Object, _pbiRepositoryMock.Object);
        }

        [Fact]
        public async Task CreatePbi_ValidModel_Success()
        {
            var pbiModel = new PBIModel { Name = "Test PBI" };
            var pbiEntity = new PBIEntity { Name = pbiModel.Name };

            _pbiRepositoryMock.Setup(r => r.InsertAsync(It.IsAny<PBIEntity>())).Callback((PBIEntity entity) => pbiEntity = entity);

            var response = await _pbiService.CreatePbi(pbiModel);

            Assert.True(response.Success);
            Assert.Equal("PBI created successfully", response.Message);
            Assert.True(response.Data);

            _pbiRepositoryMock.Verify(r => r.InsertAsync(It.IsAny<PBIEntity>()), Times.Once);
        }

        [Fact]
        public async Task CreatePbi_NullModel_ReturnsError()
        {
            var response = await _pbiService.CreatePbi(null);

            Assert.False(response.Success);
            Assert.Equal("PBI model is null", response.Message);
        }

        [Fact]
        public async Task CreatePbi_InvalidModel_ReturnsValidationErrors()
        {
            var pbiModel = new PBIModel();

            var response = await _pbiService.CreatePbi(pbiModel);

            Assert.False(response.Success);
            Assert.Equal("Validation error in PBI model", response.Message);
            Assert.NotNull(response.Errors);
            Assert.True(response.Errors.Any(error => error.MemberNames.Contains("Name")));
        }

        [Fact]
        public async Task CreateWithTasks_ValidInput_SuccessResponse()
        {
            var pbiModel = new PBIModel { Name = "Test PBI", TaskModels = new List<TaskModel>() };
            _unitOfWorkMock.Setup(uow => uow.UpInsertAsync(It.IsAny<PBIEntity>())).ReturnsAsync(true);

            var response = await _pbiService.CreateWithTasks(pbiModel, new List<TaskModel>());

            Assert.True(response.Success);
            Assert.Equal("PBI and tasks created successfully", response.Message);
        }

        [Fact]
        public async Task RemoveAsync_PBIWithTasks_FailureResponse()
        {
            var pbiId = 1;
            var existingPBI = new PBIEntity { Id = pbiId, Name = "Test PBI", Tasks = new List<TaskEntity> { new TaskEntity() } };
            _pbiRepositoryMock.Setup(repo => repo.GetByIdAsync(pbiId)).ReturnsAsync(existingPBI);

            var response = await _pbiService.RemoveAsync(pbiId);

            Assert.False(response.Success);
            Assert.Equal("Cannot remove PBI with associated tasks", response.Message);
        }

        [Fact]
        public async Task CreateWithTasks_InvalidPBIModel_ValidationError()
        {
            var pbiModel = new PBIModel();

            var response = await _pbiService.CreateWithTasks(pbiModel, new List<TaskModel>());

            Assert.False(response.Success);
            Assert.Equal("Validation error in PBI model", response.Message);
            Assert.NotEmpty(response.Errors);
        }

        [Fact]
        public async Task CreateWithTasks_NonExistentPBI_FailureResponse()
        {
            var pbiModel = new PBIModel { Name = "Test PBI", TaskModels = new List<TaskModel>() };

            _pbiRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PBIEntity)null);

            var response = await _pbiService.CreateWithTasks(pbiModel, new List<TaskModel>());

            Assert.False(response.Success);
            Assert.Equal("Failed to create PBI and tasks", response.Message);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingPBI_ReturnsPBIModel()
        {
            var existingPBIId = 1;
            var existingPBIEntity = new PBIEntity { Id = existingPBIId, Name = "Existing PBI" };
            _pbiRepositoryMock.Setup(repo => repo.GetByIdAsync(existingPBIId)).ReturnsAsync(existingPBIEntity);

            var result = await _pbiService.GetByIdAsync(existingPBIId);

            Assert.NotNull(result);
            Assert.Equal(existingPBIId, result.Id);
            Assert.Equal(existingPBIEntity.Name, result.Name);
        }
    }
}

using Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Domain.Tests.Models
{
    public class TaskModelTests
    {
        [Fact]
        public void Name_ShouldHaveRequiredAttribute()
        {
            var propertyInfo = typeof(TaskModel).GetProperty(nameof(TaskModel.Name));
            var attribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false)[0] as RequiredAttribute;

            Assert.True(attribute != null);
        }

        [Fact]
        public void Description_ShouldHaveRequiredAttribute()
        {
            var propertyInfo = typeof(TaskModel).GetProperty(nameof(TaskModel.Description));
            var attribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false)[0] as RequiredAttribute;

            Assert.True(attribute != null);
        }

        [Fact]
        public void Deadline_ShouldHaveRequiredAttribute()
        {
            var propertyInfo = typeof(TaskModel).GetProperty(nameof(TaskModel.Deadline));
            var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false)[0] as RequiredAttribute;

            Assert.True(requiredAttribute != null);

            var dataTypeAttribute = propertyInfo.GetCustomAttributes(typeof(DataTypeAttribute), false)[0] as DataTypeAttribute;

            Assert.True(requiredAttribute != null);
            Assert.Equal(DataType.DateTime, dataTypeAttribute.DataType);
        }

        [Fact]
        public void Image_ShouldHaveRequiredAttribute()
        {
            var propertyInfo = typeof(TaskModel).GetProperty(nameof(TaskModel.Image));
            var attribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false)[0] as RequiredAttribute;

            Assert.True(attribute != null);
        }

        [Fact]
        public void PBIId_ShouldHaveRequiredAttribute()
        {
            var propertyInfo = typeof(TaskModel).GetProperty(nameof(TaskModel.PBIId));
            var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false)[0] as RequiredAttribute;

            Assert.True(requiredAttribute != null);

            var rangeAttribute = propertyInfo.GetCustomAttributes(typeof(RangeAttribute), false)[0] as RangeAttribute;

            Assert.True(rangeAttribute != null);
            Assert.Equal(1, rangeAttribute.Minimum);
            Assert.Equal(int.MaxValue, rangeAttribute.Maximum);
        }
    }
}

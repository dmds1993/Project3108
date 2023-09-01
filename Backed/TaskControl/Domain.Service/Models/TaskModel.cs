using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TaskModel
    {
        [Required(ErrorMessage = "Task name is required")]
        public string Name { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Task description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Task deadline is required")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format")]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Task image is required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "PBI ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "PBI ID must be greater than 0")]
        public int PBIId { get; set; }
    }
}

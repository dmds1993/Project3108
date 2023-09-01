using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PBIModel
    {
        [Required(ErrorMessage = "PBI name is required")]
        public string Name { get; set; }
        public int Id { get; set; }
        public IEnumerable<TaskModel> TaskModels { get; set; }
    }
}

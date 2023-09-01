using System.Collections.Generic;

namespace Domain.Entities
{
    public class PBIEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }

}
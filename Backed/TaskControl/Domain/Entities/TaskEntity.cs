using System;

namespace Domain.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Image { get; set; }

        public int PBIId { get; set; }
        public PBIEntity PBI { get; set; }
    }
}
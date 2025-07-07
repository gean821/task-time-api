using Api_IngaTasks.Application.Dtos.Task;
using Api_IngaTasks.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Dtos.Project
{
    public class ProjectDto
    {
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public required ICollection<TaskEntityDto> Tasks { get; set; }
        public Guid Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

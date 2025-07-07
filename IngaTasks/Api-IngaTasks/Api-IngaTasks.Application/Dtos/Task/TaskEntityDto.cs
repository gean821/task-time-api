using Api_IngaTasks.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Dtos.Task
{
    public class TaskEntityDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string ProjectName { get; set; }
        public string? CollaboratorName { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

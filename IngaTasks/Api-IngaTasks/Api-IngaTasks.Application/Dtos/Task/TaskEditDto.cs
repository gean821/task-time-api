using Api_IngaTasks.Application.Dtos.Project;
using Api_IngaTasks.Application.Dtos.TimeTracker;
using Api_IngaTasks.Application.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Dtos.Task
{
    public class TaskEditDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public Guid? CollaboratorId { get; set; }
        public Guid ProjectId { get; set; }
        public required ProjectDto Project { get; set; }
        public required ICollection<TimeTrackerDto> TimeTrackers { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
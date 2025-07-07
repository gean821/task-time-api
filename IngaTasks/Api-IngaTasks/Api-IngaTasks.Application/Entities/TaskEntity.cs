using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public required Project Project { get; set; }
        public required ICollection<TimeTracker> TimeTrackers { get; set; }
        public Collaborator? Collaborator { get; set; }
        public Guid? CollaboratorId { get; set; }

    }
}

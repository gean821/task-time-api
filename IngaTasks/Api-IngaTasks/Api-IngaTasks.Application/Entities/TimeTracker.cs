using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Entities
{
    public class TimeTracker
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string TimeZoneId { get; set; }
        public Guid TaskId { get; set; }
        public required Collaborator Collaborator { get; set; }  
        public Guid? CollaboratorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt {get; set;}
        public required TaskEntity Task { get; set; }
    }
}

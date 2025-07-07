using Api_IngaTasks.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Dtos.TimeTracker
{
    public class TimeTrackerDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string TimeZoneId { get; set; }
        public Guid TaskId { get; set; }
        
        public Guid? CollaboratorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public required TaskEntity Task { get; set; }
    }
}

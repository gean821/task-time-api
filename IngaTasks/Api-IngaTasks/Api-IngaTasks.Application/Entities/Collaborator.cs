using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Entities
{
    public class Collaborator
    {
         public Guid Id { get; set; }
        public required string Name { get; set; }    
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public required ApplicationUser ApplicationUser { get; set; }
        public required Guid ApplicationUserId { get; set; }
        public TaskEntity? Task { get; set; } 
    }
}

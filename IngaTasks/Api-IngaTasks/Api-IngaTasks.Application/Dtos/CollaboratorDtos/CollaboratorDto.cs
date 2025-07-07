using Api_IngaTasks.Application.Dtos.Task;
using Api_IngaTasks.Application.Dtos.User;
using Api_IngaTasks.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Dtos.Collaborator
{
    public class CollaboratorDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public required UserDto User { get; set; }
        public required Guid UserId { get; set; }
        public TaskEntityDto? Task { get; set; }
    }
}

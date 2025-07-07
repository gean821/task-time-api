using Api_IngaTasks.Application.Dtos.User;
using Api_IngaTasks.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Dtos.Collaborator
{
    public class CollaboratorCreateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public required UserDto User { get; set; }
        public required Guid UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Application.Dtos.User
{
    public class UserCreateDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
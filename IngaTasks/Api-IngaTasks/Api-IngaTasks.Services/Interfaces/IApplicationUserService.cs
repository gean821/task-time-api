using Api_IngaTasks.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Services.Interfaces
{
    public interface IApplicationUserService 
    {
        public Task<UserDto> AddUser(UserCreateDto dto);
        public Task<UserDto> UpdateUser(UserUpdateDto dto);
        public Task<UserDto> DeleteUser(UserDto dto);
        public Task<List<UserDto>> ListUsers();
        public Task<UserDto> BuscarPorId(Guid id);
    }
}

using Api_IngaTasks.Application.Dtos.User;
using Api_IngaTasks.Application.Entities;
using Api_IngaTasks.Infraestructure.Interfaces;
using Api_IngaTasks.Infraestructure.Repository;
using Api_IngaTasks.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<UserCreateDto> _createValidator;
        private readonly IValidator<UserUpdateDto> _updateValidator;

        public ApplicationUserService(
            UserManager<ApplicationUser> userManager,
            UserManager<UserUpdateDto> updateUser,
            IValidator<UserUpdateDto> updateValidator,
            IValidator<UserCreateDto> createValidator)
        {
            _userManager = userManager;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<UserDto> AddUser(UserCreateDto dto)
        {
            var entity = new ApplicationUser
            {
                UserName = dto.Username,
                CreatedAt = DateTime.UtcNow,
            };
            var result = await _userManager.CreateAsync(entity, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Erro ao criar usuário: {errors}");
            }

            return new UserDto
            {
                Username = entity.UserName,
                Email = entity.Email!,
                CreatedAt = entity.CreatedAt,
                Id = Guid.Parse(entity.Id)
            };
        }

        public async Task<UserDto> BuscarPorId(Guid id)
        {
            var busca = await  _userManager.FindByIdAsync(id.ToString()); 
            if (busca == null)
            {
                throw new Exception("Usuario não encontrado.");
            }
            return new UserDto
            {
                Username = busca.UserName!,
                Email = busca.Email!,
                CreatedAt = busca.CreatedAt,
                Id = Guid.Parse(busca.Id)
            };
        
        }

        public async Task<UserDto> DeleteUser(UserDto dto)
        {
            var entity = new ApplicationUser
            {
                UserName = dto.Username,
                CreatedAt = DateTime.UtcNow,
            };

            var busca = await _userManager.FindByIdAsync(entity.Id);
            if (busca == null) {
                throw new Exception("Usuario não encontrado.");
            }

             await _userManager.DeleteAsync(busca);
            return new UserDto
            {
                Username = busca.UserName!,
                Email = busca.Email!,
                Id = Guid.Parse(busca.Id),
                CreatedAt = DateTime.UtcNow,
            };

        }

        public async Task<List<UserDto>> ListUsers()
        {
            var result =  _userManager.Users.ToList();
            if (result.Count == 0)
            {
                throw new Exception("Não há usuarios cadastrados.");
            }
            var listaUsuariosDto =  result
                .Select(x => new UserDto
            {
                Username = x.UserName!,
                CreatedAt = DateTime.UtcNow,
                Email = x.Email!,
                Id = Guid.Parse(x.Id)
            })
            .ToList();
            return listaUsuariosDto;
        }


        public async Task<UserDto> UpdateUser(UserUpdateDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            var validation = _updateValidator.Validate(dto);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }
            user.UpdatedAt = DateTime.UtcNow;
            user.Collaborator = new Collaborator
            {
                Id = dto.Collaborator.Id,
                Name = dto.Collaborator.Name,
                ApplicationUser = user,
                ApplicationUserId = Guid.Parse(user.Id)
            };

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Erro ao atualizar usuário: {errors}");
            }

            return new UserDto
            {
                Id = Guid.Parse(user.Id),
                Username = user.UserName!,
                Email = user.Email!,
                CreatedAt = user.CreatedAt
            };
        }
    }
}

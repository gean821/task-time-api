using Api_IngaTasks.Application.Entities;
using Api_IngaTasks.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api_IngaTasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUserService _user;

        public ApplicationUserController(IApplicationUserService user)
        {
            _user = user;
        }

        [HttpGet("Check-Token")]
        public IActionResult CheckToken()
        {
            var userName = User.Identity!.Name;
            return Ok($"Token valido. Usuario logado: {userName}");
        }
        [HttpDelete]
        [Route("api/controller/deletandoUser")]
        public async Task<IActionResult> DeleteUser([FromBody] ApplicationUser user) 
        {

            return Ok(user);    
        }
    }
}

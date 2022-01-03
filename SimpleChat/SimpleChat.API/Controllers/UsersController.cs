using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleChat.API.Models;
using SimpleChat.BusinessLogic.Dtos;
using SimpleChat.BusinessLogic.Services.Interfaces;
using System.Threading.Tasks;

namespace SimpleChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var userDto = _mapper.Map<UserDto>(user);
                var createdUser = await _userService.CreateUserAsync(userDto);

                return Ok(createdUser);
            }

            return BadRequest();
        }
    }
}

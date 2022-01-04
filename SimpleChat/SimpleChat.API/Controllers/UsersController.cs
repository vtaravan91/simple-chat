using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var userDto = _mapper.Map<UserDto>(user);
                var createdUser = await _userService.CreateUserAsync(userDto);

                return Ok(_mapper.Map<UserModel>(createdUser));
            }

            return BadRequest();
        }
    }
}

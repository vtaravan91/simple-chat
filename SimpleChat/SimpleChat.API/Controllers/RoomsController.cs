using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SimpleChat.API.Hubs;
using SimpleChat.API.Hubs.Interfaces;
using SimpleChat.API.Models;
using SimpleChat.API.Models.Requests;
using SimpleChat.BusinessLogic.Dtos;
using SimpleChat.BusinessLogic.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        private readonly IHubContext<RoomsHub, IClientRoomsHub> _roomsHubContext;

        public RoomsController(IMapper mapper, IRoomService roomService, IHubContext<RoomsHub, IClientRoomsHub> roomsHubContext)
        {
            _mapper = mapper;
            _roomService = roomService;
            _roomsHubContext = roomsHubContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync([FromQuery] RoomsRequestModel request)
        {
            var filter = _mapper.Map<RoomsFilterDto>(request);
            var rooms = await _roomService.GetAllAsync(filter);

            if (rooms.Any())
            {
                return Ok(_mapper.Map<IReadOnlyList<RoomModel>>(rooms));
            }

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(RoomModel room)
        {
            if (ModelState.IsValid)
            {
                var roomDto = _mapper.Map<RoomDto>(room);
                var createdRoom = await _roomService.CreateRoomAsync(roomDto);

                var createdRoomModel = _mapper.Map<RoomModel>(createdRoom);
                await _roomsHubContext.Clients.All.ReceiveNewRoomAsync(createdRoomModel);

                return Ok(createdRoomModel);
            }

            return BadRequest();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleChat.API.Models.Requests;
using SimpleChat.BusinessLogic.Dtos;
using SimpleChat.BusinessLogic.Services.Interfaces;
using System.Threading.Tasks;

namespace SimpleChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;

        public MessagesController(IMapper mapper,
            IMessageService messageService)
        {
            _mapper = mapper;
            _messageService = messageService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] MessagesRequestModel request)
        {
            var filter = _mapper.Map<MessageFilterDto>(request);

            var messages = await _messageService.GetMessagesAsync(filter);

            return Ok(messages);
        }
    }
}

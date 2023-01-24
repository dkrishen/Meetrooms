using MRA.Rooms.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRA.Rooms.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository roomRepository;

        public RoomsController(IRoomRepository roomRepository) 
            => this.roomRepository = roomRepository;

        [HttpGet]
        [Route("AllRooms")]
        public async Task<IActionResult> GetAllRoomsAsync()
        {
            return Ok(await roomRepository.GetRoomsAsync());
        }

        [HttpGet]
        [Route("RoomById")]
        public async Task<IActionResult> GetRoomByIdAsync(string data)
        {
            Guid roomId = JsonConvert.DeserializeObject<Guid>(data);
            return Ok(await roomRepository.GetRoomByIdAsync(roomId));
        }

        [HttpGet]
        [Route("RoomsByIds")]
        public async Task<IActionResult> GetRoomsByIdsAsync(string data)
        {
            var roomIds = JsonConvert.DeserializeObject<IEnumerable<Guid>>(data);
            return Ok(await roomRepository.GetRoomsByIdsAsync(roomIds));
        }
    }
}

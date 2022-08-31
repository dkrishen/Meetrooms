using MRA.Rooms.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        [Route("GetAllRooms")]
        public IActionResult GetAllRooms()
        {
            return Ok(JsonConvert.SerializeObject(roomRepository.GetRooms()));
        }

        [HttpGet]
        [Route("GetRoomById")]
        public IActionResult GetRoomById(string data)
        {
            Guid roomId = JsonConvert.DeserializeObject<Guid>(data);
            return Ok(JsonConvert.SerializeObject(roomRepository.GetRoomById(roomId)));
        }

        [HttpGet]
        [Route("GetRoomsByIds")]
        public IActionResult GetRoomsByIds(string data)
        {
            var roomIds = JsonConvert.DeserializeObject<IEnumerable<Guid>>(data);
            return Ok(JsonConvert.SerializeObject(roomRepository.GetRoomsByIds(roomIds)));
        }
    }
}

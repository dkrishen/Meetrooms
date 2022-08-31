using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRA.Users.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MRA.Users.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("GetUserNamesByIds")]
        public IActionResult GetUserInfo(string data)
        {
            var userIds = JsonConvert.DeserializeObject<IEnumerable<string>>(data ?? "[]");
            return Ok(JsonConvert.SerializeObject(_userRepository.GetUsersByIds(userIds)));
        }

        [HttpGet]
        [Route("GetAllUserNames")]
        public IActionResult GetAllUserNames()
        {
            return Ok(JsonConvert.SerializeObject(_userRepository.GetAllUserNames()));
        }
    }
}

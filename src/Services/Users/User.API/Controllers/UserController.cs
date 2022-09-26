using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRA.Users.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRA.Users.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> GetUserInfoAsync(string data)
        {
            var userIds = JsonConvert.DeserializeObject<IEnumerable<string>>(data ?? "[]");
            return Ok(await _userRepository.GetUsersByIdsAsync(userIds));
        }

        [HttpGet]
        [Route("GetAllUserNames")]
        public async Task<IActionResult> GetAllUserNamesAsync()
        {
            return Ok(await _userRepository.GetAllUserNamesAsync());
        }
    }
}

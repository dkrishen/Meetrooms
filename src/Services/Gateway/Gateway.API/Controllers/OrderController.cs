using AutoMapper;
using MRA.Gateway.Models;
using MRA.Gateway.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MRA.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        IMeetingRoomRepository _meetingRoomRepository;
        IUserRepository _userRepository;
        IOrderRepository _orderRepository;
        IMapper _mapper;

        public OrderController(
            IMeetingRoomRepository meetingRoomRepository, 
            IUserRepository userRepository, 
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _meetingRoomRepository = meetingRoomRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var orders = _orderRepository.GetOrders(authorizationHeaderValue).ToList();
            
            var roomIds = orders.Select(o => o.MeetingRoomId).ToHashSet<Guid>();
            var rooms = _meetingRoomRepository.GetRoomsByRoomIds(roomIds, authorizationHeaderValue);

            var userIds = orders.Select(o => o.UserId).ToHashSet<Guid>();
            var users = _userRepository.GetUsersByIds(userIds, authorizationHeaderValue).ToList();

            var result = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                var orderViewModel = _mapper.Map<OrderViewModel>(order);
                orderViewModel.Username = users?.Where(u => u.Id == order.UserId)?.SingleOrDefault()?.Username ?? "[DELETED USER]";
                orderViewModel.MeetingRoomName = rooms?.Where(r => r.Id == order.MeetingRoomId)?.FirstOrDefault()?.Name ?? "[DELETED ROOM]";
                result.Add(orderViewModel);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("GetOrdersByUser")]
        public IActionResult GetOrdersByUser()
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var orders = _orderRepository.GetOrdersByUser(userId, authorizationHeaderValue);

            var roomIds = orders.Select(o => o.MeetingRoomId).ToHashSet<Guid>();
            var rooms = _meetingRoomRepository.GetRoomsByRoomIds(roomIds, authorizationHeaderValue);

            var user = _userRepository.GetUsersByIds(new HashSet<Guid>() { userId }, authorizationHeaderValue).FirstOrDefault();

            var result = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                var orderViewModel = _mapper.Map<OrderViewModel>(order);
                orderViewModel.Username = user?.Username ?? "[ERROR]";
                orderViewModel.MeetingRoomName = rooms?.Where(r => r.Id == order.MeetingRoomId)?.FirstOrDefault()?.Name ?? "[DELETED ROOM]";
                result.Add(orderViewModel);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder([FromBody] OrderInputDto order)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            var newOrder = _mapper.Map<Order>(order);
            newOrder.MeetingRoomId = new Guid("1DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C");  // temporary GUID the single meeting room (have not ability for select rooms at the moment)
            newOrder.UserId = userId; 
            
            _orderRepository.AddOrder(newOrder, authorizationHeaderValue);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder([FromBody] GuidDto data)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            _orderRepository.DeleteOrder(data.id, authorizationHeaderValue);
            return Ok();
        }

        [HttpPut]
        [Route("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] OrderEditDto order)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            _orderRepository.UpdateOrder(_mapper.Map<Order>(order), authorizationHeaderValue);
            return Ok();
        }
    }
}

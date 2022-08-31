using MRA.Gateway.Models;
using System;
using System.Collections.Generic;

namespace MRA.Gateway.Repository
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> GetOrders(string token);
        public IEnumerable<Order> GetOrdersByUser(Guid id, string token);
        public void AddOrder(Order order, string token);
        public void UpdateOrder(Order order, string token);
        public void DeleteOrder(Guid id, string token);
    }
}

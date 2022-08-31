using MRA.Gateway.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MRA.Gateway.Repository
{
    public class OrderRepository : RepositoryBase , IOrderRepository
    {
        public OrderRepository(IConfiguration configuration) : base(configuration.GetSection("MRA.Orders").GetValue<string>("Url"))
        {
        }

        public void AddOrder(Order order, string token)
        {
            Request("api/book/AddBook?data=", "POST", order, token);
        }

        public void DeleteOrder(Guid id, string token)
        {
            Request("api/book/DeleteBook?data=", "DELETE", id, token);
        }

        public IEnumerable<Order> GetOrders(string token)
        {
            var jsonResponse = Request("api/book/GetAllBooks", "GET", token: token);
            return JsonConvert.DeserializeObject<IEnumerable<Order>>(jsonResponse);
        }

        public IEnumerable<Order> GetOrdersByUser(Guid id, string token)
        {
            var jsonResponse = Request("api/book/GetBooksByUserId?data=", "GET", id, token);
            return JsonConvert.DeserializeObject<IEnumerable<Order>>(jsonResponse);
        }

        public void UpdateOrder(Order order, string token)
        {
            Request("api/book/UpdateBook?data=", "PUT", order, token);
        }
    }
}

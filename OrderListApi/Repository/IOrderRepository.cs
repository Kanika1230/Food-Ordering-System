using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderListApi.Models;

namespace OrderListApi.Repository
{
    public interface IOrderRepository
    {
        public List<Order> GetOrders();
        public Order GetOrder(int? id);
        public int AddOrder(Order newOrder);
        public int DeleteOrder(int? id);
    }
}

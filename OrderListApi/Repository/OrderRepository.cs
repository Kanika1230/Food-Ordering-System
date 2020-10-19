using System;
using System.Collections.Generic;
using System.Linq;
using OrderListApi.Repository;
using System.Threading.Tasks;
using OrderListApi.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderListApi.Repository
{
    public class OrderRepository:IOrderRepository
    {
        Ordering_FoodContext context;
        public OrderRepository(Ordering_FoodContext _context)
        {
            context = _context;
        }
        public List<Order> GetOrders()
        {
            if (context != null)
            {
                return context.Order.Include("Item").ToList();
            }
            return null;
        }
        public Order GetOrder(int? id)
        {
            if (context != null)
            {
                return context.Order.Include("Item")
                    .Where(x => x.OrderId == id).FirstOrDefault();
            }
            return null;
        }
        public int AddOrder(Order newOrder)
        {
            if (context != null)
            {
                context.Order.Add(newOrder);
                context.SaveChanges();
                return (newOrder.OrderId);
            }
            return 0;
        }
        public int DeleteOrder(int? id)
        {
            int result = 0;
            if (context != null)
            {
                var data = context.Order.FirstOrDefault(e => e.OrderId == id);
                if (data != null)
                {
                    context.Order.Remove(data);
                    result = context.SaveChanges();
                }
                return result;
            }
            return result;
        }

    }
}

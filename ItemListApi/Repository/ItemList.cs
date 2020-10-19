using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemListApi.Models;

namespace ItemListApi.Repository
{
    public class ItemList:IItemList
    {
        Ordering_FoodContext context;
        public ItemList(Ordering_FoodContext _context)
        {
            context = _context;
        }
        public List<Item> GetItems()
        {
            if (context != null)
            {
                return context.Item.ToList();
            }
            return null;

        }
        public Item GetItem(int? id)
        {
            if (context != null)
            {
                return context.Item.Where(e => e.Iid == id).FirstOrDefault();
            }
            return null;
        }
        public int Delete(int? id)
        {
            int result = 0;
            if (context != null)
            {
                var data = context.Item.FirstOrDefault(e => e.Iid == id);
                if (data != null)
                {
                    context.Item.Remove(data);
                    result = context.SaveChanges();
                }
                return result;
            }
            return result;
        }
        public int UpdateItem(int? id, Item newItem)
        {
            var data = context.Item.FirstOrDefault(e => e.Iid == id);
            if (data != null)
            {
                data.Iname = newItem.Iname;
                data.Iprice = newItem.Iprice;
                data.Iavailability = newItem.Iavailability;
                context.SaveChanges();
            }
            return 0;

        }
        public int AddItem(Item item)
        {
            if (context != null)
            {
                context.Item.Add(item);
                context.SaveChanges();
                return item.Iid;
            }
            return 0;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemListApi.Models;

namespace ItemListApi.Repository
{
    public interface IItemList
    {
        public List<Item> GetItems();
        public Item GetItem(int? id);
        public int Delete(int? id);
        public int UpdateItem(int? id, Item newItem);
        public int AddItem(Item newItem);
    }
}

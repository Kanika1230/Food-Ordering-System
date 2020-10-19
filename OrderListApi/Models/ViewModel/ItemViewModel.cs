using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderListApi.Models.ViewModel
{
    public class ItemViewModel
    {
        public int Iid { get; set; }
        public string Iname { get; set; }
        public string Iprice { get; set; }
        public string Iavailability { get; set; }
        public string ItemName { get; set; }
    }
}

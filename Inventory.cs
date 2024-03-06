using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem2
{
    enum Category
    {
        Tools,
        Electrical,
        Plumbing,
        Hardware,
        Lumber,
        Garden,
        Outdoor,
        Houseware,
        Miscellanous,
    }

    class Inventory
    {
        public int StockNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
               
        public float Quantity { get; set; }
                     
        public string Description { get; set; }
        public Category Cat { get; set; }
    }
     
}

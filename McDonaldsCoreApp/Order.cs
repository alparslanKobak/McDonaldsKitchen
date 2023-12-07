using System.Collections.Generic;

namespace McDonaldsCoreApp
{
    public class Order
    {
        public int Id { get; set; }

        public string OrderName { get; set; }
      
        public string OrderStatus { get; set; }

        public List<Product> Products { get; set; }
    }
}

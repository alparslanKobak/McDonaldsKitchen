using System.Collections.Generic;

namespace McDonaldsCoreApp
{
    public class SendOrder
    {
        public int Id { get; set; }

        public string OrderStatus { get; set; }

        public List<OrderProduct> Products { get; set; }
    }
}

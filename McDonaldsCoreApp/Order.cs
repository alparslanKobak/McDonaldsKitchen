using System;
using System.Collections.Generic;
using System.Linq;

namespace McDonaldsCoreApp
{
    //[Serializable]
    public class Order
    {
        public int Id { get; set; }

        public string OrderStatus { get; set; }

        public List<Product> Products { get; set; }

       



    }
}

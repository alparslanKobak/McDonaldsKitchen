using System.Collections.Generic;

namespace McDonaldsCoreApp
{
    public class Order
    {
        public int Id { get; set; }
      
        public string OrderStatus { get; set; }

        public List<Product> Products { get; set; }

        // Product product = new Product(){
        //    Id = 1,
        //    Name = "Big Mac",
        //    Quantity = 1,
        //    OrderId = 1
        // };

        // Product product2 = new Product(){
        //    Id = 2,
        //    Name = "McChicken",
        //    Quantity = 1,
        //    OrderId = 1
        // };

        // Order order = new Order(){
        //    Id = 1,
        //    OrderStatus = "Hazırlanıyor",
        //    Products = new List<Product>(){
        //        product,
        //        product2
        //    }

        // };

        //Bu order'ın Json tipinde görünümü de şu olur.

        // {
        //    "Id": 1,
        //    "OrderStatus": "Hazırlanıyor",
        //    "Products": [
        //        {
        //            "Id": 1,
        //            "Name": "Big Mac",
        //            "Quantity": 1,
        //            "OrderId": 1
        //        },
        //        {
        //            "Id": 2,
        //            "Name": "McChicken",
        //            "Quantity": 1,
        //            "OrderId": 1
        //        }
        //    ]
        // }

        // Bu order'ı byte tipine çevirip tcp üzerinden gönderirsek, karşı tarafta byte tipinden Json tipine çevirip kullanabiliriz.
        //

    }
}

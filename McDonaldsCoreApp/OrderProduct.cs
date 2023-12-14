namespace McDonaldsCoreApp
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
  
        public int ProductId { get; set; }
        Product Product { get; set; }
        public int Quantity { get; set; }


        public string Name { get; set; }
    }
}

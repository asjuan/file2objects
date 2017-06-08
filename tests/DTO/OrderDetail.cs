namespace tests.DTO
{
    public class OrderDetail
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public bool IsPriority { get; set; }
        public double? Price { get; set; }
    }
}

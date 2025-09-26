namespace WebApplicationDemo.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid StoreId { get; set; }
        public Store Store { get; set; } = null!;
    }
}

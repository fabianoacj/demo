namespace WebApplicationDemo.Models
{
    public class Store
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        // Foreign Key
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

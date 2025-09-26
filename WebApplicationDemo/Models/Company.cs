namespace WebApplicationDemo.Models
{
    public class Company
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApplicationDemo.DTOs
{
    public class StoreCreateRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class StoreUpdateRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Guid CompanyId { get; set; }
    }

    public class StoreResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
    }
}

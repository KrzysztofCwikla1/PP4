using System.ComponentModel.DataAnnotations.Schema;

namespace EShopService.Models
{
    public class Product : BaseModel
    {
        public string? Name { get; set; }
        public string? Ean { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int? Stock { get; set; } = 0;
        public string? Sku { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}

using EShopService.Models;

namespace EShopService.Models
{
    public class Category : BaseModel
    {
        public int CategoryId { get; set; }
        public string? Category { get; set; }
    }
}
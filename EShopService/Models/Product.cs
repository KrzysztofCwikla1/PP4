﻿

namespace EShopService.Models
{
    public class Product : BaseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Ean { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; } = 0;
        public string? Sku { get; set; }

        public Category? Category { get; set; }
    }
}

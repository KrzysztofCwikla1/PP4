using EShopService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static readonly List<Category> Categories = new()
    {
        new Category { CategoryId = 1, CategoryName = "Elektronika", CreatedAt = DateTime.UtcNow, CreatedBy = Guid.NewGuid() },
        new Category { CategoryId = 2, CategoryName = "Dom i ogród", CreatedAt = DateTime.UtcNow, CreatedBy = Guid.NewGuid() }
    };

    private static readonly List<Product> Products = new();

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Products.Where(p => !p.Deleted).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id && !p.Deleted);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create(Product product)
    {
        product.Id = Products.Count > 0 ? Products.Max(p => p.Id) + 1 : 1;
        product.CreatedAt = DateTime.UtcNow;
        product.CreatedBy = Guid.NewGuid();
        product.Category = Categories.FirstOrDefault(c => c.CategoryId == product.Category.CategoryId);

        Products.Add(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product updated)
    {
        var existing = Products.FirstOrDefault(p => p.Id == id && !p.Deleted);
        if (existing == null) return NotFound();

        existing.Name = updated.Name;
        existing.Ean = updated.Ean;
        existing.Price = updated.Price;
        existing.Stock = updated.Stock;
        existing.Sku = updated.Sku;
        existing.Category = Categories.FirstOrDefault(c => c.CategoryId == updated.Category.CategoryId);
        existing.UpdatedAt = DateTime.UtcNow;
        existing.UpdatedBy = Guid.NewGuid();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id && !p.Deleted);
        if (product == null) return NotFound();

        product.Deleted = true;
        product.UpdatedAt = DateTime.UtcNow;
        product.UpdatedBy = Guid.NewGuid();

        return NoContent();
    }
}

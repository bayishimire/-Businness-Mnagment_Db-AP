using Businness_Mnagment_Db_API.Data;
using Businness_Mnagment_Db_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Businness_Mnagment_Db_API.Controllers;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/Product")]
public class Product_controller : ControllerBase
{
    //define field

    public readonly AppDbContext _products;

    //constructor
    public Product_controller(AppDbContext products)
    {
        _products = products;
    }

    //get Products
    [HttpGet]
    public async Task<IActionResult> Get_ALLPRODUCTS()
    {
        if (_products == null)
        {
            return BadRequest("No products found");
        }

        var Get_products = await _products.Products.ToListAsync();
        return Ok(Get_products);
    }

    // get products Id
    [HttpGet("{id}")]
    public async Task<IActionResult> Get_ALLPRODUCTS(int id)
    {
        var searching_data = await _products.Products.FirstOrDefaultAsync(Products_id => Products_id.ProductId == id);
        {
            if (searching_data == null)
            {
                return BadRequest("searching data not found");
            }

            return Ok(searching_data);
        }
    }

    //POST DATA To databasess
    [HttpPost]
    public async Task<IActionResult> POST_Products(Products _News_products)
    {
        if (_products == null)
        {
            return BadRequest("the POST_Products was null");
        }

        var add_products = await _products.AddAsync(_News_products);
        var save = await _products.SaveChangesAsync();
        return Ok(_News_products);



    }
    //method of delete record in data

    [HttpDelete("{id}")]
    public async Task<IActionResult> DELETE_Products(int id)
    {
        // 1. Search for the product
        var searching_removedata = await _products.Products.FirstOrDefaultAsync(ip => ip.ProductId == id);

        // 2. Check if product exists
        if (searching_removedata == null)
        {
            return NotFound($"Product {id} was not found");
        }

        // 3. Check whether product is used in existing orders
        var product_in_orders = await _products.OrderItems
            .AnyAsync(o => o.ProductId == id);

        if (product_in_orders)
        {
            return BadRequest(
                $"Product {id} cannot be deleted because it is used in existing orders");
        }

        // 4. Remove product
        _products.Products.Remove(searching_removedata);

        // 5. Save changes to MySQL
        await _products.SaveChangesAsync();

        // 6. Return success message
        return Ok($"Product {id} was deleted successfully");
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PUT_Products(int id, Products update_products)
    {
        // 1. Validate request data
        if (update_products == null)
        {
            return BadRequest("The update data was null");
        }

        // 2. Check ID mismatch
        if (update_products.ProductId != id)
        {
            return BadRequest("The update ID mismatch");
        }

        // 3. Search for the existing product
        var exist_product = await _products.Products.FirstOrDefaultAsync(p => p.ProductId == id);

        // 4. Check if product exists
        if (exist_product == null)
        {
            return NotFound("Product not found");
        }

        // 5. Update product properties
        exist_product.CategoryId = update_products.CategoryId;
        exist_product.Description = update_products.Description;
        exist_product.Price = update_products.Price;
        exist_product.ProductName = update_products.ProductName;
        exist_product.StockQuantity = update_products.StockQuantity;
        
        

        
        // 6. Save changes to the database
        await _products.SaveChangesAsync();

        // 7. Return updated product
        return Ok(exist_product);
    }
}


    



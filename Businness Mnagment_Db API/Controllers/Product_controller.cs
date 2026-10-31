using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Businness_Mnagment_Db_API.Models;
using Businness_Mnagment_Db_API.Data;


namespace Businness_Mnagment_Db_API.Controllers;
[ApiController]
[Route("api/products[controller]")]
public class Product_controller:ControllerBase


{    // difine  databasec context
    private readonly AppDbContext _dbContext;
    
    
    //difine constructor 
    //AppDbContext through Dependency Injection
    

    public Product_controller(AppDbContext Context)
    {
        _dbContext = Context;
    }

    [HttpGet]
    public async Task<IActionResult> Get_ALLPRODUCTS()
    {
      var retrive_products = await _dbContext.Products.ToListAsync();
      return Ok(retrive_products);
        
    }
}
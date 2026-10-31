using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Businness_Mnagment_Db_API.Data;
using Businness_Mnagment_Db_API.Models;

namespace Businness_Mnagment_Db_API.Controllers;

[ApiController]
[Route("api/customers")]


public class Customer_controller:ControllerBase
{
    //database field
    public readonly AppDbContext _context_customer;
    //constructor

    public Customer_controller(AppDbContext Context)
    {
        _context_customer = Context;
        
    }

    [HttpGet]
    public async Task<IActionResult> Get_ALLPRODUCTS()
    {
        
        var get_customer = await _context_customer.Customers.ToListAsync();
        {
            if (get_customer.Count == 0)
            {
                return NotFound("No records found");
            }

            if (get_customer == null)
            {
                return BadRequest("No records found NULL");
            }
            return Ok(get_customer);
        }

        
    }
}
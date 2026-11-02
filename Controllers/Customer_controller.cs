using Businness_Mnagment_Db_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Businness_Mnagment_Db_API.Models;

namespace Businness_Mnagment_Db_API.Controllers;

[ApiController]
[Route("api/customer")]
public class Customer_controller:ControllerBase
{
    //difine data field
    public readonly AppDbContext coustomer_data;
    
    //difine constructor

    public Customer_controller(AppDbContext context)
    {
        coustomer_data=context;
    }

    [HttpGet]
    public async Task<IActionResult> Get_alluser()
    {
        var user_customer = await coustomer_data.Customers.ToListAsync();
        {
            if (user_customer != null)
            {
                return Ok(user_customer);
            }
            else
            {
                return NotFound("the user is not found");
            }
        }
    }
    
    
    //get userid
    [HttpGet("{id}")]
    public async Task<IActionResult> Get_coustomer_id(int id)
    {
        var user_customer = await coustomer_data.Customers.FirstOrDefaultAsync(coustomer_id => coustomer_id.CustomerId == id);
        {
            if (user_customer != null)
            {
                return Ok(user_customer);
            }
            else
            {
                return NotFound("the customer is not found");
            }
        }
    }
    
    
    //postuser
    [HttpPost]
    public async Task<IActionResult> Post_coustomer([FromBody] Customers new_Customers)
    {

        if (new_Customers == null)
        {
            return BadRequest("The data was null");
        }
        
        
        await coustomer_data.Customers.AddAsync(new_Customers);
        await coustomer_data.SaveChangesAsync();
        return Ok(new_Customers);
    }
    
    
    //deleteuser

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete_coustomer(int id)
    {
        if (id <= 0)
        {
            return BadRequest("The data was null");
        }
        
        var searching_data_to=await coustomer_data.Customers.FirstOrDefaultAsync(coustomer_id => coustomer_id.CustomerId == id);
        {
            if (searching_data_to == null)
            {
                return NotFound("the customer is not found");
            }
            coustomer_data.Customers.Remove(searching_data_to);
            await coustomer_data.SaveChangesAsync();
            return Ok($"the customer {searching_data_to.CustomerId} deleted");
        }
    }
    
    
    //update user

    [HttpPut("{id}")]
    public async Task<IActionResult> Put_coustomer_id(int id, [FromBody] Customers new_Customers)
    {
        if (id <= 0)
        {
            return BadRequest("The data was less than or equal to 0");
        }
        
        var exist_searching=await coustomer_data.Customers.FirstOrDefaultAsync(customer_id=>customer_id.CustomerId == id);
        {
            if (exist_searching == null)
            {
                return NotFound("the customer is not found");
            }
            
            
            exist_searching.CustomerId = new_Customers.CustomerId;
            exist_searching.Phone=new_Customers.Phone;
            exist_searching.Email=new_Customers.Email;
            exist_searching.Address=new_Customers.Address;
            exist_searching.CreatedAt=new_Customers.CreatedAt;
            exist_searching.FullName=new_Customers.FullName;
            
            await coustomer_data.SaveChangesAsync();
            return Ok(new_Customers);
            
            
            
        }
    }
    

}
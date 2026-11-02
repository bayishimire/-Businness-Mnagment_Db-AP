using Businness_Mnagment_Db_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Businness_Mnagment_Db_API.Models;


namespace Businness_Mnagment_Db_API.Controllers;










[ApiController]
[Route("api/Orders")]
public class Order_Controller:ControllerBase
{
    public readonly AppDbContext _Ordes_data;


    public Order_Controller(AppDbContext Context)
    {
        _Ordes_data=Context;
        
    }

    [HttpGet]
    public async Task<IActionResult> Get_ALLPRODUCTS()
    {
        var Orders_Data = await _Ordes_data.Orders.ToListAsync();
        if (Orders_Data== null)
        {
            return BadRequest("No Orders Found");
        }
        
        return Ok(Orders_Data);
    }

    [HttpGet("id")]
    public async Task<IActionResult> Get_ALLORDERS(int id)
    {
        var order_serching=await _Ordes_data.Orders.FirstOrDefaultAsync(items_id=>items_id.OrderId==id);
        {

            if (order_serching == null)
            {
                return BadRequest("No Orders Found");
            }
            return Ok(order_serching);
        }
    }

    [HttpPost]
    public async Task<IActionResult> post_oder(Orders _new_oder)
    {
        if (_new_oder==null)
        {
          return  BadRequest("_new_oder is null");
        }
        var add_oder=await _Ordes_data.AddAsync(_new_oder);
        var store_data = await _Ordes_data.SaveChangesAsync();
        return Ok(store_data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> delete_oder(int id)
    {
        if (id <= 0)
        {
            return BadRequest("No Orders Found");
        }

        if (id == null)
        {
            return BadRequest("id is null");
        }
        
        var dsearching_oders=await _Ordes_data.Orders.FirstOrDefaultAsync(items_id=>items_id.OrderId==id);
        {
            if (dsearching_oders == null)
            {
                return BadRequest("No Orders Found");
            }
            
            _Ordes_data.Remove(dsearching_oders);
            await _Ordes_data.SaveChangesAsync();
            
            return Ok($"successfully deleted {id}");
        }
    }
    

}
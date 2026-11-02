using Businness_Mnagment_Db_API.Data;
using Businness_Mnagment_Db_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Businness_Mnagment_Db_API.Controllers;


[ApiController]
[Route("api/Payment")]
public class Payments_controllers : ControllerBase
{

    //databases field


    public readonly AppDbContext _new_Payments;

    //constructor 

    public Payments_controllers(AppDbContext context)
    {
        _new_Payments = context;
    }


    //define get method

    [HttpGet]
    public async Task<IActionResult> Get_Payments()
    {
        if (_new_Payments == null)
        {
            return NotFound("No Payments Found");
        }

        var dataget = await _new_Payments.Payments.ToListAsync();
        {
            if (dataget == null)
            {
                return NotFound("No Payments Found");
            }

            return Ok(dataget);

        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get_Payment(int id)
    {
        if (_new_Payments == null)
        {
            return NotFound("No Payments Found");
        }

        var payment_searching =
            await _new_Payments.Payments.FirstOrDefaultAsync(payment_id => payment_id.PaymentId == id);
        {
            if (payment_searching == null)
            {
                return NotFound("No Payments Found");
            }

            return Ok(payment_searching);
        }

    }


    //crete post data

    [HttpPost]
    public async Task<IActionResult> Post_Payment([FromBody] Payments _new_post_payment)
    {
        if (_new_post_payment == null)
        {
            return NotFound("No Payments Found");
        }

        await _new_Payments.Payments.AddAsync(_new_post_payment);
        await _new_Payments.SaveChangesAsync();
        return Ok(_new_post_payment);

    }
    
    //delete data
    [HttpDelete]

    public async Task<IActionResult> Delete_Payment(int id)
    {
        var seraching_data = await _new_Payments.Payments.FirstOrDefaultAsync(payment_id => payment_id.PaymentId == id);
        {
            if (seraching_data==null)
            {
                return NotFound("No Payments Found check if searching id not equal to zero");
                
            }
        
            _new_Payments.Payments.Remove(seraching_data); 
            await 
                
                _new_Payments.SaveChangesAsync();
            return Ok($"the sucessfully deleted {id}");
        }
    }
    
    
    //put data

    [HttpPut("{id}")]
    public async Task<IActionResult> Put_Payment(int id, [FromBody] Payments _new_updat_payment)
    {
        if (_new_updat_payment == null)
        {
            return NotFound("No Payments Found");
            
        }

        if (id != _new_updat_payment.PaymentId)
        {
            return BadRequest("the id does not match");
        }
        
        
        
        var exist_data=await _new_Payments.Payments.FirstOrDefaultAsync(payment_id=>payment_id.PaymentId == id);
        {
            if (exist_data == null)
            {
                return NotFound("No Payments Found");
            }
        }
        exist_data.OrderId=_new_updat_payment.OrderId;
        exist_data.Amount=_new_updat_payment.Amount;
        exist_data.PaymentDate = _new_updat_payment.PaymentDate;
        exist_data.PaymentStatus=_new_updat_payment.PaymentStatus;
        exist_data.PaymentMethod=_new_updat_payment.PaymentMethod;
        
        
        await _new_Payments.SaveChangesAsync();
        return Ok(_new_updat_payment);

    }
    
    
}
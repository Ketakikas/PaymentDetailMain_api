using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly PaymentDetailContext _context;
        public PaymentDetailsController(PaymentDetailContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentDetails>> GetPaymentDetails()
        {
            return await _context.paymentDetails.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetails>> GetPaymentDetailById(int id)
        {
            var details =await _context.paymentDetails.FindAsync(id);
            if(details==null)
            {
                return NotFound();
            }
            return details;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetails>> PostPaymentDetails(PaymentDetails details)
        {
            _context.paymentDetails.Add(details);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPaymentDetailById", new { id = details.PaymentDetailId }, details);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentDetails>> PutPaymentDetails(int id,PaymentDetails details)
        {
            if(id!=details.PaymentDetailId)
            {
                return BadRequest();
            }
            _context.Entry(details).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!PaymentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDetails>> DeletePaymentDetails(int id)
        {
            var detail = _context.paymentDetails.FirstOrDefault(x => x.PaymentDetailId == id);
            if(detail==null)
            {
                return NotFound();
            }
            _context.paymentDetails.Remove(detail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentDetailExists(int id)
        {
            return _context.paymentDetails.Any(x => x.PaymentDetailId == id);
        }
    }
}

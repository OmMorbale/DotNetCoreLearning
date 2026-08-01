using LearningDotNetCoreAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningDotNetCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public OrdersController(AppDbContext db)
        {
            _db = db;
        }

        //GET: api/orders
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _db.Orders.ToListAsync();
            return Ok(orders);
        }

        //GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order is null) return NotFound();
            return Ok(order);
        }

        //POST: api/orders
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        //PUT: api/orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Order updatedOrder)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order is null) return NotFound();

            order.CustomerName=updatedOrder.CustomerName;
            order.Amount=updatedOrder.Amount;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        //DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if(order is null) return NotFound();
            
            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

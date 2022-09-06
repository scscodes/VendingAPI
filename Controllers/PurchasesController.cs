using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using VendingAPI.Data;
using VendingAPI.Models;

namespace VendingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly VendingContext _context;

        // local decimal initialized for total cost of purchase line items
        public decimal totalCost { get; set; }


        public PurchasesController(VendingContext context)
        {
            _context = context;
        }

        // GET: api/Purchases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchase()
        {
          if (_context.Purchase == null)
          {
              return NotFound();
          }
            return await _context.Purchase.ToListAsync();
        }

        // GET: api/Purchases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetPurchase(long id)
        {
          if (_context.Purchase == null)
          {
              return NotFound();
          }
            var purchase = await _context.Purchase.FindAsync(id);

            if (purchase == null)
            {
                return NotFound();
            }

            return purchase;
        }

        // PUT: api/Purchases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchase(long id, Purchase purchase)
        {
            if (id != purchase.Id)
            {
                return BadRequest();
            }

            _context.Entry(purchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: api/Purchases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Denomination>> Dispense(Purchase purchase)
        {
            if (_context.Purchase == null || _context.Machine == null)
            {
                return Problem("Required entity set(s) in VendingContext returning null.");
            }

            var contextMachineEntity = await _context.Machine
                .Include(m => m.MachineInventory.MachineInventoryLineItem)
                .ThenInclude(p => p.Product)
                .Where(m => m.Id == purchase.Transaction.MachineId)
                .FirstOrDefaultAsync();

            // blanket check; duplicate purchase id, invalid machine reference
            if (PurchaseExists(purchase.Id) || contextMachineEntity == null)
            {
                return BadRequest();
            }

            // validate machine inventory exists
            if(contextMachineEntity.MachineInventory != null &&
                contextMachineEntity.MachineInventory.MachineInventoryLineItem != null)
            {
                // compare machine inventory and purchase line items
                foreach (var purchaseLineItem in purchase.Transaction.TransactionLineItem)
                {
                    // find; MachineInventoryLineItem:Product.Id to TransactionLineItem.ProductId
                    var contextMachineLineItem = contextMachineEntity.MachineInventory.MachineInventoryLineItem
                        .SingleOrDefault(i => i.Product.Id == purchaseLineItem.ProductId);
                    if (contextMachineLineItem != null)
                    {
                        // product found
                        if (contextMachineLineItem.CurrentQuantity - purchaseLineItem.Quantity >= 0)
                        {
                            // update inventory quantity
                            var updatedQuantity = contextMachineLineItem.CurrentQuantity - purchaseLineItem.Quantity;
                            contextMachineLineItem.CurrentQuantity = updatedQuantity;

                            // define total cost
                            totalCost += contextMachineLineItem.Product.Price * purchaseLineItem.Quantity;
                        }
                        else
                        {
                            // inventory result invalid (less than 0)
                            return BadRequest();
                        }
                    }
                } // end loop

                if (purchase.AmountTendered < totalCost)
                {
                    return BadRequest();
                }


                try
                {
                    await _context.Purchase.AddAsync(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"## exception: {ex}");
                }

                return new Denomination(purchase.AmountTendered - totalCost);
            }

            return BadRequest();

        }

        // DELETE: api/Purchases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(long id)
        {
            if (_context.Purchase == null)
            {
                return NotFound();
            }
            var purchase = await _context.Purchase.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            _context.Purchase.Remove(purchase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseExists(long id)
        {
            return (_context.Purchase?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

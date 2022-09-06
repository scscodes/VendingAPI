using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendingAPI.Data;
using VendingAPI.Models;

namespace VendingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly VendingContext _context;

        public TransactionsController(VendingContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction()
        {
          if (_context.Transaction == null)
          {
              return NotFound();
          }
            return await _context.Transaction
                .Include(t => t.TransactionLineItem)
                .ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(long id)
        {
          if (_context.Transaction == null)
          {
              return NotFound();
          }
            var transaction = await _context.Transaction
                .Include(t => t.TransactionLineItem)
                .Where(t => t.Id == id)
                .SingleAsync();

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(long id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            var contextEntity = await _context.Transaction
                .Include(t => t.TransactionLineItem)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
            if(contextEntity == null)
            {
                // !entity; Add transaction
                await _context.Transaction.AddAsync(transaction);
            }
            else
            {
                // ref A
                // entity; EntityState.Modified
                _context.Entry(contextEntity).CurrentValues.SetValues(transaction);
                // in existing line items, remove line items not found in param
                foreach(var lineItem in contextEntity.TransactionLineItem.ToList())
                {
                    if(!transaction.TransactionLineItem.Any(t => t.Id == lineItem.Id))
                    {
                        contextEntity.TransactionLineItem.ToList().Remove(lineItem);
                    }
                }

                // in existing line items, add or update line items found in param
                foreach(var newLineItem in transaction.TransactionLineItem.ToList())
                {
                    var contextLineItem = contextEntity.TransactionLineItem.SingleOrDefault(t => t.Id == newLineItem.Id);
                    if(contextLineItem != null)
                    {
                        // update existing
                        _context.Entry(contextLineItem).CurrentValues.SetValues(newLineItem);
                    }
                    else
                    {
                        // add new
                        contextEntity.TransactionLineItem.ToList().Add(newLineItem);
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            if (_context.Transaction == null || TransactionExists(transaction.Id))
            {
                return BadRequest();
            }

            _context.Transaction.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(long id)
        {
            if (_context.Transaction == null)
            {
                return NotFound();
            }
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(long id)
        {
            return (_context.Transaction?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendingAPI.Data;
using VendingAPI.Models;

// todos:
// api/post - product.id, requries unique constraint (no other product id match), should instead map to existing productid 

// api/put - ref A; updating EF related objects https://stackoverflow.com/questions/13236116/entity-framework-problems-updating-related-objects

namespace VendingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private readonly VendingContext _context;

        public MachinesController(VendingContext context)
        {
            _context = context;
        }

        // GET: api/Machines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Machine>>> GetMachine()
        {
          if (_context.Machine == null)
          {
              return NotFound();
          }
            return await _context.Machine
                .Include(m => m.MachineInventory.MachineInventoryLineItem)
                .ThenInclude(p => p.Product)
                .ToListAsync();
        }

        // GET: api/Machines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Machine>> GetMachine(long id)
        {
          if (_context.Machine == null || !MachineExists(id))
          {
              return NotFound();
          }
            var machine = await _context.Machine
                  .Include(m => m.MachineInventory.MachineInventoryLineItem)
                  .ThenInclude(p => p.Product)
                  .Where(m => m.Id == id)
                  .SingleAsync();

            if(machine == null)
            {
                return NotFound();
            }
            return machine;

        }

        // PUT: api/Machines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMachine(long id, Machine machine)
        {
            if (id != machine.Id)
            {
                return BadRequest();
            }

            var contextEntity = await _context.Machine
                .Include(m => m.MachineInventory.MachineInventoryLineItem)
                .ThenInclude(p => p.Product)
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            if(contextEntity == null)
            {
                // !entity; Add machine
                await _context.Machine.AddAsync(machine);
            }
            else
            {
                // ref A
                // entity; EntityState.Modified
                _context.Entry(contextEntity).CurrentValues.SetValues(machine);
                // compare existing to param
                if (contextEntity.MachineInventory != null)
                {
                    if (machine.MachineInventory != null)
                    {
                        if (contextEntity.MachineInventory.Id == machine.MachineInventory.Id)
                        {
                            // existing machine id and param ID match, set values
                            _context.Entry(contextEntity.MachineInventory).CurrentValues
                                .SetValues(machine.MachineInventory);
                        }
                        else
                        {
                            // existing machine id and param ID different, add new
                            _context.MachineInventory.Attach(machine.MachineInventory);
                            contextEntity.MachineInventory = machine.MachineInventory;
                        }
                    }
                    else
                    {
                        // param is null, remove relationship
                        contextEntity.MachineInventory = null;
                    }
                }
                else
                {
                    // existing is null. if param is valid, apply to existing
                    if(machine.MachineInventory != null)
                    {
                        _context.MachineInventory.Attach(machine.MachineInventory);
                        contextEntity.MachineInventory = machine.MachineInventory;
                    }
                }

                // in existing line items, remove line items not found in param
                foreach(var lineItem in contextEntity.MachineInventory.MachineInventoryLineItem.ToList())
                {
                    if(!machine.MachineInventory.MachineInventoryLineItem.Any(x => x.Id == lineItem.Id)){
                        contextEntity.MachineInventory.MachineInventoryLineItem.ToList().Remove(lineItem);
                    }
                }

                // in existing line items, add or update line items found in param
                foreach(var newLineItem in machine.MachineInventory.MachineInventoryLineItem)
                {
                    var contextLineItem = contextEntity.MachineInventory.MachineInventoryLineItem.SingleOrDefault(x => x.Id == newLineItem.Id);
                    if(contextLineItem != null)
                    {
                        // update existing
                        _context.Entry(contextLineItem).CurrentValues.SetValues(newLineItem);
                    }
                    else
                    {
                        // add new
                        contextEntity.MachineInventory.MachineInventoryLineItem.ToList().Add(newLineItem);
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MachineExists(id))
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

        // POST: api/Machines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Machine>> PostMachine(Machine machine)
        {
            if (_context.Machine == null || MachineExists(machine.Id))
            {
                return BadRequest();
            }

            _context.Machine.Add(machine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMachine", new { id = machine.Id }, machine);
        }

        // DELETE: api/Machines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(long id)
        {
            if (_context.Machine == null || !MachineExists(id))
            {
                return NotFound();
            }
            var machine = await _context.Machine.FindAsync(id);
            if (machine == null)
            {
                return NotFound();
            }

            _context.Machine.Remove(machine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MachineExists(long id)
        {
            return (_context.Machine?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

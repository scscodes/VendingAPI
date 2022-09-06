using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VendingAPI.Models;

namespace VendingAPI.Data
{
    public class VendingContext: DbContext
    {
        public VendingContext(DbContextOptions<VendingContext> options): base(options) { }

        // pre 6.0 DbSet
        // public DbSet<Product> Product { get; set; }
        public DbSet<Product> Product => Set<Product>();
        public DbSet<Transaction> Transaction => Set<Transaction>();
        public DbSet<TransactionLineItem> TransactionLineItem => Set<TransactionLineItem>();
        public DbSet<Machine> Machine => Set<Machine>();
        public DbSet<MachineInventory> MachineInventory => Set<MachineInventory>();
        public DbSet<MachineInventoryLineItem> MachineInventoryLineItem => Set<MachineInventoryLineItem>();
        public DbSet<Purchase> Purchase => Set<Purchase>();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class ShiftsContext : DbContext
    {
        public ShiftsContext(DbContextOptions<ShiftsContext> options)
            : base(options)
        {
        }

        public DbSet<Shift> Shifts { get; set; } = null!;
    }
}
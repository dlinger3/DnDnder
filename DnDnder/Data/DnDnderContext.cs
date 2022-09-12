using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DnDnder.Models;

namespace DnDnder.Data
{
    public class DnDnderContext : DbContext
    {
        public DnDnderContext (DbContextOptions<DnDnderContext> options)
            : base(options)
        {
        }

        public DbSet<DnDnder.Models.campaign> campaign { get; set; } = default!;

        public DbSet<DnDnder.Models.Character>? Character { get; set; }
    }
}

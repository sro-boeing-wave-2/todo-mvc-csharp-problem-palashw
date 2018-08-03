using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GoogleKeepDB.Model;

namespace GoogleKeepDB.Models
{
    public class KeepContext : DbContext
    {
        public KeepContext (DbContextOptions<KeepContext> options)
            : base(options)
        {
        }

        public DbSet<GoogleKeepDB.Model.Keep> Keep { get; set; }
    }
}

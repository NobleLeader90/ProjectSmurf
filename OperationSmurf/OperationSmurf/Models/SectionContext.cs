using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationSmurf.Models
{
    public class SectionContext : DbContext
    {
        public DbSet<OperationSmurf.Models.Section> Section { get; set; }

        public SectionContext(DbContextOptions<SectionContext> options) : base(options)
        {
        }

    }
}

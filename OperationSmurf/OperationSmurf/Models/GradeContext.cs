using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationSmurf.Models
{
    public class GradeContext : DbContext
    {
        public DbSet<OperationSmurf.Models.Grade> Grade { get; set; }

        public GradeContext(DbContextOptions<GradeContext> options) : base(options)
        {
        }

    }
}

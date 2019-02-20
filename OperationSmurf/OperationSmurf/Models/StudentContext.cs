using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationSmurf.Models
{
    public class StudentContext : DbContext
    {
        public DbSet<OperationSmurf.Models.Student> Student { get; set; }

        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {
        }

    }
}

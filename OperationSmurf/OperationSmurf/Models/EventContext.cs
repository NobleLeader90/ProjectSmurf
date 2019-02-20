using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationSmurf.Models
{
    public class EventContext : DbContext
    {
        public DbSet<OperationSmurf.Models.Event> Event  { get; set; }

        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataLib
{
    public class MobileDataContext : DbContext
    {
        public MobileDataContext() : base(nameOrConnectionString: "Default")
        {

        }
        public DbSet<DivisionDevice> DivisionDevices { get; set; }
    }
}

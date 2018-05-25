namespace MobileDataLib.StaffDirectoryEDM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StaffDirEDM : DbContext
    {
        public StaffDirEDM()
            : base("name=StaffDirEDM")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

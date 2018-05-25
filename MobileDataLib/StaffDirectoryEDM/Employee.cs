namespace MobileDataLib.StaffDirectoryEDM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        public int Id { get; set; }

        [Required]
        public string AccountName { get; set; }

        public string AltWorkPhone { get; set; }

        public string Division { get; set; }

        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string JobPositionCode { get; set; }

        public string JobTitle { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Location { get; set; }

        public string Mobile { get; set; }

        public string PayLocation { get; set; }

        public byte[] Picture { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? TerminationDate { get; set; }

        public string Unit { get; set; }

        public string WorkPhone { get; set; }

        public string Workgroup { get; set; }

        public string usrAssistanceOthers { get; set; }

        public string usrAvailabilityNotes { get; set; }

        public string usrComments { get; set; }

        public string usrOther { get; set; }

        [Required]
        public string PreferredName { get; set; }

        public string SupervisorAccountName { get; set; }
    }
}

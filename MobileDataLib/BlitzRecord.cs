using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataLib
{
    public class BlitzRecord
    {

        public int Id { get; set; }

        public string MobileNumber { get; set; }
        public string RawUserName { get; set; }

        public string SanitisedUserName { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string IMSI { get; set; }

        public string SIM_Number { get; set; }

        public string SIM_SerialNumber { get; set; }

        public string IMEI { get; set; }

        public string Make { get; set; }

        public string DeviceName { get; set; }

        public string OS { get; set; }
    }
    
}

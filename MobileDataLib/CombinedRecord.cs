using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataLib
{
    public class CombinedRecord
    {
        public int Id { get; set; }

        public string MobileNumber { get; set; }

        public bool IsDataSourceBlitzData { get; set; }

        public bool IsDataSourceDivisionDevice { get; set; }

        public bool IsDataSourceFrancis { get; set; }

        public string UserName_BlitzData { get; set; }

        public string UserName_DivisionData { get; set; }

        public string UserName_FrancisData { get; set; }

        public bool IsNameConsistent { get; set; }
    }
}

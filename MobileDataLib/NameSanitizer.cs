using MobileDataLib.StaffDirectoryEDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataLib
{
    public class NameSanitizer
    {
        public static string SanitizedName(string rawName)
        {
            var result = string.Empty;


            using (var db = new StaffDirEDM())
            {
                var emp = db.Employees.ToList()
                    .Where(e => rawName.ToLower().Contains(e.FirstName.ToLower()))
                    .Where(e => rawName.ToLower().Contains(e.LastName.ToLower()))
                     .FirstOrDefault();

                if (emp != null)
                {
                    result = emp.FirstName + " " + emp.LastName;
                }
            }

            return result;
        }
    }
}

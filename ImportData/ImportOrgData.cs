using MobileDataLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportData
{
    class ImportOrgData
    {

        
        

        public void ImportDivision(string DivisionName)
        {
            
        }
        public void ImportCityInfrasctureDivision()
        {
            //string fileName = "City Infrasture Divison.txt";

            string filePathName = Path.Combine(Environment.CurrentDirectory, "App_Data", "City Infrastructure Division.txt");
            string line = string.Empty;

            
            
            if (File.Exists(filePathName)) {

                using (StreamReader sr = new StreamReader(filePathName))
                using (MobileDataLib.MobileDataContext context = new MobileDataLib.MobileDataContext())
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(line);
                        var lineArray = line.Split('\t');

                        if (lineArray[5] == "100.00" && lineArray[6] == "100.00" && lineArray[7] == "Yes")
                        {
                            string mobileNumber = lineArray[2];
                            string rawName = lineArray[3];

                            Console.WriteLine($"{rawName} with phone {mobileNumber}");

                           

                            if (!context.DivisionDevices.Any(e => e.MobileNumber == mobileNumber))
                            {
                                DivisionDevice divisionDevice = new DivisionDevice();
                                divisionDevice.Division = "City Infrastructure";
                                divisionDevice.MobileNumber = mobileNumber;
                                divisionDevice.RawName = rawName;

                                context.DivisionDevices.Add(divisionDevice);
                                context.SaveChanges();
                            }


                        }

                    }
                }
            }
            else
            {
                Console.WriteLine($" {filePathName} not found");

            }

        }
    }
}

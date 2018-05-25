using MobileDataLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportData
{
    class ImportData
    {

        public List<Tuple<String, String>> OrgDivisionTupleList { get; set; }
            = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("City Infrastructure", "City Infrastructure Division.txt"),
                new Tuple<string, string>("City Planning", "City Planning Division.txt"),

                new Tuple<string, string>("Community Development", "Community Development Division.txt"),
                new Tuple<string, string>("Corporate Services", "Corporate Services Division.txt"),
                new Tuple<string, string>("Financial Services", "Financial Services Division.txt"),

                //new Tuple<string, string>("Human Resources", "Human Resources.txt"),
                new Tuple<string, string>("Corporate Services", "Human Resources.txt"),
                new Tuple<string, string>("Parks and City Amenity", "Parks and City Amenity Division.txt"),
                new Tuple<string, string>("Southern Tasmania Councils Authority", "Southern Tasmania Councils Authority.txt"),
                new Tuple<string, string>("Wellington Park Management Trust", "Wellington Park Management Trust.txt")

            };


        public void ImportEverything()
        {
            ImportAllOrgData();
            ImportBlitzData();
            ImportFrancisData();

            UpdateCombinedRecords();
        }

        public void UpdateCombinedRecords()
        {

            Console.WriteLine("Update combined records.");


            using (MobileDataLib.MobileDataContext context = new MobileDataLib.MobileDataContext())
            {

                //DivisionDevice
                foreach (var divisionDevice in context.DivisionDevices.ToList())
                {
                    var combinedRecordExisting = context.CombinedRecords.Where(e => e.MobileNumber == divisionDevice.MobileNumber).FirstOrDefault();

                    if (combinedRecordExisting == null)
                    {
                        CombinedRecord combinedRecord = new CombinedRecord()
                        {
                            MobileNumber = divisionDevice.MobileNumber,
                            IsDataSourceDivisionDevice = true,
                            UserName_DivisionData = NameSanitizer.SanitizedName(divisionDevice.RawUserName)

                        };
                        context.CombinedRecords.Add(combinedRecord);
                        context.SaveChanges();
                    }
                    else
                    {
                        combinedRecordExisting.IsDataSourceDivisionDevice = true;
                        combinedRecordExisting.UserName_DivisionData = NameSanitizer.SanitizedName(divisionDevice.RawUserName);
                        context.SaveChanges();
                    }

                }

                //BlitzData
                foreach (var blitzData in context.BlitzRecords.ToList())
                {
                    var combinedRecordExisting = context.CombinedRecords.Where(e => e.MobileNumber == blitzData.MobileNumber).FirstOrDefault();

                    if (combinedRecordExisting == null)
                    {
                        CombinedRecord combinedRecord = new CombinedRecord()
                        {
                            MobileNumber = blitzData.MobileNumber,
                            IsDataSourceBlitzData = true,
                            UserName_BlitzData = NameSanitizer.SanitizedName(blitzData.RawUserName)

                        };
                        context.CombinedRecords.Add(combinedRecord);
                        context.SaveChanges();
                    }
                    else
                    {
                        combinedRecordExisting.IsDataSourceBlitzData = true;
                        combinedRecordExisting.UserName_BlitzData = NameSanitizer.SanitizedName(blitzData.RawUserName);
                        context.SaveChanges();
                    }

                }

                //FrancisData
                foreach (var francisData in context.FrancisData.ToList())
                {
                    var combinedRecordExisting = context.CombinedRecords.Where(e => e.MobileNumber == francisData.MobileNumber).FirstOrDefault();

                    if (combinedRecordExisting == null)
                    {
                        CombinedRecord combinedRecord = new CombinedRecord()
                        {
                            MobileNumber = francisData.MobileNumber,
                            IsDataSourceFrancis = true,
                            UserName_FrancisData = NameSanitizer.SanitizedName(francisData.RawUserName)
                        };
                        context.CombinedRecords.Add(combinedRecord);
                        context.SaveChanges();
                    }
                    else
                    {
                        combinedRecordExisting.IsDataSourceFrancis = true;
                        combinedRecordExisting.UserName_FrancisData = NameSanitizer.SanitizedName(francisData.RawUserName);
                        context.SaveChanges();
                    }

                }

                //Now see if usernames are consistent
                bool IsConsistent = true; //Initial value

                foreach (var combinedRecord in context.CombinedRecords.ToList())
                {
                    if (combinedRecord.IsDataSourceDivisionDevice && !String.IsNullOrWhiteSpace(combinedRecord.UserName_DivisionData))
                    {
                        if (combinedRecord.IsDataSourceBlitzData && !String.IsNullOrWhiteSpace(combinedRecord.UserName_BlitzData))
                        {
                            if (combinedRecord.IsDataSourceFrancis && !String.IsNullOrWhiteSpace(combinedRecord.UserName_FrancisData))
                            {
                                IsConsistent = combinedRecord.UserName_DivisionData == combinedRecord.UserName_FrancisData &&
                                                combinedRecord.UserName_DivisionData == combinedRecord.UserName_BlitzData;
                            }
                            else
                            {
                                IsConsistent = combinedRecord.UserName_DivisionData == combinedRecord.UserName_BlitzData;
                            }

                        }
                        else
                        {
                            if (combinedRecord.IsDataSourceFrancis && !String.IsNullOrWhiteSpace(combinedRecord.UserName_FrancisData))
                            {
                                IsConsistent = combinedRecord.UserName_FrancisData == combinedRecord.UserName_DivisionData;
                            }
                            //else
                            //{
                            //    IsConsistent = true;
                            //}

                        }
                    }
                    else
                    {
                        if (combinedRecord.IsDataSourceBlitzData && !String.IsNullOrWhiteSpace(combinedRecord.UserName_BlitzData))
                        {
                            if (combinedRecord.IsDataSourceFrancis && !String.IsNullOrWhiteSpace(combinedRecord.UserName_FrancisData))
                            {
                                IsConsistent = combinedRecord.UserName_BlitzData == combinedRecord.UserName_FrancisData;
                            }
                            //else
                            //{
                            //    IsConsistent = true;
                            //}

                        }
                        //else
                        //{
                        //    if (combinedRecord.IsDataSourceFrancis)
                        //    {
                        //        IsConsistent = true;
                        //    }
                        //    else
                        //    {
                        //        IsConsistent = true;
                        //    }

                        //}
                    }

                    combinedRecord.IsNameConsistent = IsConsistent;
                    context.SaveChanges();

                }


                //Now do a second loop to clean up data even further
                foreach (var combinedRecord in context.CombinedRecords.ToList())
                {
                    int nameCount = 0;

                    if (String.IsNullOrWhiteSpace(combinedRecord.UserName_BlitzData))
                    {
                        combinedRecord.UserName_BlitzData = String.Empty;
                    }
                    else
                    {
                        nameCount++;
                    }

                    if (String.IsNullOrWhiteSpace(combinedRecord.UserName_DivisionData))
                    {
                        combinedRecord.UserName_DivisionData = String.Empty;
                    }
                    else
                    {
                        nameCount++;
                    }

                    if (String.IsNullOrWhiteSpace(combinedRecord.UserName_FrancisData))
                    {
                        combinedRecord.UserName_FrancisData = String.Empty;
                    }
                    else
                    {
                        nameCount++;
                    }

                    if (String.IsNullOrWhiteSpace(combinedRecord.UserName_BlitzData)
                        && String.IsNullOrWhiteSpace(combinedRecord.UserName_DivisionData)
                        && String.IsNullOrWhiteSpace(combinedRecord.UserName_FrancisData))
                    {
                        combinedRecord.IsNameConsistent = true;
                    }

                    if (nameCount == 1)
                    {
                        combinedRecord.IsNameConsistent = true;
                    }
                    context.SaveChanges();
                }
            }
        }

        public void ImportAllOrgData()
        {
            Console.WriteLine("Import all organisation data");
            foreach (Tuple<string, string> orgDivisionTuple in this.OrgDivisionTupleList)
            {
                ImportDivision(orgDivisionTuple);
            }
        }

        public void ImportBlitzData()
        {
            Console.WriteLine("Import Blitz data");
            string blitzDataFileName = "HCC blitz report - main account.csv";
            blitzDataFileName = Path.Combine(System.Environment.CurrentDirectory, "App_Data", blitzDataFileName);

            if (File.Exists(blitzDataFileName))
            {
                string line = string.Empty;

                using (StreamReader sr = new StreamReader(blitzDataFileName))
                using (MobileDataLib.MobileDataContext context = new MobileDataLib.MobileDataContext())
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        var lineArray = line.Split(',');
                        string mobileNumber = lineArray[8];
                        if (mobileNumber.Substring(0, 2) == "04")
                        {
                            if (!context.BlitzRecords.Any(e => e.MobileNumber == mobileNumber))
                            {
                                BlitzRecord blitzRecord = new BlitzRecord()
                                {
                                    MobileNumber = mobileNumber,
                                    DeviceName = lineArray[24],
                                    IMEI = lineArray[21],
                                    IMSI = lineArray[17],
                                    Make = lineArray[22],
                                    RawUserName = lineArray[9],
                                    SIM_Number = lineArray[18],
                                    SIM_SerialNumber = lineArray[19]


                                };
                                context.BlitzRecords.Add(blitzRecord);
                                context.SaveChanges();
                            }
                        }




                    }
                }

            }
            else
            {
                Console.WriteLine($" {blitzDataFileName} not found");
                Console.ReadLine();
            }

        }

        //public void ImportCityInfrasctureDivision()
        public void ImportDivision(Tuple<string, string> orgDivisionTuple)
        {

            string divisionName = orgDivisionTuple.Item1;
            string divisionFilePathName = Path.Combine(System.Environment.CurrentDirectory, "App_Data", orgDivisionTuple.Item2);
            Console.WriteLine($"Import division data for {divisionName}");

            string line = string.Empty;

            if (File.Exists(divisionFilePathName))
            {

                using (StreamReader sr = new StreamReader(divisionFilePathName))
                using (MobileDataLib.MobileDataContext context = new MobileDataLib.MobileDataContext())
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(line);
                        var lineArray = line.Split('\t');

                        if (lineArray[5] == "100.00" && lineArray[6] == "100.00" && lineArray[7] == "Yes")
                        {
                            string mobileNumber = lineArray[2].Trim().Replace(" ", "");
                            string rawName = lineArray[3];

                            //Check we have mobile number
                            if (mobileNumber.Substring(0, 2) == "04")
                            {
                                //if mobile number is not found
                                if (!context.DivisionDevices.Any(e => e.MobileNumber == mobileNumber))
                                {
                                    DivisionDevice divisionDevice = new DivisionDevice();
                                    divisionDevice.Division = divisionName;
                                    divisionDevice.MobileNumber = mobileNumber;
                                    divisionDevice.RawUserName = rawName;

                                    context.DivisionDevices.Add(divisionDevice);
                                    context.SaveChanges();

                                    //Console.WriteLine($"Import {rawName} for division {divisionName} with phone {mobileNumber}");
                                }
                            }


                        }

                    }
                }
            }
            else
            {
                Console.WriteLine($" {divisionFilePathName} not found");
                Console.ReadLine();

            }

        }

        public void ImportFrancisData()
        {
            Console.WriteLine("Import Francis data");
            string francisFileName = "From Francis - Numbers and Names April 2018.csv";
            francisFileName = Path.Combine(System.Environment.CurrentDirectory, "App_Data", francisFileName);

            if (File.Exists(francisFileName))
            {
                string line = string.Empty;

                using (StreamReader sr = new StreamReader(francisFileName))
                using (MobileDataLib.MobileDataContext context = new MobileDataLib.MobileDataContext())
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        var lineArray = line.Split(',');
                        string mobileNumber = lineArray[0].Trim().Replace(" ", "");
                        if (mobileNumber.Substring(0, 2) == "04")
                        {

                            if (!context.FrancisData.Any(e => e.MobileNumber == mobileNumber))
                            {
                                FrancisData francisData = new FrancisData()
                                {
                                    MobileNumber = mobileNumber,
                                    RawUserName = lineArray[1]

                                };

                                if (lineArray[1].Contains("Mobile"))
                                {
                                    francisData.DeviceType = "Mobile";
                                }
                                else if (lineArray[1].Contains("iPad"))
                                {
                                    francisData.DeviceType = "iPad";
                                }
                                else if (lineArray[1].Contains("Data Plan"))
                                {
                                    francisData.DeviceType = "Data Plan";
                                }
                                else
                                {
                                    francisData.DeviceType = "Unknown";
                                }



                                context.FrancisData.Add(francisData);
                                context.SaveChanges();
                            }
                        }


                    }
                }

            }
            else
            {
                Console.WriteLine($" {francisFileName} not found");
                Console.ReadLine();
            }


        }
    }
}

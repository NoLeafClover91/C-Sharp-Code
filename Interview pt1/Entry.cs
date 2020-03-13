// Chris Grimes
// Date Created: 3/11/20
// Interview pt 1

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication4
{
    public class Entry
    {

        public Entry() { GUID = ""; Val1 = ""; Val2 = ""; Val3 = ""; IsDuplicateGuid = ""; GreaterThanVal3 = ""; combinedVal = 0; }
        public string GUID { get; set; }
        public string Val1 { get; set; }
        public string Val2 { get; set; }
        public string Val3 { get; set; }
        public string IsDuplicateGuid { get; set; }
        public string GreaterThanVal3 { get; set; }
        public int    combinedVal { get; set; }

        public static void fillTest(List<string> testIn, List<Entry> testOut)
        {
            foreach (string line in testIn)
            {
                string[] entries = line.Split(' ');

                Entry newEntry = new Entry();

                newEntry.GUID = entries[0];
                newEntry.Val1 = entries[1];
                newEntry.Val2 = entries[2];
                newEntry.Val3 = entries[3];

                testOut.Add(newEntry);
            }
        }

        public static void processTestEntries(ref List<Entry> toTest, ref int numOfVal3, ref int numOfEntries, ref int indexGUID, ref List<string> guidDuplicates)
        {
            foreach (var Entry in toTest)
            {
                numOfVal3 += Entry.Val3.Length;

                for (int i = 0; i < numOfEntries; ++i)
                {
                    if (Entry.GUID == toTest[i].GUID)
                    {
                        guidDuplicates.Add(toTest[i].GUID);
                        indexGUID++;
                    }
                }
                string a = String.Empty;
                string b = String.Empty;
                int val12 = 0;
                a = Entry.Val1.Trim('"');
                a = a.Trim('"');
                a = a.Trim(',');
                a = a.Trim('"');
                b = Entry.Val2.Trim('"');
                b = b.Trim('"');
                b = b.Trim(',');
                b = b.Trim('"');

                if ((Entry.Val1 != String.Empty) && (Entry.Val2 != String.Empty))
                {
                    val12 = Convert.ToInt32(a) + Convert.ToInt32(b);
                }

                Entry.combinedVal = val12;
                numOfEntries += 1;
            }
        }

        public static void findLargestVal1Val2(ref List<Entry> inputValue, ref Entry rtnValue)
        {
            foreach (var Entry in inputValue)
            {
                if (rtnValue.combinedVal < Entry.combinedVal)
                {
                    rtnValue = Entry;
                }
            }
        }

        public static void toFile(int numRecords, ref List<Entry> testList, ref List<string> toFile, int GUID_Index, int averageVal3, List<string> duplicateGUID, string writeTo)
        {

            toFile.Add($"GUID, Val1+Val2, IsDuplicateGUID (Y or N), Is Val3 length greater than the average length of Val3 (Y or N)");

            for (int a = 1; a <= (numRecords - 1); ++a)
            {
                testList[a].IsDuplicateGuid = "N";
                if (GUID_Index > 0)
                {
                    for (int z = 0; z <= (GUID_Index - 1); ++z)
                    {
                        if (testList[a].GUID == duplicateGUID[z])
                        {
                            testList[a].IsDuplicateGuid = "Y";
                        }
                    }
                }
                testList[a].GreaterThanVal3 = "N";
                if (testList[a].Val3.Length > averageVal3)
                {
                    testList[a].GreaterThanVal3 = "Y";
                }
                toFile.Add($"{testList[a].GUID} {testList[a].combinedVal}, {testList[a].IsDuplicateGuid}, {testList[a].GreaterThanVal3}");
            }

            using (StreamWriter sw = new StreamWriter(writeTo))
            {
                for (int d = 0; d < toFile.Count; ++d)
                {
                    sw.WriteLine(toFile[d]);
                }
            }
        }

        public static void toConsole(int numberOfRecords, Entry greatestVal1Val2, int indexForGUID, int Val3AverageValue, List<string> listOfDupGUID)
        {

            Console.WriteLine($"Number of Records:{numberOfRecords - 1}\nLargest Val1+Val2: {greatestVal1Val2.GUID} {greatestVal1Val2.combinedVal}\nDuplicate GUID:");

            if (indexForGUID > 0)
            {
                for (int j = 0; j <= (indexForGUID - 1); ++j)
                {
                    string outPut = listOfDupGUID[j];
                    Console.WriteLine(outPut);
                }
            }
            else
            {
                Console.WriteLine("No Duplicate GUID's");
            }

            Console.WriteLine($"Average Val3 Length: {Val3AverageValue}");
        }

        public static void readFromFile(ref List<string> fileInfo, string readFrom)
        {
            using (StreamReader sr = new StreamReader(readFrom))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    fileInfo.Add(line);
                }
            }
        }
    }
}

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
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = @"C:\Demos\test.csv";
            string outpath = @"C:\Demos\test2.csv";

            List<Entry> Test = new List<Entry>();

            List<string> lines= new List<string>();

            Entry.readFromFile(ref lines, filepath);

            Entry.fillTest(lines, Test);

            int totalEntries = 0;

            Entry largestVal1 = new Entry();

            int totVal3 = 0;
            int avgVal3 = 0;

            List<string> dupGUID = new List<string>();

            int guidIndex = 0;

            Test[0] = new Entry();

            Entry.processTestEntries(ref Test, ref totVal3, ref totalEntries, ref guidIndex, ref dupGUID);

            Entry.findLargestVal1Val2(ref Test, ref largestVal1);

            avgVal3 = (totVal3 / (totalEntries - 1));

            Entry.toConsole(totalEntries, largestVal1, guidIndex, avgVal3, dupGUID);

            List<string> output = new List<string>();

            Entry.toFile(totalEntries, ref Test, ref output, guidIndex, avgVal3, dupGUID, outpath);

            Console.ReadLine();
        }
    }
}

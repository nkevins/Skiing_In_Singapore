using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiingInSingapore
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input map file location
            bool validFile = false;
            string mapLocation = "";

            while (!validFile)
            {
                Console.Write("Enter map file name (you can include path also): ");
                mapLocation = Console.ReadLine().Trim();

                if (mapLocation != "" && System.IO.File.Exists(mapLocation))
                {
                    validFile = true;
                    Console.WriteLine("Processing....");
                } else
                {
                    Console.WriteLine("Invalid file location, please try again");
                }
            }

            string[] lines = System.IO.File.ReadAllLines(mapLocation);

            SkiiMap skiiMap = new SkiiMap(lines);
            Result result = skiiMap.Run(0, 0);
            Console.WriteLine("Maximum Length: " + result.maxDepth + ", Maximum Drop: " + result.maxDrop);
            Console.ReadLine();
        }
    }
}

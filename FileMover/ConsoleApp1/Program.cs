using ConsoleApp1;
using System.Configuration;

namespace FileMoverApp
{
    class Program
    {
        static void Main(string[] args)
        {

            string sourceFolder = ConfigurationManager.AppSettings["sourcePath"];
            string targetFolder = ConfigurationManager.AppSettings["targetPath"];
            string fileType = ConfigurationManager.AppSettings["fileType"];
            string filePattern = ConfigurationManager.AppSettings["filePattern"];

            if (Directory.Exists(sourceFolder))
            {
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                    Console.WriteLine("Target folder created Successfully.");
                }

                string[] fileToMove = Directory.GetFiles(sourceFolder, $"*{filePattern}" + $"*{fileType}");

                if (fileToMove.Length > 0)
                {
                    string response = CheckAndMove.SecondCheck(fileToMove);
                    Console.WriteLine(response);
                }
                else
                {
                    Console.WriteLine("File not found with given keywords");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Source folder does not exist");
            }
        }
    }
}


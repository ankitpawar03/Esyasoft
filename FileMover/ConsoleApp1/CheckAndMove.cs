using System.Configuration;
using System.Net.Http.Headers;

namespace ConsoleApp1
{
    internal class CheckAndMove
    {
        public static string SecondCheck(string[] fileToMove)
        {
            string targetFolder = ConfigurationManager.AppSettings["targetPath"];
            int fileAge = int.Parse(ConfigurationManager.AppSettings["fileAge"]);

            foreach (string file in fileToMove)
            {
                FileInfo fileInfo = new FileInfo(file);

                if (fileInfo.CreationTime <= DateTime.Now.AddDays(-fileAge))
                {
                    string targetPath = Path.Combine(targetFolder, fileInfo.Name);

                    if (!File.Exists(targetPath))
                    {
                        File.Move(fileInfo.FullName, targetPath);
                        Console.WriteLine($"File '{fileInfo.Name}' moved");
                    }
                    else
                    {
                        File.Delete(fileInfo.FullName);
                        Console.WriteLine($"Already exist file '{fileInfo.Name}' deleted");
                        Console.WriteLine();
                    }
                }
                else
                {
                    return "File not exist with given Age";
                }
            }
            Console.WriteLine();
            return "File moving process finished successfully !";
        }
    }
}

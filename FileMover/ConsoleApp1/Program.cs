using System;
using System.IO;

namespace FileMoverApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Source Folder Path");
            string sourceFolderPath = Console.ReadLine();

            Console.WriteLine("Enter Target Folder Path");
            string targetFolderPath = Console.ReadLine();

            if (Directory.Exists(sourceFolderPath))
            {

                if (!Directory.Exists(targetFolderPath))
                {
                    Directory.CreateDirectory(targetFolderPath);
                    Console.WriteLine("Target folder created Successfully.");
                }

                Console.WriteLine("Enter a file type to move");

                string getFileType = Console.ReadLine();

                foreach (string txtFile in Directory.GetFiles(sourceFolderPath, $"*{getFileType}"))
                {
                    string fileName = Path.GetFileName(txtFile);
                    string targetFilePath = Path.Combine(targetFolderPath, fileName);

                    if (!File.Exists(targetFilePath))
                    {
                        File.Move(txtFile, targetFilePath);
                        Console.WriteLine($"'{fileName}' Moved to target folder.");

                    }
                    else
                    {
                        Console.WriteLine($"'{fileName}' is already available in '{targetFolderPath}'");
                    }
                }
                return;
            }

            else
            {
                Console.WriteLine("Source folder not found.");
                Console.WriteLine("Press Enter to Exit");
                Console.Read();
            }
        }
    }
}


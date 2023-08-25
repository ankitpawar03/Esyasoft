<<<<<<< HEAD
﻿using ConsoleApp1;
using System.Configuration;
=======
﻿using System;
using System.IO;
>>>>>>> fecd96645c55c37c07a6fcde1121366465761f04

namespace FileMoverApp
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD

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
=======
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
>>>>>>> fecd96645c55c37c07a6fcde1121366465761f04
            }
        }
    }
}


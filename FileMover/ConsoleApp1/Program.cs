using System;
using System.IO;
using System.Configuration;
using System.ComponentModel;

namespace FileMoverApp
{
    class Program
    {
        static void Main(string[] args)
        {

            string sourceFolderPath = ConfigurationManager.AppSettings["sourcePath"];

            string targetFolderPath = ConfigurationManager.AppSettings["targetPath"];

            Boolean checkForAll = true;
            Boolean fileContains = false;

            // validating source folder exist or not in directory
            if (Directory.Exists(sourceFolderPath))
            {
                // validating target folder exist or not in directory
                // if not then creating target folder
                if (!Directory.Exists(targetFolderPath))
                {
                    Directory.CreateDirectory(targetFolderPath);
                    Console.WriteLine("Target folder created Successfully.");
                }

                Console.WriteLine("Enter a file type or name to move");

                string getFileType = Console.ReadLine();

                foreach (string fileToMove in Directory.GetFiles(sourceFolderPath))
                {
                    string fileName = Path.GetFileName(fileToMove);

                    if (fileName.Contains(getFileType))
                    {

                        fileContains = true;
                        string targetFilePath = Path.Combine(targetFolderPath, fileName);

                        if (!File.Exists(targetFilePath))
                        {
                            File.Move(fileToMove, targetFilePath);
                            Console.WriteLine($"'{fileName}' Moved to target folder.");
                            Console.WriteLine(); // for printing blank line
                        }
                        else
                        {
                            Console.WriteLine($"'{fileName}' is already available in '{targetFolderPath}'");

                            Boolean checkForDelete = false;

                            if (checkForAll)
                            {
                                Console.WriteLine("Do you want to delete this file? y / n");
                                string ans1 = Console.ReadLine();

                                if (ans1 == "y")
                                {
                                    checkForDelete = true;
                                }
                            }
                            if (checkForDelete)
                            {
                                File.Delete(fileToMove);
                                Console.WriteLine(fileName + " deleted successfully.");
                            }
                            Console.WriteLine("Do this with all files y / n");
                            string ans2 = (Console.ReadLine());

                            if (ans2 == "y")
                            {
                                checkForAll = false;
                            }
                        }
                    }

                }
                if (!fileContains)
                {
                    Console.WriteLine($"No files exist with '{getFileType}' keyword in '{sourceFolderPath}' folder");
                }
                else
                {
                    Console.WriteLine("Files moved Successfully");
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


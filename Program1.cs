using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

class Program1
{
    static async Task Main()
    {
        PrintAllOperations();

        Console.WriteLine("Please enter the operation name you want to perform: ");
        string operation = Console.ReadLine();

        switch (operation.ToUpper())
        {
            case "FILE COPY":
                await CopyFileAsync();
                break;
            case "FILE DELETE":
                await DeleteFileAsync();
                break;
            case "QUERY FOLDER FILES":
                QueryFolder();
                break;
            case "CREATE FOLDER":
                CreateFolder();
                break;
            case "DOWNLOAD FILE":
                await DownloadFileAsync();
                break;
            case "WAIT":
                Wait();
                break;
            case "CONDITIONAL COUNT ROWS FILE":
                // code block
                break;
            default:
                Console.WriteLine("Wrong input. Please enter from the list only.");
                break;
        }
    }

    static async Task CopyFileAsync()
    {
        try
        {
            Console.WriteLine("Please enter source path: ");
            string sourceFile = Console.ReadLine();

            Console.WriteLine("Please enter destination path: ");
            string destinationFile = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(sourceFile) || string.IsNullOrWhiteSpace(destinationFile))
            {
                Console.WriteLine("Source path or destination path is empty.");
                return;
            }

            if (!File.Exists(sourceFile))
            {
                Console.WriteLine("Source file does not exist.");
                return;
            }

            string destinationFilePath = Path.Combine(destinationFile, Path.GetFileName(sourceFile));
            await Task.Run(() => File.Copy(sourceFile, destinationFilePath, true));
            Console.WriteLine("File copied successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static async Task DeleteFileAsync()
    {
        try
        {
            Console.WriteLine("Please enter file path to delete: ");
            string filePath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("File path is empty.");
                return;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            await Task.Run(() => File.Delete(filePath));
            Console.WriteLine("File deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static void QueryFolder()
    {
        try
        {
            Console.WriteLine("Please enter directory path: ");
            string directoryPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                Console.WriteLine("Directory path is empty.");
                return;
            }

            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Directory does not exist.");
                return;
            }

            string[] files = Directory.GetFiles(directoryPath);
            Console.WriteLine("Files in the directory:");
            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static void CreateFolder()
    {
        try
        {
            Console.WriteLine("Please enter parent directory path: ");
            string parentDirectory = Console.ReadLine();

            Console.WriteLine("Please enter folder name: ");
            string folderName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(parentDirectory) || string.IsNullOrWhiteSpace(folderName))
            {
                Console.WriteLine("Parent directory path or folder name is empty.");
                return;
            }

            string directoryPath = Path.Combine(parentDirectory, folderName);

            if (Directory.Exists(directoryPath))
            {
                Console.WriteLine("Folder already exists with the same name.");
                return;
            }

            Directory.CreateDirectory(directoryPath);
            Console.WriteLine("Folder successfully created.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static async Task DownloadFileAsync()
    {
        try
        {
            Console.WriteLine("Please enter file path to download: ");
            string filePath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("File path is empty.");
                return;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            WebClient webClient = new WebClient();
            await webClient.DownloadFileTaskAsync(filePath, @"C:\Users\User\Downloads\" + Path.GetFileName(filePath));
            Console.WriteLine("File successfully downloaded in " + @"C:\Users\User\Downloads\.");

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static void Wait()
    {
        try
        {
            Console.WriteLine("Please enter how many minutes you want to wait:");
            string inputSeconds = Console.ReadLine();
            if (int.Parse(inputSeconds) == 0)
            {
                Console.WriteLine("Please enter Valid value for Seconds.");
                return;
            }
            int inputMinutes = Convert.ToInt32(Console.ReadLine());
            await Task.Delay(inputMinutes * 60 * 1000);
            Console.WriteLine("Your wait of " + inputMinutes + " minute(s) is over.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static void PrintAllOperations()
    {
        List<string> operations = new List<string> { "File Copy", "File Delete", "Query Folder Files", "Create Folder", "Download File", "Wait", "Conditional Count Rows File" };

        Console.WriteLine("List of available operations:");
        foreach (string operation in operations)
        {
            Console.WriteLine(operation);
        }
    }
}

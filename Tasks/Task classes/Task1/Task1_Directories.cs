using System;
using System.IO;

namespace Tasks.Task_classes.Task1
{
  static class Task1_Directories
  {
    public static void Execute()
    {
      string dirPath = @"D:\sampleDir"; //  Адрес создаваемой директории (при необходимости можно изменить)

      if (!Directory.Exists(dirPath))
      {
        DebugDirCreate(dirPath);
        DebugDirCreate(dirPath + @"\subDir1");
        DebugDirCreate(dirPath + @"\subDir1\subDir1_1");
        DebugDirCreate(dirPath + @"\subDir2");
        Directory.Move(dirPath + @"\subDir1\subDir1_1", dirPath + @"\subDir2\subDir1_1");
        Console.WriteLine($"subdir1_1 was moved to {dirPath + @"\subDir2"}");
      }
      else
      {
        Console.WriteLine($"Directory {dirPath} already exists!");
      }

      Console.WriteLine("\nsampleDir:\n");

      string[] directories = Directory.GetDirectories(dirPath), files = Directory.GetFiles(dirPath);

      Console.WriteLine("List of subdirs:");
      if (directories.Length != 0)
      {
        foreach (string dir in directories)
        {
          Console.WriteLine($"> {dir}");
        }
      }
      else
      {
        Console.WriteLine($"There are no subdirs at {dirPath}!");
      }

      Console.WriteLine();

      Console.WriteLine("List of files:");
      if (files.Length != 0)
      {
        foreach (string file in files)
        {
          Console.WriteLine($"> {file}");
        }
      }
      else
      {
        Console.WriteLine($"There are no files at {dirPath}!");
      }

      Console.WriteLine("\nPress any key to procceed and delete directories...\n");
      Console.ReadKey();

      Directory.Delete(dirPath + @"\subDir2\subDir1_1");
      Directory.Delete(dirPath + @"\subDir2");
      Directory.Delete(dirPath + @"\subDir1");
      Directory.Delete(dirPath);

      Console.WriteLine("Directories deleted successfully!\n");
    }

    public static void DebugDirCreate(string path)
    {
      Directory.CreateDirectory(path);
      Console.WriteLine($"Directory is created at {path}");
      Console.WriteLine($"Parent directory: {Directory.GetParent(path)}\n");
    }
  }
}
using System;
using System.IO;
using System.Text;

namespace Tasks.Task_classes.Task1
{
  static class Task1_Files
  {
    public static void Execute()
    {
      string rootPath = @"D:\sampleDir";
      Task1_Directories.DebugDirCreate(rootPath); //  Адрес создаваемой директории (при необходимости можно изменить)
      Task1_Directories.DebugDirCreate(rootPath + @"\subDir1");
      Task1_Directories.DebugDirCreate(rootPath + @"\subDir2");

      FileInfo fileInfo = new FileInfo(rootPath + @"\sampleFile.txt");
      if (!fileInfo.Exists)
      {
        fileInfo.Create().Dispose();
        Console.WriteLine($"File created at {fileInfo.FullName}\n");
      }

      Console.Write("Write something in file: "); //  Ввод информации в файл для вывода его размера
      using (FileStream fStream = new FileStream(fileInfo.FullName, FileMode.Open))
      {
        byte[] bytes = Encoding.Default.GetBytes(Console.ReadLine());
        fStream.Write(bytes, 0, bytes.Length);
      }

      fileInfo.Refresh();

      Console.WriteLine($"\nFile name: {fileInfo.Name}\tExtension: {fileInfo.Extension}");
      Console.WriteLine($"File location: {fileInfo.DirectoryName}");
      Console.WriteLine($"File size: {fileInfo.Length}B");
      Console.WriteLine($"Created: {fileInfo.CreationTime}");

      fileInfo.MoveTo(rootPath + @"\subDir1\sampleFile.txt");
      Console.WriteLine($"\n{fileInfo.Name} was moved to {rootPath + @"\subDir1"}");
      fileInfo.CopyTo(rootPath + @"\subDir2\sampleFile.txt");
      Console.WriteLine($"\n{fileInfo.Name} was copied to {rootPath + @"\subDir2"}");

      Console.WriteLine("\nPress any key to procceed and delete created files and directories...\n");
      Console.ReadKey();

      fileInfo.Delete();
      File.Delete(rootPath + @"\subDir2\sampleFile.txt");
      Directory.Delete(rootPath + @"\subDir2");
      Directory.Delete(rootPath + @"\subDir1");
      Directory.Delete(rootPath);

      Console.WriteLine("Created files deleted successfully!\n");
    }
  }
}
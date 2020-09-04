using System;
using System.IO;
using System.IO.Compression;

namespace Tasks.Task_classes
{
  static class Task5_ZIP
  {
    public static void Execute()
    {
      string sourceFile = @"D:\sampleDir", zipFile = @"D:\sampleDir.zip", targetFile = @"D:\sampleDirDecompressed";
      
      Directory.CreateDirectory(sourceFile);
      Directory.CreateDirectory(targetFile);

      File.Create(sourceFile + @"\sampleFile.txt").Dispose();

      ZipFile.CreateFromDirectory(sourceFile, zipFile);
      Console.WriteLine($"{sourceFile} zipped successfully!\n");

      ZipFile.ExtractToDirectory(zipFile, targetFile);
      Console.WriteLine($"{zipFile} unzipped to {targetFile}\n");

      Console.WriteLine("\nPress any key to procceed and delete created files and directories...\n");
      Console.ReadKey();

      File.Delete(sourceFile + @"\sampleFile.txt");
      File.Delete(targetFile + @"\sampleFile.txt");
      Directory.Delete(sourceFile);
      Directory.Delete(targetFile);
      File.Delete(zipFile);

      Console.WriteLine("Created files deleted successfully!\n");
    }
  }
}
using System;
using System.IO;
using System.Text;

namespace Tasks.Task_classes
{
  abstract class Task2_FileStream
  {
    public static void Execute()
    {
      string path = @"D:\sampleDir";	//  Адрес создаваемой директории (при необходимости можно изменить)

      Directory.CreateDirectory(path);

      FileStream fStream = new FileStream(path + @"\sampleFile.txt", FileMode.OpenOrCreate);

      Console.WriteLine("Input the text in file:");
      string inputText = Console.ReadLine();

      using (fStream)
      {
        byte[] bytesArray = Encoding.Default.GetBytes(inputText);
        fStream.Write(bytesArray, 0, bytesArray.Length);

        Console.WriteLine("File updated!");
      }

      using (fStream = File.OpenRead(path + @"\sampleFile.txt"))
      {
        byte[] bytesArray = new byte[fStream.Length];
        fStream.Read(bytesArray, 0, bytesArray.Length);
        string outputText = Encoding.Default.GetString(bytesArray);
        Console.WriteLine($"\nFile text: {outputText}");
      }

      Console.WriteLine("\nPress any key to procceed and delete created files and directories...\n");
      Console.ReadKey();

      fStream.Dispose();
      File.Delete(path + @"\sampleFile.txt");
      Directory.Delete(path);
    }
  }
}
using System;
using System.IO;
using Tasks.Task_classes;

namespace Tasks
{
  class Program
  {
    static void Main(string[] args)
    {
      Task_classes.Task1.Task1_Drives.Execute();
      Task_classes.Task1.Task1_Directories.Execute();
      Task_classes.Task1.Task1_Files.Execute();
      Task2_FileStream.Execute();
      Task3_JSON.Execute();
      Task4_XML.Execute();
      Task5_ZIP.Execute();

      Console.ReadKey();
    }
  }
}

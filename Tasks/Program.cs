﻿using System;
using Tasks.Task_classes;
using InterfaceLib; //  Мой DLL-файл, подключен для вызова удобного интерфейса

namespace Tasks
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.Title = "Pair tasks";

      while (true)
      {
        Console.Clear();

        Console.WriteLine("Choose the task:");
        Lib.CallInterface("Task 1. Drives", "Task 1. Directories", "Task 1. Files", "Task 2. FileStream", "Task 3. JSON", "Task 4. XML", "Task 5. ZIP");

        string input = Console.ReadLine();
        Console.Clear();
        switch (input)
        {
          case "1":
            Task_classes.Task1.Task1_Drives.Execute();
            break;
          case "2":
            Task_classes.Task1.Task1_Directories.Execute();
            break;
          case "3":
            Task_classes.Task1.Task1_Files.Execute();
            break;
          case "4":
            Task2_FileStream.Execute();
            break;
          case "5":
            //Task3_JSON.Execute();
            break;
          case "6":
            //Task4_XML.Execute();
            break;
          case "7":
            //Task5_ZIP.Execute();
            break;
          case "exit":
            Environment.Exit(0);
            break;
          default:
            Console.WriteLine("Wrong input received, try again!\n");
            break;
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("Press any key to continue...");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.ReadKey();
      }
    }
  }
}
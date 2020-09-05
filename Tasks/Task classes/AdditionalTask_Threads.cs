using System;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using InterfaceLib; //  Мой DLL-файл, подключен для вызова удобного интерфейса
using System.Runtime.Remoting.Messaging;

namespace Tasks.Task_classes
{
  static class AdditionalTask_Threads
  {
    public static async Task Execute()
    {
      string[] hashCodes = {
        "1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad", 
        "3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b", 
        "74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f"
      };

      bool back = false;
      while(!back)
      {
        Console.Clear();

        Console.WriteLine("Choose a hash code to decypher:");
        Lib.CallInterface(hashCodes);

        string input = Console.ReadLine();
        Console.Clear();
        switch (input)
        {
          case "1":
          case "2":
          case "3":
            await AsyncBruteForce(hashCodes[Convert.ToInt32(input) - 1]);
            break;
          case "exit":
            back = true;
            break;
          default:
            Console.WriteLine("Wrong input received, try again!\n");
            break;
        }
      }
    }

    static string CalculateHash(char[] input)
    {
      SHA256 hash = SHA256.Create();
      byte[] bytes = hash.ComputeHash(Encoding.ASCII.GetBytes(input));
      StringBuilder builder = new StringBuilder();
      foreach (byte b in bytes)
        builder.Append(b.ToString("x2"));
      return builder.ToString();
    }

    static async Task AsyncBruteForce(string hashToDecypher)
    {
      for (; ; )
      {
        if (CalculateHash() == hashToDecypher)
        {

        }
      }
    }
  }
}
using System;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using InterfaceLib; //  Мой DLL-файл, подключен для вызова удобного интерфейса
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Tasks.Task_classes
{
  static class AdditionalTask_AsyncHash
  {
    public static void Execute()
    {
      CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
      CancellationToken cancellationToken = cancellationTokenSource.Token;

      string[] hashCodes = {
        "1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad", 
        "3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b", 
        "74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f"
      };

      bool back = false;
      while (!back)
      {
        Console.Clear();

        Console.WriteLine("Choose a hash code to decipher:");
        Lib.CallInterface(hashCodes);

        string input = Console.ReadLine();
        switch (input)
        {
          case "1":
          case "2":
          case "3":
            Console.Write("\nEnter the amount of threads (1 - 4): ");
            int threadsCount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nThe chosen hash is being deciphered, please stand by...\n");

            Console.Write($"The password is: {BruteForceAsync(hashCodes[Convert.ToInt32(input) - 1], threadsCount).Result}");
            Console.ReadKey();
            break;
          case "exit":
            back = true;
            Console.WriteLine();
            break;
          default:
            Console.WriteLine("Wrong input received, try again!\n");
            Console.ReadKey();
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

    static void WordIterate(int iter, char[] word, string hash, char exitChar, CancellationToken token, ref bool isIterated, ref bool isFound)
    {
      while (true)
      {
        if (iter < 4)
        {
          WordIterate(iter + 1, word, hash, exitChar, token, ref isIterated, ref isFound);
        }
        if (!isFound && CalculateHash(word) == hash)
        {
          isFound = true;
        }
        if (word[0] == exitChar && word[1] == 'z' && word[2] == 'z' && word[3] == 'z' && word[4] == 'z')
        {
          isIterated = true;
        }
        if (word[iter] == 'z' || isIterated || token.IsCancellationRequested || isFound)
        {
          break;
        }
        word[iter]++;
      }
      if (isFound || isIterated)
      {
        return;
      }
      word[iter] = 'a';
    }

    static async Task<string> BruteForceAsync(string hashToDecypher, int threadsCount)
    {
      const int LATIN_ALPHABET_SIZE = 26, A_BEGINS_FROM = 97;

      bool[] breakers;
      bool isFound = false;

      CancellationTokenSource internalTokenSource = new CancellationTokenSource();
      CancellationToken internalCancellationToken = internalTokenSource.Token;

      List<char[]> groups = new List<char[]>();
      List<char> exitChars = new List<char>();
      Task[] tasks;

      if (LATIN_ALPHABET_SIZE % threadsCount == 0)
      {
        tasks = new Task[threadsCount];
        breakers = new bool[threadsCount];
      }
      else
      {
        tasks = new Task[threadsCount + 1];
        breakers = new bool[threadsCount + 1];
      }

      for (int i = 0; i < threadsCount; i++)
      {
        groups.Add(new[] { Convert.ToChar(A_BEGINS_FROM + i * (LATIN_ALPHABET_SIZE / threadsCount)), 'a', 'a', 'a', 'a'});
        exitChars.Add(Convert.ToChar(A_BEGINS_FROM + (i + 1) * (LATIN_ALPHABET_SIZE / threadsCount) - 1));
      }
      if (exitChars[exitChars.Count - 1] != 'z')
      {
        groups.Add(new[] { Convert.ToChar(Convert.ToInt32(exitChars[exitChars.Count - 1]) + 1), 'a', 'a', 'a', 'a'});
        exitChars.Add('z');
      }

      for (int t = 0; t < tasks.Length; t++)
      {
        int taskIndex = t;
        tasks[taskIndex] = Task.Run(() => WordIterate(0, groups[taskIndex], hashToDecypher, exitChars[taskIndex], internalCancellationToken, ref breakers[taskIndex], ref isFound));
      }

      await Task.Run(() =>
      {
        while (true)
        {
          if (isFound || breakers.All(breaker => breaker))
          {
            break;
          }
        }
      });

      internalTokenSource.Cancel();

      string password = string.Empty;
      foreach (var item in groups)
      {
        if (CalculateHash(item) == hashToDecypher)
        {
          password = new string(item);
          break;
        }
      }
      if (password != string.Empty)
      {
        return password;
      }
      else
      {
        return "Unable to decipher this hash: Password not found!";
      }
    }
  }
}
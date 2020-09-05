using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tasks.Task_classes
{
  class PlayerData
  {
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
  }
  static class Task3_JSON
  {
    public static async Task Execute()
    {
      string jsonLocation = @"D:\player.json";

      PlayerData player = new PlayerData
      {
        Name = "P1", Health = 100, Damage = 20
      };

      var jsonOptions = new JsonSerializerOptions()
      {
        WriteIndented = true
      };

      using (FileStream fStream = new FileStream(jsonLocation, FileMode.OpenOrCreate))
      {
        await JsonSerializer.SerializeAsync(fStream, player, jsonOptions);
        Console.WriteLine($"Data saved to {jsonLocation}");
      }

      using (FileStream fStream = new FileStream(jsonLocation, FileMode.OpenOrCreate))
      {
        PlayerData restoredPlayerData = await JsonSerializer.DeserializeAsync<PlayerData>(fStream);
        Console.WriteLine($"Name: {restoredPlayerData.Name}\tHealth: {restoredPlayerData.Health}\tDamage: {restoredPlayerData.Damage}");
      }

      Console.WriteLine("\nPress any key to procceed and delete created .json file...\n");
      Console.ReadKey();

      File.Delete(jsonLocation);

      Console.WriteLine("Created files deleted successfully!\n");
    }
  }
}
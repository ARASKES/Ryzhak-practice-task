using System;
using System.Collections.Generic;
using System.Xml;

namespace Tasks.Task_classes
{
  static class Task4_XML
  {
    class PlayerData
    {
      public string Name { get; set; }
      public int Health { get; set; }
      public int Damage { get; set; }
    }
    public static void Execute()
    {
      PlayerData player1 = new PlayerData
      {
        Name = "P1", Health = 100, Damage = 20
      };
      PlayerData player2 = new PlayerData
      {
        Name = "P2", Health = 80, Damage = 25
      };

      List<PlayerData> players = new List<PlayerData>();
      players.Add(player1);
      players.Add(player2);


    }
  }
}
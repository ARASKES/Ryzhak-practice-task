using System;
using System.IO;

namespace Tasks.Task_classes.Task1
{
  static class Task1_Drives
  {
    public static void Execute()
    {
      DriveInfo[] drives = DriveInfo.GetDrives();

      Console.WriteLine("Drives on this PC:\n");
      for (int i = 0; i < drives.Length; i++)
      {
        if (drives[i].IsReady)
        {
          Console.WriteLine($"{i + 1}. {drives[i].VolumeLabel} ({drives[i].Name}\b)");
          Console.WriteLine($"Drive format: {drives[i].DriveFormat}\tDrive type: {drives[i].DriveType}\n");

          long bytesToGygabytesCoef = 1024 * 1024 * 1024;
          Console.WriteLine($"{drives[i].TotalFreeSpace}B ({drives[i].TotalFreeSpace/bytesToGygabytesCoef}GB) of {drives[i].TotalSize}B ({drives[i].TotalSize/bytesToGygabytesCoef}GB) is free\n");
          Console.WriteLine($"Available free space: {drives[i].AvailableFreeSpace}B ({drives[i].AvailableFreeSpace/bytesToGygabytesCoef}GB)");
          Console.WriteLine("\n");
        }
      }
    }
  }
}
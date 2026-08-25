using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Session
{
   // public const string Path = "BestReplays.bst";
    public static string Path = Application.streamingAssetsPath  + "Games.db";
    public const int ElementsInList = 10;
    int FirstDeathes;
    int SecondDeathes;
    double Time;
    public Session(double time)
    {
        FirstDeathes = ArbitrScript.Tank1.GetComponent<Tank>().DeathCount;
        SecondDeathes = ArbitrScript.Tank2.GetComponent<Tank>().DeathCount;
        Time = time;
    }
    public override string ToString()
    {
        return $"{FirstDeathes,-5}    {SecondDeathes,-6}        {Math.Round(Time, 2)}\n";
    }
    public Session(int f, int s,double time)
    {
        FirstDeathes = f;
        SecondDeathes = s;
        Time = time;
    }

    private static int Compare(Session x, Session y)
    {
        if (x == null && y == null)
        {
            return 0;
        }
        else if (x == null)
        {
            return 1;
        }
        else if(y == null)
        {
            return -1;
        }
        else
        {
            if (x.Time == y.Time)
            {
                return 0;
            }
            else if (x.Time > y.Time)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }

    public void Add()
    {
        var all = ReadFromFile().ToList();
        all.Add(this);
        all.Sort(Compare);
        all.Remove(all[all.Count-1]);
        WriteToFile(all.ToArray());
    }

    void WriteToFile(Session[] all)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(Path, FileMode.OpenOrCreate)))
        {
            foreach (var s in all)
            {
                if (s == null)
                {
                    break;
                }
                writer.Write(s.FirstDeathes);
                writer.Write(s.SecondDeathes);
                writer.Write(s.Time);
            }
        }
    }
    public static Session[] ReadFromFile()
    {
        Session[] result = new Session[ElementsInList];
        using (BinaryReader reader = new BinaryReader(File.Open(Path, FileMode.OpenOrCreate)))
        {
            for(int i = 0; i < ElementsInList; i++)
            {
                if(reader.PeekChar() > -1)
                {
                    int first = reader.ReadInt32();
                    int second = reader.ReadInt32();
                    double time = reader.ReadDouble();
                    Session current = new Session(first, second, time);
                    result[i] = current;
                }
                else
                {
                    break;
                }
            }
        }
        return result;
    }
}

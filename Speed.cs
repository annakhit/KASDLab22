using System;
using System.Diagnostics;

public static class Speed
{
    public static double SpeedOfAddingToMyHashMap(int length)
    {
        double sum = 0;
        for (int i = 0; i < 20; i++)
        {
            MyHashMap<int, int> hashMap = new MyHashMap<int, int>();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int j = 0; j < length; j++) hashMap.Put(j, j);
            timer.Stop();
            sum += timer.ElapsedMilliseconds;
        }

        return sum;
    }

    public static double SpeedOfAddingToMyTreeMap(int length)
    {
        double sum = 0;
        for (int i = 0; i < 20; i++)
        {
            MyTreeMap<int, int> treeMap = new MyTreeMap<int, int> ();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int j = 0; j < length; j++) treeMap.Put(j, j);
            timer.Stop();
            sum += timer.ElapsedMilliseconds;
        }

        return sum;
    }

    public static double SpeedOfGettingFromMyHashMap(int length)
    {
        double sum = 0;

        MyHashMap<int, int> hashMap = new MyHashMap<int, int>();

        for (int j = 0; j < length; j++) hashMap.Put(j, j);

        for (int i = 0; i < 20; i++)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int j = 0; j < length; j++) hashMap.Get(j);
            timer.Stop();
            sum += timer.ElapsedMilliseconds;
        }

        return sum;
    }

    public static double SpeedOfGettingFromMyTreeMap(int length)
    {
        double sum = 0;

        MyTreeMap<int, int> treeMap = new MyTreeMap<int, int>();

        for (int j = 0; j < length; j++) treeMap.Put(j, j);

        for (int i = 0; i < 20; i++)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int j = 0; j < length; j++) treeMap.Get(j);
            timer.Stop();
            sum += timer.ElapsedMilliseconds;
        }

        return sum;
    }

    public static double SpeedOfRemovingFromMyHashMap(int length)
    {
        double sum = 0;

        MyHashMap<int, int> hashMap = new MyHashMap<int, int>();

        for (int j = 0; j < length * 20; j++) hashMap.Put(j, j);

        for (int i = 0; i < 20; i++)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int j = 0; j < length; j++) hashMap.Remove(j);
            timer.Stop();
            sum += timer.ElapsedMilliseconds;
        }

        return sum;
    }

    public static double SpeedOfRemovingFromMyTreeMap(int length)
    {
        double sum = 0;

        MyTreeMap<int, int> treeMap = new MyTreeMap<int, int>();

        for (int j = 0; j < length * 20; j++) treeMap.Put(j, j);

        for (int i = 0; i < 20; i++)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int j = 0; j < length; j++) treeMap.Remove(j);
            timer.Stop();
            sum += timer.ElapsedMilliseconds;
        }

        return sum;
    }
}
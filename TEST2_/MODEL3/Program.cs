
using System;
using System.Collections.Generic;
using System.Timers;

abstract class Plant
{
    public string Name { get; }
    public double Height { get; protected set; } // cm
    protected static readonly Random Rng = new Random();

    protected Plant(string name, double initialHeight = 0)
    {
        Name = name;
        Height = initialHeight;
    }

    public abstract void Grow();
}

class Flower : Plant
{
    public Flower(string name, double initialHeight = 0) : base(name, initialHeight) { }

    public override void Grow()
    {
        double inc = 0.1 + Rng.NextDouble() * (0.5 - 0.1);
        Height += inc;
    }
}

class Tree : Plant
{
    public Tree(string name, double initialHeight = 0) : base(name, initialHeight) { }

    public override void Grow()
    {
        double inc = 0.5 + Rng.NextDouble() * (2.0 - 0.5);
        Height += inc;
    }
}

class GardenManager
{
    private readonly List<Plant> plants = new List<Plant>();

    public void Add(Plant p) => plants.Add(p);

    public void UpdateGrowth()
    {
        foreach (var p in plants) p.Grow();
        PrintHeights();
    }

    public void PrintHeights()
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Heights:");
        foreach (var p in plants)
            Console.WriteLine($" - {p.Name}: {p.Height:F2} cm");
        Console.WriteLine();
    }
}

class GrowthTimer
{
    private readonly System.Timers.Timer timer;
    public event EventHandler Tick;

    public GrowthTimer(int intervalMs)
    {
        timer = new System.Timers.Timer(intervalMs);
        timer.Elapsed += (s, e) => Tick?.Invoke(this, EventArgs.Empty);
    }

    public void Start() => timer.Start();
    public void Stop() => timer.Stop();
}


class Program
{
    public static void AsyncMain(string[] args)
    {
        var manager = new GardenManager();

        manager.Add(new Flower("Flower A", 5.0));
        manager.Add(new Flower("Flower B", 4.5));
        manager.Add(new Tree("Tree A", 50.0));
        manager.Add(new Tree("Tree B", 30.0));

        var growthTimer = new GrowthTimer(1000); // 1s interval
        growthTimer.Tick += (s, e) => manager.UpdateGrowth();

        Console.WriteLine("Initial state:");
        manager.PrintHeights();

        Console.WriteLine("Starting growth timer. Press ENTER to stop.");
        growthTimer.Start();

        Console.ReadLine();
        growthTimer.Stop();
        Console.WriteLine("Timer stopped. Final state:");
        manager.PrintHeights();
    }
    static bool Predicat(int x)
    {
        if(x%2==0)
            return true;
        else return false;
    }

    static int Transformer(int x)
    {
        return x/2;
    }
    static (List<int>, List<int>) MapAndPartition(List<int> list, Func<int,int> transformer, Func<int,bool> predicate)
    {
        if(list.Count==0)
            throw new ArgumentNullException("list is empty");

        List<int> satisface = new List<int>();
        List<int> nu_satisface = new List<int>();

        foreach(int elem in list)
            if(predicate(elem))
                satisface.Add(transformer(elem));
            else nu_satisface.Add(elem);

        return (satisface, nu_satisface);
    }

    static void printList(List<int> list)
    {
        foreach(int elem in list)
        {
            Console.Write(elem+" ");
        }
        Console.WriteLine();
    }
    public static void Main(string[] args)
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        (List<int> satisface, List<int> nu_satisface) = MapAndPartition(list, Transformer, Predicat);
        printList(satisface);
        printList(nu_satisface);

        AsyncMain(args);
    }
}
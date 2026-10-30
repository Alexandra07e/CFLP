using System;


// ...existing code...
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

abstract class Vehicle
{
    public string Brand { get; }
    public double Speed { get; private set; }

    protected Vehicle(string brand)
    {
        Brand = brand;
        Speed = 0;
    }

    protected void IncreaseSpeed(double delta) => Speed += delta;

    public abstract void Accelerate();

    public override string ToString() => $"{Brand}: {Speed:F2} km/h";
}

class Car : Vehicle
{
    private static readonly Random _rand = new Random();
    public Car(string brand) : base(brand) { }

    public override void Accelerate()
    {
        double inc = _rand.NextDouble() * (30 - 10) + 10; // [10,30)
        IncreaseSpeed(inc);
    }
}

class Bike : Vehicle
{
    private static readonly Random _rand = new Random();
    public Bike(string brand) : base(brand) { }

    public override void Accelerate()
    {
        double inc = _rand.NextDouble() * (15 - 5) + 5; // [5,15)
        IncreaseSpeed(inc);
    }
}

class FleetManager
{
    private readonly List<Vehicle> _vehicles = new List<Vehicle>();

    public void AddVehicle(Vehicle v) => _vehicles.Add(v);

    public void ShowStatus()
    {
        foreach (var v in _vehicles)
            Console.WriteLine(v);
    }

    public void AccelerateAll()
    {
        foreach (var v in _vehicles)
            v.Accelerate();
    }
}

class SpeedTimer
{
    public event EventHandler Tick;
    private readonly int _intervalMs;

    public SpeedTimer(int intervalMs) => _intervalMs = intervalMs;

    public async Task StartAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(_intervalMs, ct);
            Tick?.Invoke(this, EventArgs.Empty);
        }
    }
}



class Program
{
    static bool Predicate(int x)
    {
        if(x%2==0)
            return true;
        else
            return false;
    }

    static string Transformer(int x)
    {
        return x.ToString();
    }

    public static List<string> FilterAndTransform(List<int> intList, Func<int,bool> predicate, Func<int,string> transformer)
    {
        List <string> result = new List<string>();
        if(intList.Count==0)
            throw new ArgumentNullException("list is empty");

        foreach (int elem in intList)
        {
            if(predicate(elem))
                result.Add(transformer(elem));
        }

        return result;
    }

    static void printList(List<string> list)
    {
        foreach(string str in list)
            Console.Write(str+" ");
        Console.WriteLine();
    }

    static int Divby3(List<string> list)
    {
        return list .Where(s=> !string.IsNullOrEmpty(s))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .Count(s => s.Length % 3 == 0);
    }

    static bool Condition(int x)
    {
        if(x%2==1)
            return true;
        else return false;
    }

    static bool respectCondition(List<int> list, Func<int,bool> function)
    {
        return list.All(elem => function(elem));
    }

     static async Task MainAsync(string[] args)
    {
        var fleet = new FleetManager();
        fleet.AddVehicle(new Bike("Bike A"));
        fleet.AddVehicle(new Bike("Bike B"));
        fleet.AddVehicle(new Car("Car 1"));

        var timer = new SpeedTimer(1000);
        timer.Tick += (s, e) =>
        {
            fleet.AccelerateAll();
            fleet.ShowStatus();
            Console.WriteLine(new string('-', 20));
        };

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)); // runs ~10s
        await timer.StartAsync(cts.Token);
    }
    public static async Task Main(string[] args)
    {
        List<int> intList = new List<int>();
        intList.Add(2);
        intList.Add(3);
        intList.Add(4);
        intList.Add(5);
        intList.Add(6);
        List<string> result = FilterAndTransform(intList, Predicate, Transformer);
        printList(result);


        List<string> list = new List<string>();
        list.Add("Maria");
        list.Add("Alexan");
        list.Add("alexandra");
        list.Add("Alexandra");
        list.Add("Ioana");
        Console.WriteLine(Divby3(list));
        Console.WriteLine(respectCondition(intList, Condition));

        await MainAsync(args);
    }
}


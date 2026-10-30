using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

delegate int TransformerDelegate(int x);
delegate int AggregatorDelegate(IEnumerable<int> seq);

abstract class RoomSensor
{
    public string Name { get; }
    public float Temperature { get; private set; }
    protected RoomSensor(string name) 
    { 
        Name = name; 
    }
    protected void SetTemperature(float value) => Temperature = value;
    public abstract void UpdateTemperature();
}

class OfficeSensor : RoomSensor
{
    private static readonly Random _rand = new Random();
    public OfficeSensor(string name) : base(name) { }
    public override void UpdateTemperature() => SetTemperature((float)(_rand.NextDouble() * (25.0 - 18.0) + 18.0));
}

class HallSensor : RoomSensor
{
    private static readonly Random _rand = new Random();
    public HallSensor(string name) : base(name) { }
    public override void UpdateTemperature() => SetTemperature((float)(_rand.NextDouble() * (22.0 - 15.0) + 15.0));
}

class BuildingManager
{
    private readonly List<RoomSensor> _sensors = new List<RoomSensor>();
    public void AddSensor(RoomSensor sensor) => _sensors.Add(sensor);
    public void OnTimerTick(object? sender, EventArgs e)
    {
        foreach (var s in _sensors)
        {
            s.UpdateTemperature();
            Console.WriteLine($"{DateTime.Now:HH:mm:ss} {s.Name}: {s.Temperature:F1}°C");
        }
    }
}

class BuildingTimer
{
    private readonly int _intervalMs;
    public event EventHandler? Tick;
    public BuildingTimer(int intervalMs) 
    { 
        _intervalMs = intervalMs; 
    }
    public async Task StartAsync()
    {
        while (true)
        {
            await Task.Delay(_intervalMs);
            Tick?.Invoke(this, EventArgs.Empty);
        }
    }
}

class Program
{
    static int Transformer(int x)
    {
        if (x % 2 == 0) 
            return x + 1;
        return x + 2;
    }

    static int TransformAndAggregate(List<int> intList, Func<int,int> transformer, Func<IEnumerable<int>,int> aggregator)
    {
        var transformed = new List<int>();
        foreach (var v in intList) transformed.Add(transformer(v));
        return aggregator(transformed);
    }


    static double? AverageOfPositives(IEnumerable<double> numbers)
    {
        var positives = numbers.Where(n => n > 0);
        return positives.Any() ? (double?)positives.Average() : null;
    }

    static bool AllDistinctCaseInsensitiveLengthAtLeast3(IEnumerable<string> strings)
    {
        return strings
            .Select(s => s?.ToLowerInvariant() ?? string.Empty)
            .Distinct()
            .All(s => s.Length >= 3);
    }

    public static async Task Main(string[] args)
    {
        //ex1
        var ints = new List<int> { 1, 2, 3, 4, 5 };
        TransformerDelegate td = Transformer;
        AggregatorDelegate ag = seq => seq.Sum();
        Console.WriteLine(TransformAndAggregate(ints, x => td(x), seq => ag(seq)));
        Console.WriteLine();
        Console.WriteLine();
        
        //ex2a)
        var doublesWithPos = new List<double> { -1.0, 0.0, 2.5, 3.5 };
        var doublesNoPos = new List<double> { -5.0, -2.2 };
        Console.WriteLine(AverageOfPositives(doublesWithPos)?.ToString() ?? "null");
        Console.WriteLine(AverageOfPositives(doublesNoPos)?.ToString() ?? "null");
        Console.WriteLine();
        Console.WriteLine();

        //ex2b)
        var stringsGood = new List<string> { "One", "two", "ONE", "Three" };
        var stringsBad = new List<string> { "ab", "Ab", "ABC" };
        Console.WriteLine(AllDistinctCaseInsensitiveLengthAtLeast3(stringsGood));
        Console.WriteLine(AllDistinctCaseInsensitiveLengthAtLeast3(stringsBad));
        Console.WriteLine();
        Console.WriteLine();

        //ex3
        var manager = new BuildingManager();
        manager.AddSensor(new OfficeSensor("Office 1"));
        manager.AddSensor(new OfficeSensor("Office 2"));
        manager.AddSensor(new HallSensor("Hall 1"));
        manager.AddSensor(new HallSensor("Hall 2"));
        var timer = new BuildingTimer(1500);
        timer.Tick += manager.OnTimerTick;
        await timer.StartAsync();
    }
}
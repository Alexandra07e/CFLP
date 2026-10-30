
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


abstract class SmartDevice
{
    public string Name { get; }
    public string Status { get; protected set; }

    protected SmartDevice(string name)
    {
        Name = name;
        Status = "Unknown";
    }

    public abstract void UpdateStatus();

    public override string ToString() => $"{Name}: {Status}";
}

class LightBulb : SmartDevice
{
    public LightBulb(string name) : base(name) { }

    public override void UpdateStatus()
    {
        Status = DeviceUtils.Rng.Next(2) == 0 ? "On" : "Off";
    }
}

class SmartPlug : SmartDevice
{
    public SmartPlug(string name) : base(name) { }

    public override void UpdateStatus()
    {
        Status = DeviceUtils.Rng.Next(2) == 0 ? "Connected" : "Disconnected";
    }
}

static class DeviceUtils
{
    public static readonly Random Rng = new Random();
}

class DeviceManager
{
    private readonly List<SmartDevice> devices = new List<SmartDevice>();

    public void AddDevice(SmartDevice device) => devices.Add(device);

    // Compatible with EventHandler so it can be subscribed to Tick
    public void RefreshAll(object? sender, EventArgs? e)
    {
        Console.WriteLine($"Refreshing devices at {DateTime.Now:T}");
        foreach (var d in devices)
        {
            d.UpdateStatus();
            Console.WriteLine(d);
        }
        Console.WriteLine();
    }
}

class DeviceTimer
{
    public event EventHandler? Tick;

    public async Task StartAsync(int intervalMilliseconds, CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(intervalMilliseconds, ct);
            }
            catch (TaskCanceledException)
            {
                break;
            }

            Tick?.Invoke(this, EventArgs.Empty);
        }

        ct.ThrowIfCancellationRequested();
    }
}
class Program
{
    static int Transformer(int x)
    {
        return x+2;
    }
    static int ApplyAndSum(List<int> list, Func<int,int> transformer)
    {
        int sum=0;
        if(list.Count==0)   
            throw new ArgumentNullException("list is empty");

        foreach(int elem in list)
            sum += Transformer(elem);

        return sum;
    }

    static int wordNumber(List<string> props)
    {
        char[] punct = {'.', ',', '?', '!'};
        return props    .Where(s => s.IndexOfAny(punct)>=0)
                        .Sum(s => s.Split(new[] {' '}).Length);
    }

    static bool Predicate(float x)
    {
        if(x>0)
            return true;
        else return false;
    }

    static bool verify(List<float> list, Func<float,bool> predicat)
    {
        return list.Any(elem => predicat(elem));
    }

     public static async Task AsyncMain(string[] args)
    {
        var manager = new DeviceManager();

        manager.AddDevice(new LightBulb("Bulb A"));
        manager.AddDevice(new LightBulb("Bulb B"));
        manager.AddDevice(new SmartPlug("Plug A"));
        manager.AddDevice(new SmartPlug("Plug B"));

        var timer = new DeviceTimer();
        timer.Tick += manager.RefreshAll;

        using var cts = new CancellationTokenSource();
        var timerTask = timer.StartAsync(2000, cts.Token); // tick every 2s

        Console.WriteLine("Timer started. Running for 10 seconds...");
        await Task.Delay(10000);
        cts.Cancel();

        try { await timerTask; } catch (OperationCanceledException) { }

        Console.WriteLine("Timer stopped.");
    }
    public static async Task Main(string[] args)
    {
        List<int> list=new List<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Console.WriteLine(ApplyAndSum(list, Transformer));

        List<string> props=new List<string>();
        props.Add("maria are mere?");
        props.Add("ce faci, bro?");
        props.Add("urat asa gen");
        props.Add("zip zip zip zip. zip zip");

        List<float> list2 = new List<float>();
        list2.Add(-1);
        list2.Add(-2);
        list2.Add(-3);
        list2.Add(-4);
        list2.Add(5);

        Console.WriteLine(wordNumber(props));
        Console.WriteLine(verify(list2, Predicate));

        await AsyncMain(args);

    }
}

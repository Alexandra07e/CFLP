using System;
using System.Threading.Tasks;

public class CititorIntrare
{
    public event EventHandler<string>? OnKeyPressed;

    public void ReadKeys()
    {
        while (true)
        {
            string line = Console.ReadLine()!;
            if (string.IsNullOrEmpty(line))
                break;

            OnKeyPressed?.Invoke(this, line);
        }
    }
}


public class Tamagotchi
{
    public string Nume { get; }
    public int Mancare { get; private set; }
    public int RataFoamei { get; }
    public bool InViata { get; private set; }
    public char Tasta { get; }

    public Tamagotchi(string nume, int mancareInitiala, int rataFoamei, char tasta, CititorIntrare reader)
    {
        Nume = nume;
        Mancare = mancareInitiala;
        RataFoamei = rataFoamei;
        Tasta = tasta;
        InViata = true;

        reader.OnKeyPressed += HandleKeyPressed;
    }

    private void HandleKeyPressed(object? sender, string input)
    {
        if (!InViata)
            return;

        if (input.Length == 1 && input[0] == Tasta)
        {
            Mancare += 5;
            Console.WriteLine($"{Nume} a fost hrănit! (+5)");

            if (Mancare > 20)
            {
                InViata = false;
                Console.WriteLine($"{Nume} a murit din cauza hrănirii excesive! :(");
            }
        }
    }

    public override string ToString()
    {
        if (InViata)
            return $"{Nume} este sănătos și în viață. Mâncare rămasă: {Mancare}";
        else
            return $"{Nume} este mort :(";
    }

    public async Task Run()
    {
        while (InViata)
        {
            await Task.Delay(RataFoamei);
            Mancare -= 3;
            if (Mancare <= 0)
            {
                Mancare = 0;
                InViata = false;
                Console.WriteLine($"{Nume} a murit de foame! :(");
                break;
            }

            if (Mancare > 20)
            {
                InViata = false;
                Console.WriteLine($"{Nume} a murit din cauza hrănirii excesive! :(");
                break;
            }

            Console.WriteLine(ToString());
        }
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        CititorIntrare reader = new CititorIntrare();

        Tamagotchi t1 = new Tamagotchi("Pisu", 10, 2000, 'p', reader);
        Tamagotchi t2 = new Tamagotchi("Tomita", 12, 1500, 't', reader);

        Console.WriteLine("Hraneste Tamagotchi apasand tasta corecta (ex: 'p', 't')");
        Console.WriteLine("Apasa ENTER pentru a opri.");

        Task run1 = t1.Run();
        Task run2 = t2.Run();
        reader.ReadKeys();

        await Task.WhenAll(run1, run2);
        Console.WriteLine("Jocul s-a terminat!");
    }
}
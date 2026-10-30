using System;
using System.Collections.Generic;
using System.Linq;

public class Joc
{
    public string Nume { get; }
    public string Dezvoltator { get; }
    public DateTime DataLansare { get; }
    public float Pret { get; }
    public List<string> Etichete { get; }

    public Joc(string nume, string dezvoltator, DateTime dataLansare, float pret, List<string> etichete)
    {
        if (string.IsNullOrWhiteSpace(nume))
            throw new ArgumentException("Numele jocului nu poate fi gol.");

        if (string.IsNullOrWhiteSpace(dezvoltator))
            throw new ArgumentException("Numele dezvoltatorului nu poate fi gol.");

        if (pret < 0)
            throw new ArgumentException("Prețul nu poate fi negativ.");

        if (etichete == null)
            throw new ArgumentNullException(nameof(etichete));

        Nume = nume;
        Dezvoltator = dezvoltator;
        DataLansare = dataLansare;
        Pret = pret;
        Etichete = etichete;
    }

    public override string ToString()
    {
        string tags = Etichete.Count == 0 ? "Fără etichete" :
                       string.Join(", ", Etichete);
        return $"{Nume} ({Dezvoltator}) - Lansat la {DataLansare:dd.MM.yyyy}, Preț: {Pret} lei, Etichete: {tags}";
    }
}

public class Magazin
{
    private List<Joc> jocuri;
    public Magazin()
    {
        jocuri = new List<Joc>();
    }

    public void AdaugaJoc(Joc joc)
    {
        if (joc == null)
            throw new ArgumentNullException(nameof(joc));
        jocuri.Add(joc);
    }

    public List<Joc> JocuriInainteDe(DateTime data)
    {
        return jocuri
            .Where(j => j.DataLansare < data)
            .ToList();
    }

    public int JocuriIntre(int an1, int an2)
    {
        return jocuri
            .Count(j => j.DataLansare.Year >= an1 && j.DataLansare.Year <= an2);
    }

    public List<Joc> OrdoneazaDupaDezvoltatorSiNume()
    {
        return jocuri
            .OrderBy(j => j.Dezvoltator)
            .ThenBy(j => j.Nume)
            .ToList();
    }

    public List<Joc> JocuriDupaDezvoltatorOrdonateDupaPret(string dezvoltator, bool descrescator = false)
    {
        if (descrescator)
        {
            return jocuri
                .Where(j => j.Dezvoltator.Equals(dezvoltator, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(j => j.Pret)
                .ToList();
        }

        return jocuri
            .Where(j => j.Dezvoltator.Equals(dezvoltator, StringComparison.OrdinalIgnoreCase))
            .OrderBy(j => j.Pret)
            .ToList();
    }

    public Joc PrimulJocAlDezvoltatorului(string dezvoltator)
    {
        return jocuri
            .Where(j => j.Dezvoltator.Equals(dezvoltator, StringComparison.OrdinalIgnoreCase))
            .OrderBy(j => j.DataLansare)
            .FirstOrDefault()!;
    }

    public bool JocMaiScumpDecat(float pret)
    {
        return jocuri
            .Any(j => j.Pret > pret);
    }

    public float SumaPreturilorPentruJocurileCuEticheta(string eticheta)
    {
        return jocuri
            .Where(j => j.Etichete.Contains(eticheta))
            .Sum(j => j.Pret);
    }

    public List<Joc> JocuriCareContinCelPutinOEticheta(List<string> etichete)
    {
        return jocuri
            .Where(j => j.Etichete.Intersect(etichete).Any())
            .ToList();
    }
}

public class Program
{
    public static void Main()
    {
        Magazin m = new Magazin();

        Joc j1 = new Joc("Elden Ring", "FromSoftware", new DateTime(2022, 2, 25), 299, new List<string>{"RPG", "OpenWorld"});
        Joc j2 = new Joc("Sekiro", "FromSoftware", new DateTime(2019, 3, 22), 249, new List<string>{"SoulsLike"});
        Joc j3 = new Joc("Witcher 3", "CD Projekt", new DateTime(2015, 5, 19), 99, new List<string>{"RPG", "Story"});

        m.AdaugaJoc(j1);
        m.AdaugaJoc(j2);
        m.AdaugaJoc(j3);

        Console.WriteLine("     1. Jocuri inainte de 22/2/2022:");
        foreach(var joc in m.JocuriInainteDe(new DateTime(2022, 2, 22)))
            Console.WriteLine(joc);

        Console.WriteLine("\n   2. Jocuri intre 2019 si 2022: "+m.JocuriIntre(2019,2022));

        Console.WriteLine("\n   3. Ordoneaza Dupa Dezvoltator Si Nume:");
        foreach(var joc in m.OrdoneazaDupaDezvoltatorSiNume())
            Console.WriteLine(joc);

        Console.WriteLine("\n   4. Jocuri Dupa Dezvoltator Ordonate Dupa Pret - dezvoltator = FromSoftware:");
        foreach(var joc in m.JocuriDupaDezvoltatorOrdonateDupaPret("FromSoftware", true))
            Console.WriteLine(joc);

        Console.WriteLine("\n   5. Primul Joc Al Dezvoltatorului 'FromSoftware': \n"+m.PrimulJocAlDezvoltatorului("FromSoftware"));

        Console.WriteLine("\n   6. Joc Mai Scump Decat 50: "+m.JocMaiScumpDecat(50));

        Console.WriteLine("\n   7. Suma Preturilor Pentru Jocurile Cu Eticheta 'RPG': "+ m.SumaPreturilorPentruJocurileCuEticheta("RPG"));

        List<string> etichete=new List<string>{"RPG", "OpenWorld"};
        Console.WriteLine("\n   8. Jocuri Care Contin Cel Putin O Eticheta: ");
        foreach(var joc in m.JocuriCareContinCelPutinOEticheta(etichete))
            Console.WriteLine(joc);
    }
}


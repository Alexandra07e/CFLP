using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Car
{
    public int ID { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public int Mileage { get; set; }
    public string FuelType { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        string filePath = "cars.csv";

        var lines = File.ReadAllLines(filePath);

        var cars = lines.Skip(1)
                        .Select(line =>
                        {
                            var c = line.Split(',');
                            return new Car
                            {
                                ID = int.Parse(c[0]),
                                Make = c[1],
                                Model = c[2],
                                Year = int.Parse(c[3]),
                                Price = decimal.Parse(c[4]),
                                Mileage = int.Parse(c[5]),
                                FuelType = c[6]
                            };
                        })
                        .ToList();

        // 1. Selectează toate automobilele fabricate după anul 2018.

        Console.WriteLine("\n   1) Masini fabricate dupa 2018");

        var q1_dec = from car in cars
                     where car.Year > 2018
                     select car;

        var q1_proc = cars.Where(c => c.Year > 2018);

        Console.WriteLine("Declarativ:");
        foreach (var car in q1_dec)
            Console.WriteLine($"{car.Make} {car.Model} ({car.Year})");

        Console.WriteLine("Procedural:");
        foreach (var car in q1_proc)
            Console.WriteLine($"{car.Make} {car.Model} ({car.Year})");


        // 2. Afișează toate automobilele care au tipul combustibilului "Electric".

        Console.WriteLine("\n   2) Masini electrice");

        var q2_dec = from car in cars
                     where car.FuelType == "Electric"
                     select car;

        var q2_proc = cars.Where(c => c.FuelType == "Electric");

        Console.WriteLine("Declarativ:");
        foreach (var car in q2_dec)
            Console.WriteLine($"{car.Make} {car.Model}");

        Console.WriteLine("Procedural:");
        foreach (var car in q2_proc)
            Console.WriteLine($"{car.Make} {car.Model}");


        // 3. Găsește mașina cu cel mai mare preț.
        Console.WriteLine("\n   3) Masina cu cel mai mare pret");

        var q3_dec = (from car in cars
                      orderby car.Price descending
                      select car).First();

        var q3_proc = cars.OrderByDescending(c => c.Price).First();

        Console.WriteLine("Declarativ:");
        Console.WriteLine($"{q3_dec.Make} {q3_dec.Model} - {q3_dec.Price}");

        Console.WriteLine("Procedural:");
        Console.WriteLine($"{q3_proc.Make} {q3_proc.Model} - {q3_proc.Price}");


//4. Calculează prețul mediu al tuturor mașinilor care au un kilometraj sub 50,000.
        Console.WriteLine("\n   4) Pret mediu pt masini km < 50000");

        var q4_dec = (from car in cars
                      where car.Mileage < 50000
                      select car.Price).Average();

        var q4_proc = cars.Where(c => c.Mileage < 50000)
                          .Average(c => c.Price);

        Console.WriteLine("Declarativ: " + q4_dec);
        Console.WriteLine("Procedural: " + q4_proc);


//5. Grupați mașinile după tipul combustibilului și afișați câte mașini sunt în fiecare grupă.
        Console.WriteLine("\n   5) Grupare dupa combustibil");

        var q5_dec = from car in cars
                     group car by car.FuelType into g
                     select new { Fuel = g.Key, Count = g.Count() };

        var q5_proc = cars.GroupBy(c => c.FuelType)
                          .Select(g => new { Fuel = g.Key, Count = g.Count() });

        Console.WriteLine("Declarativ:");
        foreach (var g in q5_dec)
            Console.WriteLine($"{g.Fuel}: {g.Count}");

        Console.WriteLine("Procedural:");
        foreach (var g in q5_proc)
            Console.WriteLine($"{g.Fuel}: {g.Count}");


        // 6. Selectează modelele și anii de fabricație ale mașinilor care costă mai puțin de 20,000 USD.

        Console.WriteLine("\n   6) Modele + ani cu pret < 20000");

        var q6_dec = from car in cars
                     where car.Price < 20000
                     select new { car.Model, car.Year };

        var q6_proc = cars.Where(c => c.Price < 20000)
                          .Select(c => new { c.Model, c.Year });

        Console.WriteLine("Declarativ:");
        foreach (var x in q6_dec)
            Console.WriteLine($"{x.Model} ({x.Year})");

        Console.WriteLine("Procedural:");
        foreach (var x in q6_proc)
            Console.WriteLine($"{x.Model} ({x.Year})");


        // 7. Afișează mașinile fabricate de "Toyota" și "Honda".

        Console.WriteLine("\n   7)Toyota si Honda");

        var q7_dec = from car in cars
                     where car.Make == "Toyota" || car.Make == "Honda"
                     select car;

        var q7_proc = cars.Where(c => c.Make == "Toyota" || c.Make == "Honda");

        Console.WriteLine("Declarativ:");
        foreach (var c in q7_dec)
            Console.WriteLine($"{c.Make} {c.Model}");

        Console.WriteLine("Procedural:");
        foreach (var c in q7_proc)
            Console.WriteLine($"{c.Make} {c.Model}");


        // 8. Sortează toate mașinile în ordine descrescătoare după anul fabricației.

        Console.WriteLine("\n   8) Sortare descrescatoare dupa an");

        var q8_dec = from car in cars
                     orderby car.Year descending
                     select car;

        var q8_proc = cars.OrderByDescending(c => c.Year);

        Console.WriteLine("Declarativ:");
        foreach (var c in q8_dec)
            Console.WriteLine($"{c.Make} {c.Model} ({c.Year})");

        Console.WriteLine("Procedural:");
        foreach (var c in q8_proc)
            Console.WriteLine($"{c.Make} {c.Model} ({c.Year})");


        // 9. Selectează doar ID-urile mașinilor care au un kilometraj mai mare de 80,000.

        Console.WriteLine("\n   9)ID-uri cu km > 80000");

        var q9_dec = from car in cars
                     where car.Mileage > 80000
                     select car.ID;

        var q9_proc = cars.Where(c => c.Mileage > 80000)
                          .Select(c => c.ID);

        Console.WriteLine("Declarativ:");
        foreach (var id in q9_dec)
            Console.WriteLine(id);

        Console.WriteLine("Procedural:");
        foreach (var id in q9_proc)
            Console.WriteLine(id);


        // 10. Găsește suma totală a prețurilor tuturor mașinilor fabricate înainte de 2015.

        Console.WriteLine("\n   10) Suma preturilor masinilor inainte de 2015");

        var q10_dec = (from car in cars
                       where car.Year < 2015
                       select car.Price).Sum();

        var q10_proc = cars.Where(c => c.Year < 2015)
                           .Sum(c => c.Price);

        Console.WriteLine("Declarativ: " + q10_dec);
        Console.WriteLine("Procedural: " + q10_proc);
    }
}
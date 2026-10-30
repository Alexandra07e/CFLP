using System;
using System.Globalization;

abstract class Animal_de_casa
{
    public int an_nastere {get; protected set;}
    public string rasa {get; protected set;}
    public string nume {get; protected set;}

    public Animal_de_casa(int an_nastere, string rasa, string nume)
    {
        this.an_nastere=an_nastere;
        this.rasa=rasa;
        this.nume=nume;
    }

    public abstract void Salut();
    public override string ToString()
    {
        return "Rasa: "+rasa+", Nume: "+nume+", An Nastere: "+an_nastere+", ";
    }
}

class Animal_de_casa_de_acvariu : Animal_de_casa{
    public double volum_dm_cubi {get; protected set;}

    public Animal_de_casa_de_acvariu(int an_nastere, string rasa, string nume, double volum_dm_cubi) : base(an_nastere, rasa, nume)
    {
        this.volum_dm_cubi=volum_dm_cubi;
    }

    public override void Salut()
    {
        Console.WriteLine("Salut, sunt Animal_de_casa_de_acvariu");
    }

    public override string ToString()
    {
        return base.ToString()+"Volum: "+volum_dm_cubi+" dm^3";
    }
}

class Animal_de_casa_de_terariu : Animal_de_casa
{
    public double suprafata_dm_patrati {get; protected set;}

    public Animal_de_casa_de_terariu(int an_nastere, string rasa, string nume, double suprafata_dm_patrati) : base(an_nastere, rasa, nume)
    {
        this.suprafata_dm_patrati=suprafata_dm_patrati;
    }

    public override void Salut()
    {
        Console.WriteLine("Salut, sunt Animal_de_casa_de_terariu");
    }

    public override string ToString()
    {
        return base.ToString()+"Suprafata: "+suprafata_dm_patrati+" dm^2";
    }
}
class Student16
{

    public static void Hotel04(ref float a, ref float b)
    {
        if(a>79 && b > 79)
        {
            float aux=a;
            a=b;
            b=aux;
        } else Console.WriteLine("Numerele nu sunt mai mari decat 79");
    }

    public static float ConvertToFloat(string input)
    {
        float value;
        var culture = CultureInfo.CurrentCulture;
        while (!float.TryParse(input, NumberStyles.Float | NumberStyles.AllowThousands, culture, out value))
        {
            Console.WriteLine("Intrare invalidă");
            input = Console.ReadLine()!;
        }
        return value;
    }

    public delegate bool MyDelegate1(byte x);
    public delegate byte MyDelegate2(byte x);

    public static bool Conditie(byte x)
    {
        if(x>5)
            return true;
        else return false;
    }

    public static byte Transformare(byte x)
    {
        return (byte)(x*2);
    }

    public static List<byte> Victor32(List<byte> list, MyDelegate1 f1, MyDelegate2 f2)
    {
        if(list.Count==0)
            throw new ArgumentNullException("Lista este goala");
        
        List<byte> result=new List<byte>();
        foreach(byte element in list)
        {
            if(!f1(element))
                result.Add(f2(element));
            else result.Add(element);
        }
        return result;
    }

    public static void PrintList(List<byte> list)
    {
        foreach(byte element in list){
            Console.Write(element+" ");
        }
        Console.WriteLine();
    }

    public static void Main(string[] args)
    {
        //ex1
        Console.WriteLine("EXERCITIUL 1:");
        Console.Write("a = ");
        float a = ConvertToFloat(Console.ReadLine()!);
        Console.Write("b = ");
        float b = ConvertToFloat(Console.ReadLine()!);
        Console.WriteLine("Nr initiale: a = "+a+"  b = "+b);
        Hotel04(ref a, ref b);
        Console.WriteLine("Dupa swap: a = "+a+"  b = "+b);

        //ex2
        Console.WriteLine("\nEXERCITIUL 2:");
        List<byte> list = new List<byte>();
        list.Add(7);
        list.Add(8);
        list.Add(9);
        list.Add(2);
        list.Add(3);
        Console.Write("Lista initiala: ");
        PrintList(list);
        List<byte> result=Victor32(list, Conditie, Transformare);
        Console.Write("Lista rezultata: ");
        PrintList(result);

        //ex3
        Console.WriteLine("\nEXERCITIUL 3:");
        List<Animal_de_casa> colectie = new List<Animal_de_casa>();
        Animal_de_casa_de_acvariu a1=new Animal_de_casa_de_acvariu(2020, "peste", "Nemo", 128.54);
        Animal_de_casa_de_acvariu a2=new Animal_de_casa_de_acvariu(2017, "mormoloc", "Squidward", 78);
        Animal_de_casa_de_terariu a3=new Animal_de_casa_de_terariu(2022, "pisica", "Matzel", 13);
        Animal_de_casa_de_terariu a4=new Animal_de_casa_de_terariu(2019, "caine", "Azorel", 20.78);
        Animal_de_casa_de_terariu a5=new Animal_de_casa_de_terariu(2018, "soarece", "Jerry", 2);

        colectie.Add(a1);
        colectie.Add(a2);
        colectie.Add(a3);
        colectie.Add(a4);
        colectie.Add(a5);

        foreach(Animal_de_casa animal in colectie)
        {
            animal.Salut();
        }
        Console.WriteLine();

        foreach(Animal_de_casa animal in colectie)
        {
            Console.WriteLine(animal.ToString());
        }
    }
}
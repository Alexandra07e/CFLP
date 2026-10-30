using System;

abstract class Bicicleta
{
    public string marca{get; protected set;}
    public double pret{get; protected set;}
    public int an{get; protected set;}

    public Bicicleta(string marca, double pret, int an)
    {
        this.marca=marca;
        this.pret=pret;
        this.an=an;
    }

    public abstract void Salut();

    public override string ToString()
    {
        return marca+" "+pret+" "+an+" ";
    } 
}

class Bicicleta_munte : Bicicleta
{
    public double latime_ghidon{get; protected set;}

    public Bicicleta_munte(string marca, double pret, int an, double latime_ghidon) : base(marca,pret,an)
    {
        this.latime_ghidon=latime_ghidon;
    }

    public override void Salut()
    {
        Console.WriteLine("Salut, sunt Bicicleta_munte");
    }

    public override string ToString()
    {
        return base.ToString()+latime_ghidon+" m";
    }
}

class Bicicleta_oras : Bicicleta
{
    public int nr_viteze{get; protected set;}

    public Bicicleta_oras(string marca, double pret, int an, int nr_viteze) : base(marca, pret, an)
    {
        this.nr_viteze=nr_viteze;
    }

    public override void Salut()
    {
        Console.WriteLine("Salut, sunt Bicicleta_oras");
    }

    public override string ToString()
    {
        return base.ToString()+nr_viteze;
    }
}

class Student3
{
    public static void Echo41(ref short a, ref short b)
    {
        if(a>35 && b > 35)
        {
            short aux=a;
            a=b;
            b=aux;
        } else Console.WriteLine("Numbers are not greater than 35");
    }

    public static bool Cond(double x)
    {
        if(x%2==1)
            return true;
        else return false;
    }

    public static double Transform(double x)
    {
        return -1;
    }

    public static List<double> Oscar44(List<double> list, Func<double,bool> f1, Func<double,double> f2)
    {
        if(list.Count==0)
            throw new ArgumentNullException("List is empty");
        List<double> result=new List<double>();
        foreach(double elem in list)
        {
            if(!f1(elem))
                result.Add(f2(elem));
            else result.Add(elem);
        }
        return result;
    }

    public static void PrintList(List<double> list)
    {
        foreach(double elem in list)
        {
            Console.Write(elem+" ");
        }
        Console.WriteLine();
    }

    public static void Main(string[] args)
    {
        //ex1
        Console.WriteLine("EXERCITIUL 1:    ");
        short a=36;
        short b=56;
        Console.WriteLine(a+" "+b);
        Echo41(ref a, ref b);
        Console.WriteLine(a+" "+b);

        //ex2
        Console.WriteLine("\nEXERCITIUL 2:    ");
        List<double> list=new List<double>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.Add(5);
        list.Add(6);
        PrintList(list);
        List<double> result=Oscar44(list, Cond, Transform);
        PrintList(result);

        //ex3
        Console.WriteLine("\nEXERCITIUL 3:    ");
        List<Bicicleta> lista=new List<Bicicleta>();
        Bicicleta_munte b1 = new Bicicleta_munte("m1", 1234.5, 2020, 78.90);
        Bicicleta_munte b2 = new Bicicleta_munte("m2", 107, 2019, 98);
        Bicicleta_oras b3 = new Bicicleta_oras("m3", 4000, 2023, 3);
        Bicicleta_oras b4 = new Bicicleta_oras("m4", 8908, 2024, 5);

        lista.Add(b1);
        lista.Add(b2);
        lista.Add(b3);
        lista.Add(b4);

        foreach(Bicicleta bic in lista)
        {
            bic.Salut();
        }

        foreach(Bicicleta bic in lista)
        {
            Console.WriteLine(bic.ToString());
        }
    }
}

using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

class Ex1
{
    public static int makeSum()
    {
        int sum = 0;
        Console.Write("Enter n: ");
        int n = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the numbers: ");
        for (int i = 0; i < n; i++)
        {
            int x = Convert.ToInt32(Console.ReadLine());
            sum += x;
        }
        return sum;
    }
}

class Ex2
{
    public static void makeSolutions()
    {
        Console.Write("Enter a, b, c: ");
        int a = Convert.ToInt32(Console.ReadLine());
        int b = Convert.ToInt32(Console.ReadLine());
        int c = Convert.ToInt32(Console.ReadLine());
        double delta = b * b - 4 * a * c;
        if (delta < 0)
            Console.WriteLine("Solutions don't exist");
        else if (delta == 0)
            Console.WriteLine("There is only one solution");
        else
            Console.WriteLine("There are two solutions");
        double x1 = (-b + Math.Sqrt(delta)) / 2 * a;
        double x2 = (-b - Math.Sqrt(delta)) / 2 * a;
        if (x1 == x2)
        {
            Console.WriteLine("Only one solution: " + x1);
        }
        else
        {
            Console.WriteLine("The solutions are: " + x1 + " , " + x2);
        }
    }
}

class Student
{
    private readonly string name;
    private readonly int year;
    private readonly double grade1, grade2, grade3;

    public Student(string s, int y, double g1, double g2, double g3)
    {
        name = s;
        year = y;
        grade1 = g1;
        grade2 = g2;
        grade3 = g3;
    }

    public double getAverage()
    {
        double avg = 0;
        avg = (grade1 + grade2 + grade3) / 3;
        return avg;
    }

    public String toString()
    {
        return name + ", " + year + ", " + "grades: " + grade1 + " " + grade2 + " " + grade3;
    }
}

class Program
{
    static void Main(string[] args)
    {
        //ex1
        Console.WriteLine("The sum is: "+Ex1.makeSum());

        //ex2
        Ex2.makeSolutions();

        //ex3
        List<Student> array = new List<Student>();
        Console.Write("Enter no. of students: ");
        int n = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter student: ");
            Console.Write("name: ");
            string s = Console.ReadLine()!;
            Console.Write("year: ");
            int y = Convert.ToInt32(Console.ReadLine());
            Console.Write("grades: ");
            int g1 = Convert.ToInt32(Console.ReadLine());
            int g2 = Convert.ToInt32(Console.ReadLine());
            int g3 = Convert.ToInt32(Console.ReadLine());

            Student student = new Student(s, y, g1, g2, g3);
            array.Add(student);
        }

        double max_avg = 0;
        Student copy = array[0];
        foreach (Student s in array)
        {
            double avg = s.getAverage();
            if (avg > max_avg)
            {
                max_avg = avg;
                copy = s;
            }
        }
        Console.WriteLine(copy.toString());
    }
}
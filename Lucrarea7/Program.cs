using System;
using System.Collections.Generic;
public class InputReader
{
    public class KeyboardInputEventArgs : EventArgs
    {
        public string Input { get; }

        public KeyboardInputEventArgs(string input)
        {
            this.Input = input;
        }
    }

    public event EventHandler<KeyboardInputEventArgs>? OnKeyPressed;

    public void ReadKeys()
    {
        while (true)
        {
            string line = Console.ReadLine()!;
            if (string.IsNullOrEmpty(line))
                break;
            OnKeyPressed?.Invoke(this, new KeyboardInputEventArgs(line));
        }
    }
}

public class CharListener
{
    private char key {get;}
    public CharListener(char key, InputReader reader)
    {
        this.key = key;
        reader.OnKeyPressed += HandleKeyPressed;
    }

    private void HandleKeyPressed(object? sender, InputReader.KeyboardInputEventArgs e)
    {
        if(e.Input.Equals("print last") || e.Input.Equals("print all"))
            return;
        if(e.Input.Contains(key))
            Console.WriteLine(e.Input+" contine "+key);
        else
            Console.WriteLine(e.Input+" NU contine "+key);
    }
}

public class ComplexListener
{
    private int strNo {get;} 
    private List<string> list;
    public ComplexListener(int strNo, List<string> list, InputReader reader)
    {
        this.strNo = strNo;
        this.list = list;
        reader.OnKeyPressed += HandleKeyPressed;
    }

    private void HandleKeyPressed(object? sender, InputReader.KeyboardInputEventArgs e)
    {
        if(e.Input.Equals("print last"))
            if(list.Count == 0)
                Console.WriteLine("Lista e goala");
            else
                Console.WriteLine(list[list.Count-1]);
        else
        {
            if(e.Input.Equals("print all"))
            {
                if(list.Count == 0)
                    Console.WriteLine("Lista e goala");
                else
                {
                    foreach(string str in list)
                        Console.Write(str+"  ");
                    Console.WriteLine();
                }
                    
            }
            else
            {
                if(list.Count == strNo)
                    list.RemoveAt(0);
                list.Add(e.Input);
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        InputReader reader = new InputReader();

        int strNo = 3;
        List<string> list=new List<string>(strNo);

        CharListener ch1 = new CharListener('a', reader);
        CharListener ch2 = new CharListener('c', reader);

        ComplexListener complex = new ComplexListener(strNo, list, reader);

        Console.WriteLine("Comenzile disponibile: 'print last' si 'print all'");
        Console.WriteLine("Introduceti string-uri:");
        reader.ReadKeys();
    } 
}

using System;

public interface IRentable
{
    bool IsRented { get; set; }
    double MonthlyRent { get; set; }
}
abstract class Property
{
    private string Address { get; set; }
    private double IndoorArea { get; set; }
    private double PropertyValue { get; set; }

    public string getAddress()
    {
        return Address;
    }

    public double getIndoorArea()
    {
        return IndoorArea;
    }

    public double getPropertyValue()
    {
        return PropertyValue;
    }

    public Property(string address, double indoorArea, double propertyValue)
    {
        Address = address;
        IndoorArea = indoorArea;
        PropertyValue = propertyValue;
    }

    public override string ToString()
    {
        return "Address: " + Address + ", Indoor Area: " + IndoorArea + ",  Property Value:" + PropertyValue;
    }
}

class House : Property
{
    private double OutdoorArea { get; set; }
    private double TotalArea { get; set; }

    public House(string address, double indoorArea, double propertyValue, double OutdoorArea) : base(address, indoorArea, propertyValue)
    {
        this.OutdoorArea = OutdoorArea;
        this.TotalArea = indoorArea + this.OutdoorArea;
    }

    public override string ToString()
    {
        return base.ToString() + ", OutdoorArea: " + OutdoorArea + ", TotalArea: " + TotalArea;
    }

}

class Apartment : Property
{
    private int Floor { get; set; }
    private bool HasElevator { get; set; }

    public Apartment(string address, double indoorArea, double propertyValue, int floor, bool hasElevator) : base(address, indoorArea, propertyValue)
    {
        this.Floor = floor;
        this.HasElevator = hasElevator;
    }

    public override string ToString()
    {
        return base.ToString() + ", Floor: " + Floor + ", Has Elevator: " + HasElevator;
    }
}

class RentableApartment : Apartment, IRentable
{
    public bool IsRented { get; set; }
    public double MonthlyRent { get; set; }

    public RentableApartment(string address, double indoorArea, double propertyValue, int floor, bool hasElevator, bool IsRented, double MonthlyRent) : base(address, indoorArea, propertyValue, floor, hasElevator)
    {
        this.IsRented = IsRented;
        this.MonthlyRent = MonthlyRent;
    }

    public override string ToString()
    {
        return base.ToString() + ", Is Rented: " + IsRented + ", MonthlyRent: " + MonthlyRent;
    }
}

class RealEstateAgency
{
    private List<Property> properties = new List<Property>();

    public bool AddProperty(Property p)
    {
        foreach (Property prop in properties)
        {
            if (prop.getAddress().Equals(p.getAddress()))
            {
                Console.WriteLine("Property already exists!");
                return false;
            }
        }
        properties.Add(p);
        Console.WriteLine("Property added successfully!");
        return true;
    }
    
    public bool RentProperty(string address)
    {
        foreach (Property prop in properties)
        {
            if (prop.getAddress().Equals(address))
            {
                if (prop is IRentable rentable)
                {
                    if (rentable.IsRented == true)
                    {
                        Console.WriteLine("Property is already rented");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Property available for renting");
                        rentable.IsRented = true;
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("Property is not for renting sorry");
                    return false;
                }

            }
        }
        Console.WriteLine("Property with address given couldn't be found :(");
        return false;
     }
 }
class Program
{
    public static void Main(string[] args)
    {
        Apartment p1 = new Apartment("Bvd Oltenia nr 73", 79, 82000, 1, false);
        House p2 = new House("Bvd George Enescu nr 75", 98, 100000, 89);
        RentableApartment p3 = new RentableApartment("Bvd Nicolae Titulescu nr 43", 56, 67000, 3, false, false, 250);
        House p4 = new House("Bvd Oltenia nr 73", 89, 82900, 89);

        Console.WriteLine(p1.ToString());
        Console.WriteLine(p2.ToString());
        Console.WriteLine(p3.ToString());
        Console.WriteLine(p4.ToString());

        RealEstateAgency array = new RealEstateAgency();
        array.AddProperty(p1);
        array.AddProperty(p2);
        array.AddProperty(p3);
        array.AddProperty(p4);

        array.RentProperty("Bvd Oltenia nr 73"); //address p1
        array.RentProperty("Bvd George Enescu nr 75"); //address p2
        array.RentProperty("Bvd Nicolae Titulescu nr 43"); //address p3
        array.RentProperty("Bvd Nicolae Titulescu nr 43");
    }
}
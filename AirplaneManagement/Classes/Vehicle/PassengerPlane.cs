using System.Numerics;
using Raylib_cs;

public class PassengerPlane : Plane
{
    public static int levelPassangerMultplier = 6;
    static Texture2D[] sprites = new Texture2D[5] {
        Raylib.LoadTexture(@"Level1.png"),
        Raylib.LoadTexture(@"Level2.png"),
        Raylib.LoadTexture(@"Level3.png"),
        Raylib.LoadTexture(@"Level4.png"),
        Raylib.LoadTexture(@"Level5.png")
    };
    public List<Person> passangers;
    public PassengerPlane(Route route) : base(route)
    {
        passangers = new List<Person>();
        sprite = sprites[0];
    }

    public void Upgrade()
    {
        base.Upgrade();

        if (level >= sprites.Length)
        {
            sprite = sprites[sprites.Length - 1];
        }
        else
        {
            sprite = sprites[level - 1];
        }
    }

    public int Arrived()
    {
        base.Arrived();

        int income = (int)(route.dist/10f * passangers.Count);

        Airport landedAirport = route.airportBase;
        Airport destinationAirport = route.airportSecond;
        if (currentPoint > route.points.Length/2f)
        {
            landedAirport = route.airportSecond;
            destinationAirport = route.airportBase;
        }   

        Console.Clear();

        //Load all passangers from airport to plane
        List<Person> newPassangers = new List<Person>();
        Console.WriteLine(landedAirport.passengers.Count + " before");
        for (int i = landedAirport.passengers.Count - 1; i >= 0; i--)
        {
            //They dont have a route cause no path to their destination exists
            if (landedAirport.passengers[i].route == null)
            {
                continue;
            }

            if (landedAirport.passengers[i].route[0].id == destinationAirport.id)
            {
                newPassangers.Add(landedAirport.passengers[i]);
                landedAirport.passengers.RemoveAt(i);
            }
            if (newPassangers.Count == levelPassangerMultplier * level)
            {
                break;
            }
        }
        Console.WriteLine(newPassangers.Count + " new");
        Console.WriteLine(landedAirport.passengers.Count + " count");

        //For each passanger that has landed add them to the airport
        for (int i = 0; i < passangers.Count; i++)
        {
            landedAirport.passengers.Add(passangers[i]);
        }

        //load the new passangers to the plane
        passangers = newPassangers;

        
        landedAirport.HandleArrivingPassangers();

        return income;

        //Make a new list and fill it with persons that want to go to the given destinations
        //Add all passangers from plane to airport
        //call airport handle passangersFunction
    }
}
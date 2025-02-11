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

    //Makes all upgrades happend
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

    //Loads/unloads the passangers
    public int Arrived()
    {
        base.Arrived();

        int income = (int)(route.dist/10f * passangers.Count);

        //Figure out current airport
        Airport landedAirport = route.airportBase;
        Airport destinationAirport = route.airportSecond;
        if (currentPoint > route.points.Length/2f)
        {
            landedAirport = route.airportSecond;
            destinationAirport = route.airportBase;
        }   

        //Load all passangers on plane
        List<Person> newPassangers = new List<Person>();
        for (int i = landedAirport.passengers.Count - 1; i >= 0; i--)
        {
            //Passangers cant get to their destination
            if (landedAirport.passengers[i].route == null)
            {
                continue;
            }

            //Passangers next stop is the next airport
            if (landedAirport.passengers[i].route[0].id == destinationAirport.id)
            {
                newPassangers.Add(landedAirport.passengers[i]);
                landedAirport.passengers.RemoveAt(i);
            }
            
            //dont overload the plane
            if (newPassangers.Count == levelPassangerMultplier * level)
            {
                break;
            }
        }

        //add old passangers to airport
        for (int i = 0; i < passangers.Count; i++)
        {
            landedAirport.passengers.Add(passangers[i]);
        }

        //load the new passangers to the plane
        passangers = newPassangers;

        
        landedAirport.HandleArrivingPassangers();

        //Reason the passangers are first loaded and then added to the airport
        //I find it punching that an airport will be overcrowded for 1ms and therefore lose when dropping of passangers first.
        //Doing it in my order should keep the airports fullness +-0

        return income;
    }
}
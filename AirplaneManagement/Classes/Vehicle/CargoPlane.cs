using System.Dynamic;
using System.Numerics;
using Raylib_cs;

public class CargoPlane : Plane
{
    static int levelWeightMultplier = 20;
    static Texture2D[] sprites = new Texture2D[5] {
        Raylib.LoadTexture(@"Level1Cargo.png"),
        Raylib.LoadTexture(@"Level2Cargo.png"),
        Raylib.LoadTexture(@"Level3Cargo.png"),
        Raylib.LoadTexture(@"Level4Cargo.png"),
        Raylib.LoadTexture(@"Level5Cargo.png")
    };

    public int maxWeight;
    public List<Cargo> cargo;

    public CargoPlane(Route route) : base(route)
    {
        maxWeight = levelWeightMultplier * level;
        cargo = new List<Cargo>();
        sprite = sprites[0];
    }

    public int GetCargoWeight()
    {
        int weight = 0;
        for (int i = 0; i < cargo.Count; i++)
        {
            weight += cargo[i].weight;
        }
        return weight;
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
            sprite = sprites[level];
        }

        maxWeight = level * levelWeightMultplier;
    }

    public int Arrived()
    {
        base.Arrived();
        Console.WriteLine("ARRIVED");
        int income = (int)(route.dist/10f * cargo.Count) * 2;

        //figure out what airport plane is at
        Airport landedAirport = route.airportBase;
        Airport destinationAirport = route.airportSecond;
        if (currentPoint > route.points.Length / 2f)
        {
            landedAirport = route.airportSecond;
            destinationAirport = route.airportBase;
        }

        //Load all new cargo
        List<Cargo> newCargo = new List<Cargo>();
        int weight = 0;
        for (int i = landedAirport.cargo.Count - 1; i >= 0; i--)
        {
            //Cargo cant get to their destination
            if (landedAirport.cargo[i].route == null)
            {
                continue;
            }

            //Cargo next stop is the next airport
            if (landedAirport.cargo[i].route[0].id == destinationAirport.id)
            {
                if (weight + landedAirport.cargo[i].weight > maxWeight)
                {
                    break;
                }
                else
                {
                    weight += landedAirport.cargo[i].weight;
                    newCargo.Add(landedAirport.cargo[i]);
                    landedAirport.cargo.RemoveAt(i);
                }
            }
        }

        //Load airport
        for (int i = 0; i < cargo.Count; i++)
        {
            landedAirport.passengers.Add(cargo[i]);
        }

        landedAirport.HandleArrivingCargo();

        cargo = newCargo;

        return income;
    }
}
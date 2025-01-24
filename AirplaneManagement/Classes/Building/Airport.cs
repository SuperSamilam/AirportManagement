using System.Numerics;

public class Airport : Building
{
    public static int levelMultiplier = 6;
    public int level;

    //Passengers
    public int maxPassengers; 
    public List<Person> passengers;

    //Cargo
    public int maxCargo;
    public List<Cargo> cargo;

    public Airport (string name, Vector2 position) : base(name, position)
    {
        level = 1;
        maxPassengers = level * levelMultiplier;
        maxCargo = level * levelMultiplier;
        cargo = new List<Cargo>();
        passengers = new List<Person>();
    }
}
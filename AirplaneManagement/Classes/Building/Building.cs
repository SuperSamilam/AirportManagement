using System.Numerics;

public class Building : Gameobject
{
    public string name;
    public Vector2 position;

    public Building(string name, Vector2 position) : base()
    {
        this.name = name;
        this.position = position;
    }

}
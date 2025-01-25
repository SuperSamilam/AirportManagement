using System.Numerics;
using Raylib_cs;

public class Vehicle : Gameobject
{
    public static int levelSpeedMultplier = 1;

    public int level;
    public float speed;
    public Texture2D sprite;

    public Vehicle() : base()
    {
        this.speed = levelSpeedMultplier;
    }

    public void Upgrade()
    {
        level++;
        speed = level * levelSpeedMultplier;

        Console.WriteLine("UPDTAE");
    }
}


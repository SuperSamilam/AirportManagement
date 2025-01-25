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
            sprite = sprites[level-1];
        }

    }
}
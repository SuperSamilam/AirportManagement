using System.Numerics;
using Raylib_cs;

public class PassengerPlane : Plane
{
    static int levelPassangerMultplier = 6;
    static Texture2D[] sprites = new Texture2D[5] {
        Raylib.LoadTexture(@"Level1.png"),
        Raylib.LoadTexture(@"Level2.png"),
        Raylib.LoadTexture(@"Level3.png"),
        Raylib.LoadTexture(@"Level4.png"),
        Raylib.LoadTexture(@"Level5.png")
    };
    public Person[] passangers;
    public PassengerPlane(Route route) : base(route)
    {
        passangers = new Person[levelPassangerMultplier];
        sprite = sprites[0];
    }

    void Upgrade()
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

        Person[] newPassangers = new Person[level * levelPassangerMultplier];

        for (int i = 0; i < passangers.Length; i++)
        {
            newPassangers[i] = passangers[i];
        }
    }
}
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

    public CargoPlane(float speed, Texture2D sprite, Vector2 pos, Route route, int maxWeight) : base(route)
    {
        this.maxWeight = maxWeight;
        cargo = new List<Cargo>();
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

        maxWeight = level * levelWeightMultplier;
    }
}
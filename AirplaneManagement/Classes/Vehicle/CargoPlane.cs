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

    public void Arrive()
    {
        
    }
}
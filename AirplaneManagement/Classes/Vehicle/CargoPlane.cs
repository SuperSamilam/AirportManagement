using System.Numerics;
using Raylib_cs;

public class CargoPlane : Plane
{
    public int maxWeight;
    public List<Cargo> cargo;

    public CargoPlane(float speed, Texture2D sprite, Vector2 pos, Route route, int maxWeight) : base(speed, sprite, pos, route)
    {
        this.maxWeight = maxWeight;
        cargo = new List<Cargo>();
    }
}
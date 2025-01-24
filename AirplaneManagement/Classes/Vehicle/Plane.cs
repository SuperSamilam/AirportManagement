using System.Numerics;
using Raylib_cs;

public class Plane : Vehicle
{
    public Vector2 pos;
    public Route route;

    public Plane(float speed, Texture2D sprite, Vector2 pos, Route route) : base(speed, sprite)
    {
        this.pos = pos;
        this.route = route;
    }
}
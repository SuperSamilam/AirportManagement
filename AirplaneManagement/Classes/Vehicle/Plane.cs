using System.Numerics;
using Raylib_cs;

public class Plane : Vehicle
{
    public Vector2 pos;
    public Route route;
    public int currentPoint;

    public Plane(Route route) : base()
    {
        
        this.route = route;
        pos = route.points[0];
        currentPoint = 0;
    }
}
using System.Numerics;
using Raylib_cs;

public class Plane : Vehicle
{
    public Vector2 pos;
    public float rot;
    public Route route;
    public int currentPoint;
    public int dir;

    public float groundedTimeLeft;

    public Plane(Route route) : base()
    {
        this.route = route;
        pos = route.points[0];
        currentPoint = 0;
        dir = 1;
        groundedTimeLeft = 3;
    }

    public void Arrived()
    {
        groundedTimeLeft = 3f;
        dir = -dir;
    }
}
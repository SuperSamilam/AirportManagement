using System.Numerics;
using Raylib_cs;

public class PassengerPlane : Plane
{
    public Person[] passangers;

    public PassengerPlane(float speed, Texture2D sprite, Vector2 pos, Route route, int maxPassangers) : base(speed, sprite, pos, route)
    {
        passangers = new Person[maxPassangers];
    }
}
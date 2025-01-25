using System.Numerics;

public class Route : Gameobject
{
    public Airport airportBase;
    public Airport airportSecond;

    public List<Plane> planes;

    public Vector2[] points;

    public Route(Airport baseAirport, Airport secondAirport, Vector2[] points, string id) : base()
    {
        this.id = id;
        this.airportBase = baseAirport;
        this.airportSecond = secondAirport;
        this.points = points;
        planes = new List<Plane>();

        PassengerPlane plane = new PassengerPlane(this); 
        planes.Add(plane);
    }
}
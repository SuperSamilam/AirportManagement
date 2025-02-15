using System.Numerics;

public class Route : Gameobject
{
    public Airport airportBase;
    public Airport airportSecond;

    public List<Plane> planes;

    public float dist;

    public Vector2[] points;

    public Route(Airport baseAirport, Airport secondAirport, Vector2[] points, int dist, string id) : base()
    {
        this.id = id;
        this.airportBase = baseAirport;
        this.airportSecond = secondAirport;
        this.points = points;
        planes = new List<Plane>();
        this.dist = dist;

        AddNewPassangerPlane();
    }

    //Create new passanger plane for this route
    public void AddNewPassangerPlane()
    {
        PassengerPlane plane = new PassengerPlane(this);
        planes.Add(plane);
    }

    //Create new cargo plane for this route
    public void AddNewCargoPlane()
    {
        CargoPlane plane = new CargoPlane(this);
        planes.Add(plane);
    }
}
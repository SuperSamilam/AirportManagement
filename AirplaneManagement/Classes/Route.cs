public class Route : Gameobject
{
    public Airport airportBase;
    public Airport airportSecond;

    public List<Plane> planes;

    public Route(Airport baseAirport, Airport secondAirport, Plane plane) : base()
    {
        this.airportBase = baseAirport;
        this.airportSecond = secondAirport;
        planes = new List<Plane>();
        planes.Add(plane);
    }
}
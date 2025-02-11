using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public class Airport : Building
{
    static List<Airport> airports = new List<Airport>()
{
  new Airport("Stockholm", new Vector2(7,77)),//stockholm
  new Airport("Göteborg", new Vector2(-160,150)),//göteborg
  new Airport("Malmö", new Vector2(-120,230)),//malmö
  new Airport("Linköping", new Vector2(-40,120)), //linköping
  new Airport("Örebro", new Vector2(-90,77)), //örebro
  new Airport("Sundsvall", new Vector2(-30,-20)), //sundsvall
  new Airport("Luleå", new Vector2(80,-160)), //Luleå
  new Airport("Jönköping", new Vector2(-100,140)), //Jönköping
  new Airport("Kuruna", new Vector2(0,-230)), //Kiruna
  new Airport("Åre", new Vector2(-160,-60)), //Åre

  new Airport("Oslo", new Vector2(-220,60)), //Oslo
  new Airport("Stavanger", new Vector2(-350,100)), //Stavanger
  new Airport("Bergen", new Vector2(-360,20)), //Bergen
  new Airport("Ålesund", new Vector2(-290,-40)), //Ålesund
  new Airport("Trondheim", new Vector2(-210,-90)), //Trondheim
  new Airport("Alta", new Vector2(100,-290)), //Alta

  new Airport("Helsinki", new Vector2(190,50)), //Helsinki
  new Airport("Turku", new Vector2(140,40)), //Turku
  new Airport("Tampere", new Vector2(175,0)), //Tampere
  new Airport("Oulu", new Vector2(200,-130)), //Oulu
  new Airport("Rovaniemi", new Vector2(205,-190)), //Rovaniemi
  new Airport("Karigasnemi", new Vector2(190,-280)), //Karigasnemi
  new Airport("Joensuu", new Vector2(340, -30)), //Joensuu

  new Airport("Tallinn", new Vector2(220, 100)), //Tallinn

  new Airport("Riga", new Vector2(185, 195)), //Riga

  new Airport("Klaipeda", new Vector2(140, 240)), //Klaipeda
  new Airport("Vilnius", new Vector2(250, 280)), //Vilnius

  new Airport("Gdansk", new Vector2(30, 290)), //Gdansk

  new Airport("Hamburg",new Vector2(-240, 290)), //Hamburg

  new Airport("Copenhagen", new Vector2(-150, 250)), //Copenhagen
  new Airport("Flyn", new Vector2(-250, 250)), //Flyn
  new Airport("Aalborg", new Vector2(-225, 180)), //Aalborg
};

    public static int levelMultiplier = 6;
    public int level;

    public List<Route> routes;

    //Passengers
    public int maxPassengers = 6;
    public List<Person> passengers;

    //Cargo
    public int maxCargo = 6;
    public List<Cargo> cargo;

    public Airport(string name, Vector2 position) : base(name, position)
    {
        level = 1;
        cargo = new List<Cargo>();
        passengers = new List<Person>();
        routes = new List<Route>();
    }

    public bool PressedAirport()
    {
        return Raylib.CheckCollisionPointRec(GlobalData.Instance.CalculatedValue, new Rectangle(position.X - 12, position.Y - 12, 24, 24));
    }

    public void Upgrade()
    {
        level++;
        maxPassengers = level * levelMultiplier;
        maxCargo = level * levelMultiplier;
    }

    public void HandleArrivingPassangers()
    {
        for (int i = passengers.Count - 1; i >= 0; i--)
        {
            if (passengers[i].destination == this)
            {
                passengers.RemoveAt(i);
                continue;
            }

            passengers[i].RemoveRoutePoint(this);


        }
    }

    public void UpdatePassangerRoutes(List<Airport> airports)
    {
        for (int i = 0; i < passengers.Count; i++)
        {
            if (passengers[i].route == null)
            {
                passengers[i].CalculateRoute(airports, this);
            }
        }
    }

    public static void UpdateAllPassangerRoutes(List<Airport> airports)
    {
        for (int i = 0; i < airports.Count; i++)
        {
            airports[i].UpdatePassangerRoutes(airports);
        }
    }

    public static Airport? GetNewAirport(List<Airport> ownedAirport)
    {
        //Find the 3 closest airports that are not owned by the player
        List<Airport> closestAirports = new List<Airport>();

        for (int i = 0; i < 3; i++)
        {
            Airport closestAirport = null;
            float closestDistance = float.MaxValue;
            foreach (Airport airport in airports)
            {
                if (!ownedAirport.Contains(airport) && !closestAirports.Contains(airport))
                {
                    float distance = Vector2.Distance(ownedAirport[ownedAirport.Count - 1].position, airport.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestAirport = airport;
                    }
                }
            }
            closestAirports.Add(closestAirport);
        }

        if (closestAirports.Count == 0)
        {
            return null;
        }

        return closestAirports[new Random().Next(0, closestAirports.Count)];
    }

    public static Airport GetAirport(int n)
    {
        return airports[n];
    }


}
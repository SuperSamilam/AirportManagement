public class Person
{
    public Airport destination;
    public List<Airport> route;


    public Person(Airport destination)
    {
        this.destination = destination;

    }

    public void RemoveRoutePoint(Airport airport)
    {
        //path to destination dont exist
        if (route == null)
            return;

        if (route[0] == airport)
        {
            route.RemoveAt(0);
            route.TrimExcess();
        }
    }

    public void CalculateRoute(List<Airport> airports, Airport currentAirport)
    {
        Queue<Airport> queue = new Queue<Airport>();
        Dictionary<Airport, Airport?> visited = new Dictionary<Airport, Airport?>();

        visited.Add(currentAirport, null);
        queue.Enqueue(currentAirport);

        while (queue.Count != 0)
        {
            Airport a = queue.Dequeue();
            if (a == destination)
            {
                ReconstructPath(visited, currentAirport);
                break;
            }

            if (a.routes == null)
            {
                continue;
            }

            for (int i = 0; i < a.routes.Count; i++)
            {
                Airport toCheck = a.routes[i].airportBase;
                if (a.routes[i].airportBase.id == a.id)
                {
                    toCheck = a.routes[i].airportSecond;
                }

                if (!visited.ContainsKey(toCheck))
                {
                    queue.Enqueue(toCheck);
                    visited.Add(toCheck, a);
                }
            }
        }
        // Console.WriteLine("No Paths exists");
    }

    void ReconstructPath(Dictionary<Airport, Airport?> tree, Airport currentAirport)
    {
        route = new List<Airport>();
        route.Add(destination);

        while (true)
        {
            Airport next = tree[route[0]];

            if (next == currentAirport)
            {
                break;
            }
            else
            {
                route.Insert(0, next);
            }
        }
    }

}
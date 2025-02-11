using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public class RouteBuilder : Executor
{
    bool drawing = false;
    Airport startAirport;

    public void Update(Gamedata gamedata)
    {
        //Check input and if selection
        if (Raylib.IsMouseButtonPressed(MouseButton.Right))
        {
            for (int i = 0; i < gamedata.airports.Count; i++)
            {
                if (gamedata.airports[i].PressedAirport())
                {
                    drawing = true;
                    startAirport = gamedata.airports[i];
                    break;
                }
            }
        }

        //Draw preview if its possible
        if (Raylib.IsMouseButtonDown(MouseButton.Right) && drawing)
        {
            Vector2[] points = GenerateRoutePreview(WorldMouse.Instance.Position, 25);
            float dist = Vector2.Distance(startAirport.position, WorldMouse.Instance.Position); //Price

            Color drawColor = Color.Green;
            if (gamedata.money < dist)
                drawColor = Color.Red;

            Raylib.DrawText(dist.ToString("0"), (int)WorldMouse.Instance.Position.X - 10, (int)WorldMouse.Instance.Position.Y + 15, 30, drawColor);

            for (int i = 0; i < points.Length - 1; i++)
            {
                Raylib.DrawLineEx(points[i], points[i + 1], 3, drawColor);
            }
        }

        //Try to make new route
        if (Raylib.IsMouseButtonReleased(MouseButton.Right) && drawing)
        {
            for (int i = 0; i < gamedata.airports.Count; i++)
            {   
                if (gamedata.airports[i].PressedAirport() && gamedata.airports[i].id != startAirport.id)
                {
                    //Making sure this route dosent exists
                    string routeId = startAirport.id + "-" + gamedata.airports[i].id;
                    for (int j = 0; j < gamedata.routes.Count; j++)
                    {
                        if (gamedata.routes[j].id == routeId)
                        {
                            drawing = false;
                            return;
                        }
                    }

                    
                    int price = (int)Vector2.Distance(startAirport.position, gamedata.airports[i].position);
                    if (gamedata.money < price)
                    {
                        drawing = false;
                        return;
                    }

                    //Make the new route
                    gamedata.money -= price;
                    Route route = new Route(startAirport, gamedata.airports[i], GenerateRoutePreview(gamedata.airports[i].position, 25), price, routeId);
                    
                    gamedata.routes.Add(route);
                    startAirport.routes.Add(route);
                    gamedata.airports[i].routes.Add(route);
                    drawing = false;

                    Airport.UpdateAllPassangerRoutes(gamedata.airports);

                    return;
                }
            }
            drawing = false;
        }
    }

    //Generates points for Route
    public Vector2[] GenerateRoutePreview(Vector2 endpos, int steps = 25)
    {
        //Generates a controll point
        Vector2 middlePos = (startAirport.position + endpos) / 2;

        Vector2 dir = Vector2.Normalize(endpos - startAirport.position);
        Vector2 birnormal = new Vector2(-dir.Y, dir.X); 

        //Makes sure so curvature is always point upwards
        if (startAirport.position.X < endpos.X)
        {
            birnormal = -birnormal;
        }

        Vector2 controllPoint = middlePos + birnormal * Vector2.Distance(startAirport.position, endpos) / 3;

    
        Vector2[] points = new Vector2[(int)steps + 1];
        points[0] = startAirport.position;
        points[points.Length - 1] = endpos;

        //Generates the curve
        for (int i = 1; i < steps; i++)
        {
            float t = i / (float)steps;

            Vector2 a = startAirport.position + (controllPoint - startAirport.position) * t;
            Vector2 b = controllPoint + (endpos - controllPoint) * t;
            Vector2 p = a + (b - a) * t;

            points[i] = p;
        }

        return points;
    }


    //dont need
    public void LateUpdate(Gamedata gamedata)
    {

    }

}
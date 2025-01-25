using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public class RouteBuilder : Executor
{
    bool drawing = false;
    Airport startAirport;
    Vector2[] points;

    public RouteBuilder()
    {
        Register.executorRegistry.Add(this);
    }
    public void New()
    {

    }

    public void Start()
    {

    }

    public void Update(Gamedata gamedata)
    {
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
        if (Raylib.IsMouseButtonDown(MouseButton.Right) && drawing)
        {
            Vector2 mousePos = Raylib.GetMousePosition() - new Vector2(387, 340);
            Vector2 middlePos = (startAirport.position + mousePos) / 2;

            Vector2 dir = Vector2.Normalize(mousePos - startAirport.position);
            Vector2 birnormal = new Vector2(-dir.Y, dir.X);

            if (startAirport.position.X < mousePos.X)
            {
                birnormal = -birnormal;
            }

            Vector2 p3 = middlePos + birnormal * Vector2.Distance(startAirport.position, mousePos) / 3;

            //Debug purposes
            // Raylib.DrawCircleV(startAirport.position, 5, Color.Blue);
            // Raylib.DrawCircleV(mousePos, 5, Color.Blue);
            // Raylib.DrawCircleV(p3, 5, Color.Blue);

            float steps = 15;
            points = new Vector2[(int)steps+1]; //points on curve is steps - 1, so we add 1 for the start and end to get 2 extra points
            points[0] = startAirport.position;
            points[points.Length-1] = mousePos;
            
            for (int i = 1; i < steps; i++)
            {
                float t = i / steps;
                Vector2 p = (1 - t) * (1 - t) * startAirport.position + 2 * (1 - t) * t * p3 + t * t * mousePos;
                points[i] = p;    
                ///Raylib.DrawCircleV(p, 2, Color.Black); //Debug purposes
            }

            for (int i = 0; i < points.Length-1; i++)
            {
                Raylib.DrawLineEx(points[i], points[i+1], 3, Color.Black);
            }
        }
        if (Raylib.IsMouseButtonReleased(MouseButton.Right) && drawing)
        {
            for (int i = 0; i < gamedata.airports.Count; i++)
            {
                if (gamedata.airports[i].PressedAirport() && gamedata.airports[i].id != startAirport.id)
                {
                    string routeId = startAirport.id + "-" + gamedata.airports[i].id;

                    for (int j = 0; j < gamedata.routes.Count; j++)
                    {
                        if (gamedata.routes[j].id == routeId)
                        {
                            drawing = false;
                            return;
                        }
                    }   

                    Route route = new Route(startAirport, gamedata.airports[i], points, routeId);
                    gamedata.routes.Add(route);
                    drawing = false;
                    return;
                }
            }
            drawing = false;
        }
    }

    public void LateUpdate(Gamedata gamedata)
    {
        
    }

}
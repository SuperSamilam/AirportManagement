using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public class RouteBuilder : Executor
{
    bool drawing = false;
    Airport startAirport;

    public RouteBuilder()
    {
        Register.executorRegistry.Add(this);
    }


    public void Update(Gamedata gamedata)
    {
        //Check first click and record data
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

        //If still holding down draw temporary position
        if (Raylib.IsMouseButtonDown(MouseButton.Right) && drawing)
        {
            Vector2[] points = GenerateRoutePreview(GlobalData.Instance.CalculatedValue, 25);

            //Calculate price
            float dist = Vector2.Distance(startAirport.position, GlobalData.Instance.CalculatedValue);

            Color drawColor = Color.Green;
            if (gamedata.money < dist)
                drawColor = Color.Red;

            Raylib.DrawText(dist.ToString("0"), (int)GlobalData.Instance.CalculatedValue.X - 10, (int)GlobalData.Instance.CalculatedValue.Y + 15, 30, drawColor);

            for (int i = 0; i < points.Length - 1; i++)
            {
                Raylib.DrawLineEx(points[i], points[i + 1], 3, drawColor);
            }
        }

        //Done drawing
        if (Raylib.IsMouseButtonReleased(MouseButton.Right) && drawing)
        {
            for (int i = 0; i < gamedata.airports.Count; i++)
            {
                //make sure i make an uniqe route
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

                    int price = (int)Vector2.Distance(startAirport.position, gamedata.airports[i].position);
                    if (gamedata.money < price)
                    {
                        drawing = false;
                        return;
                    }

                    //route is uniqe create it
                    Route route = new Route(startAirport, gamedata.airports[i], GenerateRoutePreview(gamedata.airports[i].position, 25), routeId);
                    gamedata.routes.Add(route);
                    drawing = false;
                    return;
                }
            }
            drawing = false;
        }
    }

    public Vector2[] GenerateRoutePreview(Vector2 endpos, int steps = 25)
    {
        //Generates controllpoint
        Vector2 middlePos = (startAirport.position + endpos) / 2;

        Vector2 dir = Vector2.Normalize(endpos - startAirport.position);
        Vector2 birnormal = new Vector2(-dir.Y, dir.X); //Binormal is the vector perpendicular to the normal

        //makes sure route curvature is never upside down
        if (startAirport.position.X < endpos.X)
        {
            birnormal = -birnormal;
        }

        Vector2 controllPoint = middlePos + birnormal * Vector2.Distance(startAirport.position, endpos) / 3;

        //The extra on is to make sure a start and end position can be added       
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

            Console.WriteLine(p);
            points[i] = p;
        }

        return points;
    }



    public void LateUpdate(Gamedata gamedata)
    {

    }

}
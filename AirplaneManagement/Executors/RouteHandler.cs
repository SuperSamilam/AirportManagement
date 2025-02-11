using Raylib_cs;

public class RouteHandler : Executor
{

    Route route;
    bool isRouteSelected = false;


    public void LateUpdate(Gamedata gamedata)
    {   
        //Draw UI
        if (isRouteSelected)
        {
            Raylib.DrawRectangle(10, 471, 250, 200, Color.SkyBlue);
            Raylib.DrawText(route.airportBase.name + " - " + route.airportSecond.name, 20, 490, 20, Color.Black);

            Raylib.DrawRectangle(20, 520, 230, 40, Color.Green);
            Raylib.DrawText("Buy new Cargo Plane(250)", 30, 532, 15, Color.Black);

            Raylib.DrawRectangle(20, 570, 230, 40, Color.Green);
            Raylib.DrawText("Buy new Passanger Plane(250)", 30, 582, 15, Color.Black);

            //New planes got a set price of 250, chould be a variable
            if (gamedata.money < 250)
            {
                return;
            }

            //Cargo plane
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(20, 520, 230, 40)) && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                gamedata.money -= 250;
                route.AddNewCargoPlane();

            }

            //Passanger plane
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(20, 570, 230, 40)) && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                gamedata.money -= 250;
                route.AddNewPassangerPlane();
            }

        }
    }

    public void Update(Gamedata gamedata)
    {
        //Draw a blue line over showing it is selected
        if (isRouteSelected)
        {
            for (int j = 0; j < route.points.Length - 1; j++)
            {
                Raylib.DrawLineEx(route.points[j], route.points[j + 1], 4, Color.Blue);
            }
        }

        //Makes it possible to selected a route
        for (int i = 0; i < gamedata.routes.Count; i++)
        {
            Route route = gamedata.routes[i];

            if (Raylib.IsMouseButtonPressed(MouseButton.Left) && CollisionDetection.GetCollsionOnRoute(WorldMouse.Instance.Position, gamedata.routes[i]))
            {
                this.route = route;
                isRouteSelected = true;
                break;
            }
            else if (Raylib.IsMouseButtonPressed(MouseButton.Left) && !Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(10, 471, 250, 200)))
            {
                isRouteSelected = false;
            }
        }
    }
}
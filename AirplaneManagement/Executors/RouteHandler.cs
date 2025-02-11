using Raylib_cs;

public class RouteHandler : Executor
{

    Route selectedRoute;
    bool routeSelected = false;
    public RouteHandler()
    {
        Register.executorRegistry.Add(this);
    }

    public void LateUpdate(Gamedata gamedata)
    {
        if (routeSelected)
        {
            //Draw Route UI
            // AIRPORT - AIRPORT
            // Buy New Passanger Plane(250)
            //Buy new Cargo Plane(250)

            Raylib.DrawRectangle(10, 471, 250, 200, Color.SkyBlue);
            Raylib.DrawText(selectedRoute.airportBase.name + " - " + selectedRoute.airportSecond.name, 20, 490, 20, Color.Black);

            Raylib.DrawRectangle(20, 520, 230, 40, Color.Green);
            Raylib.DrawText("Buy new Cargo Plane(250)", 30, 532, 15, Color.Black);

            Raylib.DrawRectangle(20, 570, 230, 40, Color.Green);
            Raylib.DrawText("Buy new Passanger Plane(250)", 30, 582, 15, Color.Black);



            if (gamedata.money < 250)
            {
                return;
            }

            //Cargo plane
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(20, 520, 230, 40)) && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                gamedata.money -= 250;
                selectedRoute.AddNewCargoPlane();

            }

            //Passanger plane
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(20, 570, 230, 40)) && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                gamedata.money -= 250;
                selectedRoute.AddNewPassangerPlane();
            }


            // if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(564, 580, 150, 50)) && Raylib.IsMouseButtonPressed(MouseButton.Left))
            // {
            //     gamedata.money -= upgradeCost;
            //     if (passengerPlane != null)
            //     {
            //         passengerPlane.Upgrade();
            //     }
            //     if (cargoPlane != null)
            //     {
            //         cargoPlane.Upgrade();
            //     }
            // }

        }
    }

    public void Update(Gamedata gamedata)
    {
        if (routeSelected)
        {
            for (int j = 0; j < selectedRoute.points.Length - 1; j++)
            {
                Raylib.DrawLineEx(selectedRoute.points[j], selectedRoute.points[j + 1], 4, Color.Blue);
            }
        }



        for (int i = 0; i < gamedata.routes.Count; i++)
        {
            Route route = gamedata.routes[i];

            if (Raylib.IsMouseButtonPressed(MouseButton.Left) && CollisionDetection.GetCollsionOnRoute(GlobalData.Instance.CalculatedValue, gamedata.routes[i]))
            {
                selectedRoute = route;
                routeSelected = true;
            }
        }
    }
}
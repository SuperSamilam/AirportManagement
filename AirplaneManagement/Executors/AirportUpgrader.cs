using Raylib_cs;

public class AirportUpgrader : Executor
{
    Airport airport;
    bool isAirportSelected = false;
    
    public void Update(Gamedata gamedata)
    {
        int upgradeCost = 0;
        if (isAirportSelected)
        {
            upgradeCost = (int)(airport.maxPassengers * airport.maxPassengers / 2f);
        }


        if (Raylib.IsMouseButtonPressed(MouseButton.Left) && isAirportSelected)
        {
            //if click isent on box remove UI
            if (!Raylib.CheckCollisionPointRec(WorldMouse.Instance.Position, new Rectangle(airport.position.X - 75, (int)airport.position.Y - 110, 150, 100)))
            {
                isAirportSelected = false;
            }

            //Upgrade
            if (Raylib.CheckCollisionPointRec(WorldMouse.Instance.Position, new Rectangle((int)airport.position.X - 50, (int)airport.position.Y - 45, 100, 30)))
            {
                if (gamedata.money < upgradeCost)
                {
                    return;
                }

                gamedata.money -= upgradeCost;
                airport.Upgrade();
            }
        }

        //Select Airport
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            for (int i = 0; i < gamedata.airports.Count; i++)
            {
                if (gamedata.airports[i].PressedAirport())
                {
                    isAirportSelected = true;
                    airport = gamedata.airports[i];
                    break;
                }
            }
        }
         
        //Drawing UI, UI Is drawn here because the position of the UI is relevant to the airport position
        if (isAirportSelected)
        {
            Raylib.DrawCircle((int)airport.position.X, (int)airport.position.Y, 9, Color.Green);
            Raylib.DrawRectangle((int)airport.position.X - 75, (int)airport.position.Y - 110, 150, 100, Color.SkyBlue);
            int width = Raylib.MeasureText(airport.name, 20);
            Raylib.DrawText(airport.name, (int)airport.position.X - width / 2, (int)airport.position.Y - 110 + 10, 20, Color.Black);

            width = Raylib.MeasureText(airport.passengers.Count + "/" + airport.maxPassengers + " passengers", 15);
            Raylib.DrawText(airport.passengers.Count + "/" + airport.maxPassengers + " passengers", (int)airport.position.X - width / 2, (int)airport.position.Y - 80, 15, Color.Black);

            width = Raylib.MeasureText(airport.cargo.Count + "/" + airport.maxCargo + " cargo", 15);
            Raylib.DrawText(airport.cargo.Count + "/" + airport.maxCargo + " cargo", (int)airport.position.X - width / 2, (int)airport.position.Y - 60, 15, Color.Black);

            Raylib.DrawRectangle((int)airport.position.X - 50, (int)airport.position.Y - 45, 100, 30, Color.Green);
            Raylib.DrawText("Upgrade " + upgradeCost, (int)airport.position.X - 50 + 20, (int)airport.position.Y - 45 + 8, 15, Color.Black);
        }

    }

    //Dont need
    public void LateUpdate(Gamedata gamedata)
    {
    }
}
using Raylib_cs;

public class AirportUpgrader : Executor
{
    Airport selectedAirport;
    bool airportSelected = false;
    public AirportUpgrader()
    {
        Register.executorRegistry.Add(this);
    }

    public void Update(Gamedata gamedata)
    {
        if (Raylib.IsMouseButtonPressed(MouseButton.Left) && airportSelected)
        {
            if (!Raylib.CheckCollisionPointRec(GlobalData.Instance.CalculatedValue, new Rectangle(selectedAirport.position.X - 75, (int)selectedAirport.position.Y - 110, 150, 100)))
            {
                airportSelected = false;
            }
            if (Raylib.CheckCollisionPointRec(GlobalData.Instance.CalculatedValue, new Rectangle((int)selectedAirport.position.X - 50, (int)selectedAirport.position.Y - 45, 100, 30)))
            {
                selectedAirport.Upgrade();
            }
        }
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            for (int i = 0; i < gamedata.airports.Count; i++)
            {
                if (gamedata.airports[i].PressedAirport())
                {
                    airportSelected = true;
                    selectedAirport = gamedata.airports[i];
                    break;
                }
            }
        }
        //Drawing UI
        if (airportSelected)
        {
            Raylib.DrawCircle((int)selectedAirport.position.X, (int)selectedAirport.position.Y, 9, Color.Green);
            Raylib.DrawRectangle((int)selectedAirport.position.X - 75, (int)selectedAirport.position.Y - 110, 150, 100, Color.SkyBlue);
            int width = Raylib.MeasureText(selectedAirport.name, 20);
            Raylib.DrawText(selectedAirport.name, (int)selectedAirport.position.X - width / 2, (int)selectedAirport.position.Y - 110 + 10, 20, Color.Black);

            width = Raylib.MeasureText(selectedAirport.passengers.Count + "/" + selectedAirport.maxPassengers + " passengers", 15);
            Raylib.DrawText(selectedAirport.passengers.Count + "/" + selectedAirport.maxPassengers + " passengers", (int)selectedAirport.position.X - width / 2, (int)selectedAirport.position.Y - 80, 15, Color.Black);

            width = Raylib.MeasureText(selectedAirport.cargo.Count + "/" + selectedAirport.maxCargo + " cargo", 15);
            Raylib.DrawText(selectedAirport.cargo.Count + "/" + selectedAirport.maxCargo + " cargo", (int)selectedAirport.position.X - width / 2, (int)selectedAirport.position.Y - 60, 15, Color.Black);

            Raylib.DrawRectangle((int)selectedAirport.position.X - 50, (int)selectedAirport.position.Y - 45, 100, 30, Color.Green);
            Raylib.DrawText("Upgrade", (int)selectedAirport.position.X - 50 + 20, (int)selectedAirport.position.Y - 45 + 8, 15, Color.Black);
        }

    }

    public void LateUpdate(Gamedata gamedata)
    {
        // if (airportSelected)
        // {
        //     Raylib.DrawRectangle((int)selectedAirport.position.X + 387 - 75, (int)selectedAirport.position.Y + 340 - 110, 150, 100, Color.SkyBlue);

        // }
    }
}
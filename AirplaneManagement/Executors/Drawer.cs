using Raylib_cs;

//This class is self explanitory
public class Drawer : Executor
{
    Texture2D worldTexture = Raylib.LoadTexture(@"Scnadi.png");
    Texture2D water = Raylib.LoadTexture(@"WaterRect.png");
    float screenWidth = Raylib.GetScreenWidth();
    float screenHeight = Raylib.GetScreenHeight();

    public void LateUpdate(Gamedata gamedata)
    {
    }

    public void Update(Gamedata gamedata)
    {
        DrawWorld(gamedata);
        DrawAirports(gamedata);
        DrawRoutes(gamedata);

        if (!gamedata.alive)
        {
            Raylib.DrawText("YOU LOST", -200, -35, 80, Color.Black);
        }
    }

    void DrawAirports(Gamedata data)
    {
        for (int i = 0; i < data.airports.Count; i++)
        {
            Color airportColor = Raylib.ColorLerp(Color.Green, Color.Red, data.airports[i].passengers.Count / (float)data.airports[i].maxPassengers);
            Raylib.DrawCircle((int)data.airports[i].position.X, (int)data.airports[i].position.Y, 12, Color.Black);
            Raylib.DrawCircle((int)data.airports[i].position.X, (int)data.airports[i].position.Y, 9, airportColor);

            if (data.unlockedCargo)
            {
                Raylib.DrawCircleSector(data.airports[i].position, 9, -90, 90, 30, Raylib.ColorLerp(Color.DarkGreen, new Color(149, 6, 6), data.airports[i].cargo.Count / (float)data.airports[i].maxCargo));
            }

            int width = Raylib.MeasureText(data.airports[i].name, 10);
            Raylib.DrawText(data.airports[i].name, (int)data.airports[i].position.X - width / 2, (int)data.airports[i].position.Y - 25, 10, airportColor);
        }
    }

    void DrawRoutes(Gamedata data)
    {
        for (int i = 0; i < data.routes.Count; i++)
        {
            for (int j = 0; j < data.routes[i].points.Length - 1; j++)
            {
                Raylib.DrawLineEx(data.routes[i].points[j], data.routes[i].points[j + 1], 2, Color.Black);
            }
        }
    }

    void DrawWorld(Gamedata data)
    {
        Raylib.DrawTexture(water, -1000, -1000, Color.White);
        Raylib.DrawTexture(worldTexture, (int)(-screenWidth / 2f), (int)(-screenHeight / 2f), Color.White);
    }

}
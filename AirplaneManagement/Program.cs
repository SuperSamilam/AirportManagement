using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

Gamedata data = new Gamedata();

Raylib.InitWindow(774, 681, "Game IDK");
Raylib.SetTargetFPS(60);

Texture2D worldTexture = Raylib.LoadTexture(@"Scnadi.png");
Texture2D water = Raylib.LoadTexture(@"WaterRect.png");




Camera2D camera = new Camera2D();
camera.Zoom = 1f;
float screenWidth = Raylib.GetScreenWidth();
float screenHeight = Raylib.GetScreenHeight();
camera.Offset = new Vector2(screenWidth / 2, screenHeight / 2);
camera.Target = new Vector2(screenWidth / 2, screenHeight / 2);
camera.Rotation = 0;

Movement movement = new Movement();
RouteBuilder routeBuilder = new RouteBuilder();
Timer timer = new Timer(1, TimerType.AddAirport);
AirportUpgrader airportUpgrader = new AirportUpgrader();
AirplaneHandler airplaneHandler = new AirplaneHandler();
AirplaneUpgrader airplaneUpgrader = new AirplaneUpgrader();

data.airports.Add(Airport.GetAirport(0));
data.airports.Add(Airport.GetAirport(1));


while (!Raylib.WindowShouldClose())
{

  Raylib.BeginDrawing();


  Raylib.BeginMode2D(camera);
  GlobalData.Instance.UpdateValue(camera);
  Raylib.ClearBackground(Color.Black);

  DrawWorld();
  DrawRoutes();
  DrawAirports();

  camera.Target = movement.position;
  camera.Zoom = movement.zoom;

  Register.UpdateExecutors(data);

  DrawAirportDebugBox();
  Raylib.EndMode2D();
  Register.LateUpdateExecutors(data);

  Raylib.EndDrawing();


}

void DrawWorld()
{
  Raylib.DrawTexture(water, -1000, -1000, Color.White);
  Raylib.DrawTexture(worldTexture, (int)(-screenWidth / 2f), (int)(-screenHeight / 2f), Color.White);
}

void DrawRoutes()
{
  for (int i = 0; i < data.routes.Count; i++)
  {
    for (int j = 0; j < data.routes[i].points.Length - 1; j++)
    {
      Raylib.DrawLineEx(data.routes[i].points[j], data.routes[i].points[j + 1], 2, Color.Black);
    }
  }
}

void DrawAirports()
{
  for (int i = 0; i < data.airports.Count; i++)
  {
    Raylib.DrawCircle((int)data.airports[i].position.X, (int)data.airports[i].position.Y, 12, Color.Black);
    Raylib.DrawCircle((int)data.airports[i].position.X, (int)data.airports[i].position.Y, 9, Color.Red);

    int width = Raylib.MeasureText(data.airports[i].name, 10);
    Raylib.DrawText(data.airports[i].name, (int)data.airports[i].position.X - width / 2, (int)data.airports[i].position.Y - 25, 10, Color.Red);
  }
}

void DrawAirportDebugBox()
{
  for (int i = 0; i < 1; i++)
  {
    data.airports[i].PressedAirport();
  }
}
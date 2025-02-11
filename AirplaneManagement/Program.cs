using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

Gamedata data = new Gamedata();
float screenWidth = 774, screenHeight = 681;

Raylib.InitWindow((int)screenWidth, (int)screenHeight, "Game IDK");
Raylib.SetTargetFPS(60);


//Camera
Camera2D camera = new Camera2D();
camera.Zoom = 1f;
camera.Offset = new Vector2(screenWidth / 2, screenHeight / 2);
camera.Target = new Vector2(screenWidth / 2, screenHeight / 2);
camera.Rotation = 0;

//Executors
Drawer drawer = new Drawer();
Movement movement = new Movement();
RouteBuilder routeBuilder = new RouteBuilder();
Timer addAirportTimer = new Timer(2000, TimerType.AddAirport);
Timer addPassengerTimer = new Timer(5, TimerType.AddPassenger);

AirportUpgrader airportUpgrader = new AirportUpgrader();
AirplaneHandler airplaneHandler = new AirplaneHandler();
AirplaneUpgrader airplaneUpgrader = new AirplaneUpgrader();
RouteHandler routeHandler = new RouteHandler();

//Debug 
data.airports.Add(Airport.GetAirport(0));
data.airports.Add(Airport.GetAirport(1));

Register register = new();

while (!Raylib.WindowShouldClose())
{

  Raylib.BeginDrawing();


  Raylib.BeginMode2D(camera);

  //This is gets the mouse position based on the camera position
  GlobalData.Instance.UpdateValue(camera);
  Raylib.ClearBackground(Color.Black);


  register.UpdateExecutors(data);

  camera.Target = movement.position;
  camera.Zoom = movement.zoom;
  Raylib.EndMode2D();

  Register.LateUpdateExecutors(data);

  //Dont draw money here?
  Raylib.DrawText("Money: " + data.money, 10, 35, 20, Color.Black);

  Raylib.EndDrawing();

  if (data.airports.Count >= 8)
  {
    data.unlockedCargo = true;
  }

}







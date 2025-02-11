using System.Diagnostics;
using System.Numerics;
using System.Security.AccessControl;
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
Timer addAirportTimer = new Timer(10, TimerType.AddAirport);
Timer addPassengerTimer = new Timer(1, TimerType.AddPassenger);

AirportUpgrader airportUpgrader = new AirportUpgrader();
AirplaneMovement airplaneMovement = new AirplaneMovement();
AirplaneUpgrader airplaneUpgrader = new AirplaneUpgrader();
RouteHandler routeHandler = new RouteHandler();

WinController winController = new WinController();

//Debug 
data.airports.Add(Airport.GetAirport(0));
data.airports.Add(Airport.GetAirport(1));

Register register = new();
register.executorRegistry.Add(drawer);
register.executorRegistry.Add(movement);
register.executorRegistry.Add(routeBuilder);
register.executorRegistry.Add(addAirportTimer);
register.executorRegistry.Add(addPassengerTimer);
register.executorRegistry.Add(airportUpgrader);
register.executorRegistry.Add(airplaneMovement);
register.executorRegistry.Add(airplaneUpgrader);
register.executorRegistry.Add(routeHandler);
register.executorRegistry.Add(winController);

while (!Raylib.WindowShouldClose())
{

  Raylib.BeginDrawing();


  Raylib.BeginMode2D(camera);

  if (data.alive)
  {
    //This is gets the mouse position based on the camera position
    WorldMouse.Instance.UpdateValue(camera);
    Raylib.ClearBackground(Color.Black);


    register.UpdateExecutors(data);
    camera.Target = movement.position;
    camera.Zoom = movement.zoom;
    Raylib.EndMode2D();

    register.LateUpdateExecutors(data);

    //Dont draw money here?
    Raylib.DrawText("Money: " + data.money, 10, 35, 20, Color.Black);
  }
  else
  {
    camera.Zoom = 1f;
    camera.Target = new Vector2(0, 0);
    camera.Rotation = 0;
    drawer.Update(data);
  }

  Raylib.EndDrawing();

  if (data.airports.Count >= 8)
  {
    data.unlockedCargo = true;
  }

}







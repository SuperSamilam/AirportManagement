using System.Diagnostics;
using System.Numerics;
using System.Security.AccessControl;
using Raylib_cs;

Gamedata data = new Gamedata();
float screenWidth = 774, screenHeight = 681;

Raylib.InitWindow((int)screenWidth, (int)screenHeight, "Game IDK");
Raylib.SetTargetFPS(60);

//Makes sure all executors exists
#region Executors

//Executors
Drawer drawer = new Drawer();
Movement movement = new Movement();
RouteBuilder routeBuilder = new RouteBuilder();
Timer addAirportTimer = new Timer(40, TimerType.AddAirport);
Timer addPassengerTimer = new Timer(10, TimerType.AddPassenger);

AirportUpgrader airportUpgrader = new AirportUpgrader();
AirplaneMovement airplaneMovement = new AirplaneMovement();
RouteHandler routeHandler = new RouteHandler();
AirplaneUpgrader airplaneUpgrader = new AirplaneUpgrader();
WinController winController = new WinController();

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
#endregion

data.airports.Add(Airport.GetAirport(0));
data.airports.Add(Airport.GetAirport(1));

while (!Raylib.WindowShouldClose())
{

  Raylib.BeginDrawing();

  Raylib.BeginMode2D(movement.camera);
  
  //if havent lost game call all fucntions the make game work
  if (data.alive)
  {
    Raylib.ClearBackground(Color.Black);
    
    WorldMouse.Instance.UpdateValue(movement.camera);
    register.UpdateExecutors(data);

    Raylib.EndMode2D();

    register.LateUpdateExecutors(data);
  }
  else //Lost the game reset position and draw losescreen
  {
    movement.ResetCamera();
    drawer.Update(data);
  }

  Raylib.EndDrawing();

  //unlock cargo
  if (data.airports.Count >= 20)
  {
    data.unlockedCargo = true;
  }

}







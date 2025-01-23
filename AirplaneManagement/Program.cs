using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

Raylib.InitWindow(774, 681, "Game IDK");
Raylib.SetTargetFPS(60);

Texture2D worldTexture = Raylib.LoadTexture(@"Scnadi.png");
Texture2D water = Raylib.LoadTexture(@"WaterRect.png");




Camera2D camera = new Camera2D();
camera.Zoom = 1f;
float screenWidth = Raylib.GetScreenWidth();
float screenHeight = Raylib.GetScreenHeight();
camera.Offset = new Vector2(screenWidth / 2, screenHeight/2);
camera.Target = new Vector2(screenWidth / 2, screenHeight/2);
camera.Rotation = 0;

Movement movement = new Movement();

List<Airport> airports = new List<Airport>()
{
  new Airport(new Vector2(7,77)),//stockholm
  new Airport(new Vector2(-160,150)),//göteborg
  new Airport(new Vector2(-120,230)),//malmö
  new Airport(new Vector2(-10,60)),//uppsala
  new Airport(new Vector2(-40,120)), //linköping
  new Airport(new Vector2(-90,77)), //örebro
  new Airport(new Vector2(-30,-20)), //sundsvall
  new Airport(new Vector2(80,-160)), //Luleå
  new Airport(new Vector2(-100,140)), //Jönköping
  new Airport(new Vector2(0,-230)), //Kiruna
  new Airport(new Vector2(-160,-60)), //Åre

  new Airport(new Vector2(-220,60)), //Oslo
  new Airport(new Vector2(-350,100)), //Stavanger
  new Airport(new Vector2(-360,20)), //Bergen
  new Airport(new Vector2(-290,-40)), //Ålesund
  new Airport(new Vector2(-210,-90)), //Trondheim
  new Airport(new Vector2(100,-290)), //Alta

  new Airport(new Vector2(190,50)), //Helsinki
  new Airport(new Vector2(140,40)), //Turku
  new Airport(new Vector2(175,0)), //Tampere
  new Airport(new Vector2(200,-130)), //Oulu
  new Airport(new Vector2(205,-190)), //Rovaniemi
  new Airport(new Vector2(190,-280)), //Karigasnemi
  new Airport(new Vector2(340, -30)), //Joensuu

  new Airport(new Vector2(220, 100)), //Tallinn

  new Airport(new Vector2(185, 195)), //Riga

  new Airport(new Vector2(140, 240)), //Klaipeda
  new Airport(new Vector2(250, 280)), //Vilnius

  new Airport(new Vector2(30, 290)), //Gdansk

  new Airport(new Vector2(-240, 290)), //Hamburg

  new Airport(new Vector2(-150, 250)), //Copenhagen
  new Airport(new Vector2(-250, 250)), //Flyn
  new Airport(new Vector2(-225, 180)), //Aalborg
};


while (!Raylib.WindowShouldClose())
{
  
  Raylib.BeginDrawing();
  
  Register.UpdateExecutors();

  Raylib.BeginMode2D(camera);

  Raylib.ClearBackground(Color.Black);

  DrawWorld();

  camera.Target = movement.position;
  camera.Zoom = movement.zoom;

  for (int i = 0; i < airports.Count; i++)
  {
    Raylib.DrawCircle((int)airports[i].position.X, (int)airports[i].position.Y, 10, Color.Red);
  }


  Raylib.EndMode2D();
  

  Raylib.EndDrawing();
}

void DrawWorld()
{
  Raylib.DrawTexture(water, -1000, -1000, Color.White);
  Raylib.DrawTexture(worldTexture, (int)(-screenWidth/2f), (int)(-screenHeight/2f), Color.White);
}



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
  new Airport("Stockholm", new Vector2(7,77)),//stockholm
  new Airport("Göteborg", new Vector2(-160,150)),//göteborg
  new Airport("Malmö", new Vector2(-120,230)),//malmö
  new Airport("Uppsala", new Vector2(-10,60)),//uppsala
  new Airport("Linköping", new Vector2(-40,120)), //linköping
  new Airport("Örebro", new Vector2(-90,77)), //örebro
  new Airport("Sundsvall", new Vector2(-30,-20)), //sundsvall
  new Airport("Luleå", new Vector2(80,-160)), //Luleå
  new Airport("Jönköping", new Vector2(-100,140)), //Jönköping
  new Airport("Kuruna", new Vector2(0,-230)), //Kiruna
  new Airport("Åre", new Vector2(-160,-60)), //Åre

  new Airport("Oslo", new Vector2(-220,60)), //Oslo
  new Airport("Stavanger", new Vector2(-350,100)), //Stavanger
  new Airport("Bergen", new Vector2(-360,20)), //Bergen
  new Airport("Ålesund", new Vector2(-290,-40)), //Ålesund
  new Airport("Trondheim", new Vector2(-210,-90)), //Trondheim
  new Airport("Alta", new Vector2(100,-290)), //Alta

  new Airport("Helsinki", new Vector2(190,50)), //Helsinki
  new Airport("Turku", new Vector2(140,40)), //Turku
  new Airport("Tampere", new Vector2(175,0)), //Tampere
  new Airport("Oulu", new Vector2(200,-130)), //Oulu
  new Airport("Rovaniemi", new Vector2(205,-190)), //Rovaniemi
  new Airport("Karigasnemi", new Vector2(190,-280)), //Karigasnemi
  new Airport("Joensuu", new Vector2(340, -30)), //Joensuu

  new Airport("Tallinn", new Vector2(220, 100)), //Tallinn

  new Airport("Riga", new Vector2(185, 195)), //Riga

  new Airport("Klaipeda", new Vector2(140, 240)), //Klaipeda
  new Airport("Vilnius", new Vector2(250, 280)), //Vilnius

  new Airport("Gdansk", new Vector2(30, 290)), //Gdansk

  new Airport("Hamburg",new Vector2(-240, 290)), //Hamburg

  new Airport("Copenhagen", new Vector2(-150, 250)), //Copenhagen
  new Airport("Flyn", new Vector2(-250, 250)), //Flyn
  new Airport("Aalborg", new Vector2(-225, 180)), //Aalborg
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



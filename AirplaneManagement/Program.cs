using System.Numerics;
using Raylib_cs;

Raylib.InitWindow(1200, 675, "Game IDK");
Raylib.SetTargetFPS(60);

Texture2D worldTexture = Raylib.LoadTexture(@"world.png");
Texture2D waterNormal = Raylib.LoadTexture(@"WaterNormal.jpg");
Vector2 scrollPosition = new Vector2();
// Console.WriteLine(@"world.png");

List<Vector2> mapOffsets = new List<Vector2>()
{
  new Vector2(2000,0),new Vector2(-2000,0),
  new Vector2(0,-857),new Vector2(0,857),
  new Vector2(2000, 857), new Vector2(-2000, 857),
  new Vector2(2000,-857), new Vector2(-2000,-857),
  new Vector2(0,0)
  
};

Movement movement = new Movement();

while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();

  Register.UpdateExecutors();

  Raylib.ClearBackground(Color.White);
  DrawBackground(movement.chunkPosition);

  DrawBackground(new Vector2(2000,0) + movement.chunkPosition);
  DrawBackground(new Vector2(-2000,0) + movement.chunkPosition);
  DrawBackground(new Vector2(0,857) + movement.chunkPosition);
  DrawBackground(new Vector2(0,-857) + movement.chunkPosition);

  DrawBackground(new Vector2(2000, 857) + movement.chunkPosition);
  DrawBackground(new Vector2(-2000,857) + movement.chunkPosition);
  DrawBackground(new Vector2(2000,-857) + movement.chunkPosition);
  DrawBackground(new Vector2(-2000,-857) + movement.chunkPosition);


  Raylib.EndDrawing();
}

void DrawBackground(Vector2 offset)
{

  Raylib.ClearBackground(Color.White);

  Rectangle source = new Rectangle(scrollPosition.X, scrollPosition.Y, 2000, 1157);
  Vector2 position = new Vector2(0, -100);

  Raylib.DrawTextureRec(waterNormal, source, position + movement.position + offset, Color.White);
  Raylib.DrawTexture(worldTexture, (int)movement.position.X + (int)offset.X, (int)movement.position.Y + (int)offset.Y, Color.White);
  scrollPosition += new Vector2(0.1f, 0.1f); 
}
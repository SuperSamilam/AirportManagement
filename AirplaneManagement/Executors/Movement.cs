using System.Numerics;
using Raylib_cs;

public class Movement : Executor
{
    public Vector2 position;
    float speed = 10;

    public float zoom = 1;
    float maxZoom = 5f;
    float minZoom = 1f;

    public Movement()
    {
        Register.executorRegistry.Add(this);
    }


    public void Update(Gamedata gamedata)
    {
        float x = position.X;
        float y = position.Y;
        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            y -= speed * zoom/maxZoom;
        }
        if (Raylib.IsKeyDown(KeyboardKey.S))
        {
            y += speed * zoom/maxZoom;
        }
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            x -= speed * zoom/maxZoom;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            x += speed * zoom/maxZoom;
        }

        zoom = Math.Clamp(Raylib.GetMouseWheelMove() * 0.1f + zoom, minZoom, maxZoom);
        
        float minX = Raylib.GetScreenWidth() / 2 / zoom - 388;
        float minY = Raylib.GetScreenHeight() / 2 / zoom - 344;
        float maxX = 774 - Raylib.GetScreenWidth() / 2 / zoom - 388;
        float maxY = 681 - Raylib.GetScreenHeight() / 2 / zoom - 344;

        
        position = new Vector2(
            Math.Clamp(x, minX, maxX), 
            Math.Clamp(y, minY, maxY)
        );

    }

    public void LateUpdate(Gamedata gamedata)
    {
        
    }
}
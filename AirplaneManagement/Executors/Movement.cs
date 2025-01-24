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

    public void New()
    {
    }

    public void Start()
    {
    }

    //Boundery 
    // screenwidth/2
    // screenheight/2
    // -screenwidth/2
    // -screenheight/2

    public void Update()
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

        
        position = new Vector2(
            Math.Clamp(x, -Raylib.GetScreenWidth()/2f, Raylib.GetScreenWidth()/2f), 
            Math.Clamp(y, -Raylib.GetScreenWidth()/2f, Raylib.GetScreenWidth()/2f)
            );
        zoom = Math.Clamp(Raylib.GetMouseWheelMove() * 0.1f + zoom, minZoom, maxZoom);
    }
}
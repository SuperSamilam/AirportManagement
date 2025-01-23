using System.Numerics;
using Raylib_cs;

public class Movement : Executor
{
    public Vector2 position;
    float speed = 10;

    public Vector2 chunkPosition;
    int chunkWidth = 2000;
    int chunkHeight = 857;

    public Movement()
    {
        Register.executorRegistry.Add(this);
    }

    public void New()
    {
        throw new NotImplementedException();
    }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        float x = position.X;
        float y = position.Y;
        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            y += speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.S))
        {
            y -= speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            x += speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            x -= speed;
        }

        
        position = new Vector2(x, y);
        //Calculate closest chunk position
        chunkPosition = new Vector2(
            -MathF.Round(position.X / chunkWidth) * chunkWidth, 
            -MathF.Round(position.Y / chunkHeight) * chunkHeight
        );
    
    }
}
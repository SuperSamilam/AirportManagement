using System.Numerics;
using Raylib_cs;

public class Movement : Executor
{
    float speed = 10;

    float maxZoom = 5f;
    float minZoom = 1f;

    //Camera
    public Camera2D camera = new Camera2D();

    public Movement()
    {
        camera.Zoom = 1f;
        camera.Offset = new Vector2(774 / 2, 681 / 2);
        camera.Target = new Vector2(774 / 2, 681 / 2);
        camera.Rotation = 0;
    }


    public void Update(Gamedata gamedata)
    {
        float x = camera.Target.X;
        float y = camera.Target.Y;
        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            y -= speed * camera.Zoom / maxZoom;
        }
        if (Raylib.IsKeyDown(KeyboardKey.S))
        {
            y += speed * camera.Zoom / maxZoom;
        }
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            x -= speed * camera.Zoom / maxZoom;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            x += speed * camera.Zoom / maxZoom;
        }

        camera.Zoom = Math.Clamp(Raylib.GetMouseWheelMove() * 0.1f + camera.Zoom, minZoom, maxZoom);

        //Makes sure that the position is a valid position given the zoom, camera should not be able to see outside the edge
        float minX = Raylib.GetScreenWidth() / 2 / camera.Zoom - 388;
        float minY = Raylib.GetScreenHeight() / 2 / camera.Zoom - 344;
        float maxX = 774 - Raylib.GetScreenWidth() / 2 / camera.Zoom - 388;
        float maxY = 681 - Raylib.GetScreenHeight() / 2 / camera.Zoom - 344;

        camera.Target = new Vector2(
            Math.Clamp(x, minX, maxX),
            Math.Clamp(y, minY, maxY)
        );
    }

    public void ResetCamera()
    {
        camera.Zoom = 1f;
        camera.Target = new Vector2(0, 0);
        camera.Rotation = 0;
    }

    //Dosent need a late update
    public void LateUpdate(Gamedata gamedata)
    {

    }
}
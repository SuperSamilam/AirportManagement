using System.Numerics;
using Raylib_cs;

public class WorldMouse
{
    static WorldMouse instance;

    WorldMouse() { }

    Vector2 position;
    public Vector2 Position => position;

    public static WorldMouse Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new WorldMouse();
            }
            return instance;
        }
    }

    public void UpdateValue(Camera2D camera)
    {
        position = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), camera);
    }
}

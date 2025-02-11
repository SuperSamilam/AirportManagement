using System.Numerics;
using Raylib_cs;

public static class CollisionDetection
{
    //Checks if point is instead rotated rect
    public static bool CheckCollisionOnRotatedRect(Vector2 point, Vector2 pos, float width, float height, float rotation)
    {
        Vector2[] corners = GetRotatedRectangleCorners(pos, width, height, rotation);

        //If point is collidiong wuith either triangel
        return Raylib.CheckCollisionPointTriangle(point, corners[0], corners[1], corners[2]) ||
           Raylib.CheckCollisionPointTriangle(point, corners[2], corners[3], corners[0]);
    }

    //Calculates corners based on 1 pos
    static Vector2[] GetRotatedRectangleCorners(Vector2 pos, float width, float height, float rotation)
    {
        Vector2[] corners = new Vector2[4];

        float angleRad = rotation * MathF.PI / 180f;


        corners[0] = new Vector2(pos.X, pos.Y);
        corners[1] = new Vector2(pos.X + height * MathF.Cos(angleRad), pos.Y + width * MathF.Sin(angleRad));

        float angleRadBack = (rotation + 90) * MathF.PI / 180f;
        corners[2] = new Vector2(pos.X + height * MathF.Cos(angleRadBack), pos.Y + width * MathF.Sin(angleRadBack));
        corners[3] = new Vector2(corners[2].X + height * MathF.Cos(angleRad), corners[2].Y + width * MathF.Sin(angleRad));

        return corners;
    }

    //Checks multple triangels around a route path
    public static bool GetCollsionOnRoute(Vector2 point, Route route)
    {
        for (int i = 0; i < route.points.Length - 1; i++)
        {
            Vector2[] corners = new Vector2[4];
            Vector2 dir = Vector2.Normalize(route.points[i] - route.points[i + 1]);

            corners[0] = new Vector2(-dir.Y, dir.X) + route.points[i];
            corners[1] = new Vector2(dir.Y, -dir.X) + route.points[i];
            corners[2] = new Vector2(dir.Y, -dir.X) + route.points[i + 1];
            corners[3] = new Vector2(-dir.Y, dir.X) + route.points[i + 1];

            if (Raylib.CheckCollisionPointTriangle(point, corners[0], corners[1], corners[2]))
            {
                return true;
            }
            else if (Raylib.CheckCollisionPointTriangle(point, corners[3], corners[2], corners[0]))
            {
                return true;
            }
        }
        return false;
    }
}
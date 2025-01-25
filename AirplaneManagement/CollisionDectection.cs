using System.Numerics;
using Raylib_cs;

public static class CollisionDetection
{   
    //Checks if point is instead rotated rect
    public static bool CheckCollisionOnRotatedRect(Vector2 point, Vector2 pos, float width, float height, float rotation)
    {
        Vector2[] corners = GetRotatedRectangleCorners(pos, width, height, rotation);

        return Raylib.CheckCollisionPointTriangle(point, corners[0], corners[1], corners[2]) ||
           Raylib.CheckCollisionPointTriangle(point, corners[2], corners[3], corners[0]);
    }

    //calculates the positon of the corners of the rectangle
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
}
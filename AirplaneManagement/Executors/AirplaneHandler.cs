using System.Numerics;
using Raylib_cs;

public class AirplaneHandler : Executor
{
    public AirplaneHandler()
    {
        Register.executorRegistry.Add(this);
    }

    public void Update(Gamedata gamedata)
    {
        float delta = Raylib.GetFrameTime();
        for (int i = 0; i < gamedata.routes.Count; i++)
        {
            for (int j = 0; j < gamedata.routes[i].planes.Count; j++)
            {
                Plane plane = gamedata.routes[i].planes[j];
                Vector2 offset = new Vector2(128 * 0.1f, 128 * 0.1f);

                Vector2 refPoint = Vector2.Zero;
                float multplier = -12.8f;
                if (plane.currentPoint == gamedata.routes[i].points.Length - 1)
                {
                    refPoint = gamedata.routes[i].points[plane.currentPoint - 1];
                    multplier = -multplier;
                }
                else
                {
                    refPoint = gamedata.routes[i].points[plane.currentPoint + 1];
                }
                Vector2 goalPos = Vector2.Normalize(refPoint - gamedata.routes[i].points[plane.currentPoint]);
                goalPos = new Vector2(-goalPos.Y, goalPos.X) * multplier + gamedata.routes[i].points[plane.currentPoint];




                Vector2 dir = Vector2.Normalize(goalPos - plane.pos);
                plane.pos += dir * plane.speed * delta * 10;
                float rotation = MathF.Atan2(dir.Y, dir.X) * 180f / MathF.PI + 90;
                // plane.pos = calculatedPos;

                if (Vector2.Distance(plane.pos, goalPos) < 0.2f)
                {
                    plane.currentPoint++;
                    Console.WriteLine(plane.currentPoint + " " + gamedata.routes[i].points.Length + " " + goalPos);
                    if (plane.currentPoint >= gamedata.routes[i].points.Length - 1)
                    {
                        plane.currentPoint = 0;
                        plane.pos = gamedata.routes[i].points[0];
                    }
                };

                Raylib.DrawCircle((int)plane.pos.X, (int)plane.pos.Y, 5, Color.Yellow);
                Raylib.DrawCircle((int)goalPos.X, (int)goalPos.Y, 5, Color.Blue);
                Raylib.DrawTextureEx(plane.sprite, plane.pos, rotation, 0.1f, Color.White);


                //Going back in right direction
            }
        }
    }

    public void LateUpdate(Gamedata gamedata)
    {

    }
}
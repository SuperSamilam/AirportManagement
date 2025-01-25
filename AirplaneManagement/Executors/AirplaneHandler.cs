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

                Vector2 dir = Vector2.Normalize(gamedata.routes[i].points[plane.currentPoint+1] - plane.pos);
                plane.pos += dir * plane.speed * delta * 10;

                if (Vector2.Distance(plane.pos, gamedata.routes[i].points[plane.currentPoint+1]) < 0.2f)
                {
                    plane.currentPoint++;
                    if (plane.currentPoint >= gamedata.routes[i].points.Length - 1)
                    {
                        plane.currentPoint = 0;
                        plane.pos = gamedata.routes[i].points[0];
                    }
                };

                float dot = Vector2.Dot(plane.pos, gamedata.routes[i].points[plane.currentPoint+1]);

                float mag = MathF.Sqrt(plane.pos.X * plane.pos.X + plane.pos.Y * plane.pos.Y);
                float mag2 = MathF.Sqrt(gamedata.routes[i].points[plane.currentPoint+1].X * gamedata.routes[i].points[plane.currentPoint+1].X + gamedata.routes[i].points[plane.currentPoint+1].Y * gamedata.routes[i].points[plane.currentPoint+1].Y);

                float progress = dot / (mag * mag2);
                progress = MathF.Max(-1f, Math.Min(1f, progress));
                float angle = MathF.Acos(progress);
                angle = angle * 180f / MathF.PI;
                Console.WriteLine(angle);

                Raylib.DrawTextureEx(plane.sprite, plane.pos - new Vector2(128*0.1f, 128*0.1f), angle - 90, 0.1f, Color.White);
            }
        }
    }

    public void LateUpdate(Gamedata gamedata)
    {
        
    }
}
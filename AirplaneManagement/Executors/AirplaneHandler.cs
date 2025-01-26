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

                //Makes grouded planes not move or show
                if (plane.groundedTimeLeft > 0)
                {
                    HandleGroundedPlane(plane, gamedata.routes[i], delta);
                    continue;
                }

                //gets a working offset point based on direction because the route is an array
                Vector2 offsetPoint = GetGoalPoint(plane, gamedata.routes[i]);

                //Move the plane
                Vector2 dir = Vector2.Normalize(offsetPoint - plane.pos);
                plane.pos += dir * plane.speed * delta * 30;
                plane.rot = MathF.Atan2(dir.Y, dir.X) * 180f / MathF.PI + 90;

                //Changed goal point if near offset point, change direction if end of route
                Vector2 nextPos = plane.pos + dir * plane.speed * delta * 30;//predicting next pos
                if (Vector2.Dot(nextPos - offsetPoint, plane.pos - offsetPoint) < 0)
                {
                    plane.currentPoint += plane.dir;
                    if (plane.currentPoint >= gamedata.routes[i].points.Length || plane.currentPoint <= -1)
                    {
                        if (plane is PassengerPlane passengerPlane)
                            passengerPlane.Arrived();
                        if (plane is CargoPlane cargoPlane)
                            cargoPlane.Arrived(); //Not implimeted yet
                    }
                }


                //Distance check
                // if (Vector2.Distance(plane.pos, offsetPoint) < 0.3f)
                // {
                //     plane.currentPoint += plane.dir;
                //     if (plane.currentPoint >= gamedata.routes[i].points.Length)
                //     {
                //         plane.groundedTimeLeft = 3f;
                //         plane.dir = -plane.dir;
                //     }
                //     else if (plane.currentPoint <= -1)
                //     {
                //         plane.groundedTimeLeft = 3f;
                //         plane.dir = -plane.dir;
                //     }
                // }

                Raylib.DrawTextureEx(plane.sprite, plane.pos, plane.rot, 0.1f, Color.White);

            }
        }
    }

    void HandleGroundedPlane(Plane plane, Route route, float delta)
    {
        plane.groundedTimeLeft -= delta;
        if (plane.groundedTimeLeft <= 0)
        {
            //Set position to start position
            if (plane.dir == 1)
            {
                plane.pos = GetOffsetPoint(route.points[0], route.points[1], -12.8f);
                plane.currentPoint += 2; //This line is problimatic, the first time a plane spawns it will aim for the second second point, but it does not matter cause the result it the same
            }
            else
            {
                plane.pos = GetOffsetPoint(route.points[route.points.Length - 1], route.points[route.points.Length - 2], -12.8f);
                plane.currentPoint -= 2;
            }
        }
    }

    Vector2 GetGoalPoint(Plane plane, Route route)
    {
        Vector2 samplePoint = Vector2.Zero;
        float multplier = -12.8f;
        //Based on direction get a correct sample to find binormal
        if (plane.dir == 1)
        {
            if (plane.currentPoint == route.points.Length - 1)
            {
                samplePoint = route.points[plane.currentPoint - plane.dir];
                multplier = -multplier;
            }
            else
            {
                samplePoint = route.points[plane.currentPoint + plane.dir];
            }
        }
        else
        {
            if (plane.currentPoint == 0)
            {
                samplePoint = route.points[plane.currentPoint - plane.dir];
                multplier = -multplier;
            }
            else
            {
                samplePoint = route.points[plane.currentPoint + plane.dir];
            }
        }
        return GetOffsetPoint(route.points[plane.currentPoint], samplePoint, multplier);
    }

    Vector2 GetOffsetPoint(Vector2 point, Vector2 refPoint, float multplier)
    {
        Vector2 goalPos = Vector2.Normalize(refPoint - point);
        goalPos = new Vector2(-goalPos.Y, goalPos.X) * multplier + point;
        return goalPos;
    }

    public void LateUpdate(Gamedata gamedata)
    {

    }
}
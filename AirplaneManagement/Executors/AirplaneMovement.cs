using System.Numerics;
using Raylib_cs;

//Makes sure airplane moves
public class AirplaneMovement : Executor
{

    public void Update(Gamedata gamedata)
    {
        float delta = Raylib.GetFrameTime();
        for (int i = 0; i < gamedata.routes.Count; i++)
        {

            for (int j = 0; j < gamedata.routes[i].planes.Count; j++)
            {

                Plane plane = gamedata.routes[i].planes[j];

                //Makes grounded planes not show up or move
                if (plane.groundedTimeLeft > 0)
                {
                    HandleGroundedPlane(plane, gamedata.routes[i], delta);
                    continue;
                }

                //Explenation of offsetpoint in method
                Vector2 offsetPoint = GetGoalPoint(plane, gamedata.routes[i]);

                //Moving
                Vector2 dir = Vector2.Normalize(offsetPoint - plane.pos);
                plane.pos += dir * plane.speed * delta * 10;
                plane.rot = MathF.Atan2(dir.Y, dir.X) * 180f / MathF.PI + 90;

                //Handle when plane comes close enought to its goal
                Vector2 nextPos = plane.pos + dir * plane.speed * delta * 30; //Predicting the next position
                if (Vector2.Dot(nextPos - offsetPoint, plane.pos - offsetPoint) < 0)
                {
                    plane.currentPoint += plane.dir;
                    //Plane arrived at airport
                    if (plane.currentPoint >= gamedata.routes[i].points.Length || plane.currentPoint <= -1)
                    {
                        if (plane is PassengerPlane passengerPlane)
                        {
                            gamedata.money += passengerPlane.Arrived();   

                        }
                        if (plane is CargoPlane cargoPlane)
                            cargoPlane.Arrived(); //Not implimeted yet
                    }
                }

                
                Raylib.DrawTextureEx(plane.sprite, plane.pos, plane.rot, 0.1f, Color.White);

            }

        }
    }

    //When plane arrives do all stuff it needs to do
    void HandleGroundedPlane(Plane plane, Route route, float delta)
    {
        plane.groundedTimeLeft -= delta; //Make sure plane is groundex x seconds
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

    //Gets the point the plane wants to travel too.
    Vector2 GetGoalPoint(Plane plane, Route route)
    {
        //Because the orgin of an image is topleft i need to offset the goalpoint

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

    //Dont need it
    public void LateUpdate(Gamedata gamedata)
    {

    }
}
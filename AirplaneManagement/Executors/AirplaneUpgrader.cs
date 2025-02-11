using System.Numerics;
using Raylib_cs;

public class AirplaneUpgrader : Executor
{
    Plane plane;
    bool isPlaneSelected = false;

    public void Update(Gamedata gamedata)
    {
        //Try to select a plane
        for (int i = 0; i < gamedata.routes.Count; i++)
        {
            for (int j = 0; j < gamedata.routes[i].planes.Count; j++)
            {
                Plane plane = gamedata.routes[i].planes[j];

                if (Raylib.IsMouseButtonPressed(MouseButton.Left) && CollisionDetection.CheckCollisionOnRotatedRect(WorldMouse.Instance.Position, plane.pos, 25, 25, plane.rot))
                {
                    this.plane = plane;
                    isPlaneSelected = true;
                }
                // else if (Raylib.IsMouseButtonPressed(MouseButton.Left) && !Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(514, 471, 250,200))) //If plane is selected but press anywhere else deslect
                // {
                //     isPlaneSelected = false;
                // }
                //this code is removed cause it made it hard to select a plane
            }
        }


    }
    public void LateUpdate(Gamedata gamedata)
    {
        //Draws UI
        if (isPlaneSelected)
        {
            Raylib.DrawRectangle(514, 471, 250, 200, Color.SkyBlue);

            Raylib.DrawText("Speed: " + plane.speed, 585, 500, 30, Color.Black);

            PassengerPlane? passengerPlane = plane as PassengerPlane;
            CargoPlane? cargoPlane = plane as CargoPlane;

            int upgradeCost = 0;
            if (passengerPlane != null)
            {
                upgradeCost = passengerPlane.level * PassengerPlane.levelPassangerMultplier * passengerPlane.level * PassengerPlane.levelPassangerMultplier / 2;
                Raylib.DrawText(passengerPlane.passangers.Count + "/" + PassengerPlane.levelPassangerMultplier * passengerPlane.level + " Passengers", 550, 545, 20, Color.Black);
            }
            else if (cargoPlane != null)
            {
                upgradeCost = cargoPlane.maxWeight * cargoPlane.maxWeight / 2;
                Raylib.DrawText(cargoPlane.GetCargoWeight() + "/" + cargoPlane.maxWeight + " cargo", 570, 545, 30, Color.Black);
            }

            Raylib.DrawRectangle(564, 580, 150, 50, Color.Green);
            Raylib.DrawText("Upgrade " + upgradeCost, 574, 585, 20, Color.Black);


            if (gamedata.money <= upgradeCost)
            {
                return;
            }
            if (plane.level >= 10)
            {
                return;
            }

            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(564, 580, 150, 50)) && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                gamedata.money -= upgradeCost;
                if (passengerPlane != null)
                {
                    passengerPlane.Upgrade();
                }
                if (cargoPlane != null)
                {
                    cargoPlane.Upgrade();
                }
            }
        }
    }
}
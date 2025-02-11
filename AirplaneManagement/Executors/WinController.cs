using Raylib_cs;

public class WinController : Executor
{
    public void LateUpdate(Gamedata gamedata)
    {

    }

    public void Update(Gamedata gamedata)
    {
        //Make sure airports arent overcroweded/filled
        for (int i = 0; i < gamedata.airports.Count; i++)
        {
            if (gamedata.airports[i].maxCargo < gamedata.airports[i].GetCargoWeight())
            {
                gamedata.alive = false;
            }
            if (gamedata.airports[i].maxPassengers < gamedata.airports[i].passengers.Count)
            {
                gamedata.alive = false;
            }
        }
    }
}
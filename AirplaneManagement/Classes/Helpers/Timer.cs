using Raylib_cs;

public class Timer : Executor
{
    float time;
    float timeUntilNextEvent;

    TimerType timerType;

    public Timer(float timeUntilNextEvent, TimerType timerType)
    {
        this.timeUntilNextEvent = timeUntilNextEvent;
        this.timerType = timerType;
    }

    public void Update(Gamedata gamedata)
    {
        time += Raylib.GetFrameTime();
        if (time >= timeUntilNextEvent)
        {
            time = 0;
            if (timerType == TimerType.AddAirport)
            {
                //Create a new airport
                Airport airport = Airport.GetNewAirport(gamedata.airports);
                if (airport != null)
                {
                    gamedata.airports.Add(airport);
                }

            }
            else if (timerType == TimerType.AddPassenger)
            {
                //Create a new passanger for every airport
                Random rand = new Random();
                for (int i = 0; i < gamedata.airports.Count; i++)
                {
                    //Try to find an unqie desitniation, if possible create new passnager/cargo
                    Airport destination = gamedata.airports[rand.Next(0, gamedata.airports.Count)];
                    if (destination.id != gamedata.airports[i].id)
                    {
                        Person p = new Person(destination);
                        p.CalculateRoute(gamedata.airports, gamedata.airports[i]);
                        gamedata.airports[i].passengers.Add(p);
                    }

                    if (gamedata.unlockedCargo)
                    {
                        destination = gamedata.airports[rand.Next(0, gamedata.airports.Count)];
                        if (destination.id != gamedata.airports[i].id)
                        {
                            Cargo cargo = new Cargo(destination, rand.Next(1, 4));
                            cargo.CalculateRoute(gamedata.airports, gamedata.airports[i]);
                            gamedata.airports[i].cargo.Add(cargo); 
                        }
                    }
                }
            }
        }
    }

    public void LateUpdate(Gamedata gamedata)
    {
        if (timerType == TimerType.AddAirport)
        {
            Raylib.DrawText("Next airport in: " + (timeUntilNextEvent - time).ToString("0"), 10, 10, 20, Color.Black);
        }
    }


}

public enum TimerType
{
    AddAirport,
    AddPassenger
}
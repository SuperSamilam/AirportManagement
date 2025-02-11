using Raylib_cs;

public class Timer : Executor
{
    float time;
    float TimeUntilNextEvent;

    TimerType timerType;

    public Timer(float TimeUntilNextEvent, TimerType timerType)
    {
        this.TimeUntilNextEvent = TimeUntilNextEvent;
        this.timerType = timerType;
    }

    public void Update(Gamedata gamedata)
    {
        time += Raylib.GetFrameTime();
        if (time >= TimeUntilNextEvent)
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
                int failed = 0;
                for (int i = 0; i < gamedata.airports.Count; i++)
                {
                    Airport destination = gamedata.airports[rand.Next(0, gamedata.airports.Count)];
                    if (destination.id == gamedata.airports[i].id) 
                    {
                        failed++;
                        continue;
                    }

                    Person p = new Person(destination);
                    p.CalculateRoute(gamedata.airports, gamedata.airports[i]);
                    gamedata.airports[i].passengers.Add(p);
                }
            }
        }
    }

    public void LateUpdate(Gamedata gamedata)
    {
        if (timerType == TimerType.AddAirport)
        {
            Raylib.DrawText("Next airport in: " + (TimeUntilNextEvent - time).ToString("0"), 10, 10, 20, Color.Black);
        }
    }


}

public enum TimerType
{
    AddAirport,
    AddPassenger
}
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
        Register.executorRegistry.Add(this);
    }

    public void Update(Gamedata gamedata)
    {
        time += Raylib.GetFrameTime();
        if (time >= TimeUntilNextEvent)
        {
            time = 0;
            if (timerType == TimerType.AddAirport)
            {
                Airport airport = Airport.GetNewAirport(gamedata.airports);
                if (airport != null)
                {
                    gamedata.airports.Add(airport);
                }

            }
            else if (timerType == TimerType.AddPassenger)
            {
                //Add passenger
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
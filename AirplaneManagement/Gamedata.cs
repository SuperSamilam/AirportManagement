public class Gamedata
{
    public List<Airport> airports;
    public List<Route> routes;
    public int money;
    public bool unlockedCargo; 
    public bool alive;

    public Gamedata()
    {
        airports = new List<Airport>();
        routes = new List<Route>();
        money = 20000;
        unlockedCargo = false;
        alive = true;
    }   
}
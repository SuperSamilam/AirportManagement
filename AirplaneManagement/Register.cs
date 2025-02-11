
public class Register
{
    //Makes sure ever executor updteas
    public List<Executor> executorRegistry = new List<Executor>();

    //Makes sure all update functions gets called
    public void UpdateExecutors(Gamedata gamedata)
    {
        for (int i = 0; i < executorRegistry.Count; i++)
        {
            executorRegistry[i].Update(gamedata);
        }
    }

    //Makes sure all LateUpdate functions gets called
    public void LateUpdateExecutors(Gamedata gamedata)
    {
        for (int i = 0; i < executorRegistry.Count; i++)
        {
            executorRegistry[i].LateUpdate(gamedata);
        }
    }
}
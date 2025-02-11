
public class Register
{
    //Makes sure ever executor updteas
    public List<Executor> executorRegistry = new List<Executor>();

    public void UpdateExecutors(Gamedata gamedata)
    {
        for (int i = 0; i < executorRegistry.Count; i++)
        {
            executorRegistry[i].Update(gamedata);
        }
    }

    public void LateUpdateExecutors(Gamedata gamedata)
    {
        for (int i = 0; i < executorRegistry.Count; i++)
        {
            executorRegistry[i].LateUpdate(gamedata);
        }
    }
}
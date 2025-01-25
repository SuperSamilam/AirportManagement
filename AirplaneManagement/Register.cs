
public static class Register
{
    public static List<Executor> executorRegistry = new List<Executor>();

    public static void UpdateExecutors(Gamedata gamedata)
    {
        for (int i = 0; i < executorRegistry.Count; i++)
        {
            executorRegistry[i].Update(gamedata);
        }
    }

    public static void LateUpdateExecutors(Gamedata gamedata)
    {
        for (int i = 0; i < executorRegistry.Count; i++)
        {
            executorRegistry[i].LateUpdate(gamedata);
        }
    }
}
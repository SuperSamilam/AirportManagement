
public static class Register
{
    public static List<Executor> executorRegistry = new List<Executor>();

    public static void UpdateExecutors()
    {
        for (int i = 0; i < executorRegistry.Count; i++)
        {
            executorRegistry[i].Update();
        }
    }
}

public class Gameobject
{
    public string id;

    public Gameobject()
    {
        id = Guid.NewGuid().ToString();
    }
}

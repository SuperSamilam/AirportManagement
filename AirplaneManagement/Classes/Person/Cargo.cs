public class Cargo : Person
{
    public int weight;

    public Cargo(Airport desitnation, int weight) : base(desitnation)
    {
        this.weight = weight;
    }
}
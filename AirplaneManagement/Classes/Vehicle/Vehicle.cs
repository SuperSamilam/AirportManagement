using System.Numerics;
using Raylib_cs;

public class Vehicle : Gameobject
{
    public static int levelSpeedMultplier = 1;

    public int level;
    public float speed;
    public Texture2D sprite;

    public Vehicle() : base()
    {
        this.speed = levelSpeedMultplier;
        level = 1;
    }

    //Upgrades the plane basic variabels
    public void Upgrade()
    {
        if (level == 10)
            return;
        level++;
        speed = level * levelSpeedMultplier;
    }
}


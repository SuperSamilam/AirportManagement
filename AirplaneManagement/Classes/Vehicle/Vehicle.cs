using System.Numerics;
using Raylib_cs;

public class Vehicle : Gameobject
{
    public float speed;
    public Texture2D sprite;

    public Vehicle(float speed, Texture2D sprite) : base()
    {
        this.speed = speed;
        this.sprite = sprite;
    }
}


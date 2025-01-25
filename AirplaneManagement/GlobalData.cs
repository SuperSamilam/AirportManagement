using System.Numerics;
using Raylib_cs;

public sealed class GlobalData
{
    static readonly Lazy<GlobalData> instance = new(() => new GlobalData());
    public static GlobalData Instance => instance.Value;

    private Vector2 _calculatedValue;
    public Vector2 CalculatedValue => _calculatedValue;

    private GlobalData() { }

    public void UpdateValue(Camera2D camera)
    {

        Vector2 newValue = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), camera); // Replace with actual logic

        if (_calculatedValue != newValue) // Only trigger event if value changes
        {
            _calculatedValue = newValue;
        }
    }
}
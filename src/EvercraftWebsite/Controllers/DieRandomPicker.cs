namespace EvercraftWebsite.Controllers;

public class DieRandomPicker : Random
{
    public override int Next(int minValue, int maxValue)
    {
        return base.Next(minValue, maxValue);
    }
}
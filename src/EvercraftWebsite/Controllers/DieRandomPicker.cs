namespace EvercraftWebsite.Controllers;

public class DieRandomPicker : Random
{
    public override int Next(int minValue, int maxValue)
    {
        var baseRoll = base.Next(minValue, maxValue);
        bool hasCustomRoll = true;
        if (hasCustomRoll)
        {
            int customRoll = 11;
            baseRoll = customRoll;
        }
        return baseRoll;
    }
}
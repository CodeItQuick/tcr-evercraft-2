namespace EvercraftWebsite.Controllers;

public class DieRandomPicker : Random
{
    private bool _hasCustomRoll = true;
    private int _customRoll = 11;

    public override int Next(int minValue, int maxValue)
    {
        var baseRoll = base.Next(minValue, maxValue);
        if (_hasCustomRoll)
        {
            baseRoll = _customRoll;
        }
        return baseRoll;
    }
}
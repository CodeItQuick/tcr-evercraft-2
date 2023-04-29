namespace EvercraftWebsite.Controllers;

public class DieRandomPicker : Random
{
    private bool _hasCustomRoll = false;
    private int _customRoll = 11;

    public DieRandomPicker()
    {
    }

    public DieRandomPicker(bool hasCustomRoll, int customRoll)
    {
        _hasCustomRoll = hasCustomRoll;
        _customRoll = customRoll;
    }

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
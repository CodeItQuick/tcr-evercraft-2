namespace EvercraftWebsite.Controllers;

public class DieRandomPicker : Random
{
    private bool _hasCustomRoll = false;
    private int _customRoll = 11;

    public DieRandomPicker()
    {
    }

    public DieRandomPicker(int customRoll)
    {
        _hasCustomRoll = true;
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
namespace Device.BusinessLogic.Services;


public class BAtteryPercentageFunctionality
{
    private int BatteryPercentage { get; set; }

    public BAtteryPercentageFunctionality(int Battery)
    {
        if (Battery > 100 || Battery < 0)
        {
            throw new ArgumentException("error has to be o-100");
        }
        BatteryPercentage = Battery;
    }
    public bool isLowOnBattery()
    {
        return BatteryPercentage < 20;
    }

    public bool smallerThen11()
    {
        return BatteryPercentage < 11;
    }

    public void BatterMinus10()
    {
        BatteryPercentage -= 10;
        if (BatteryPercentage < 0)
        {
            BatteryPercentage = 0;
        }
    }
    public int GetBatteryLevel()
    {
        return BatteryPercentage;
    }
    public void SetBatteryLevel(int value)
    {
        if (value < 0 || value > 100)
            throw new ArgumentException("Battery must be between 0 and 100.");

        BatteryPercentage = value;
    }

}
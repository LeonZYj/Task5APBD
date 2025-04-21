namespace DeviceManager.Entries.Devices
{
    public class BatteryPercentageFunctionality
    {
        private int batteryLevel;

        public BatteryPercentageFunctionality(int initialLevel)
        {
            batteryLevel = initialLevel;
        }

        public int GetBatteryLevel()
        {
            return batteryLevel;
        }

        public void SetBatteryLevel(int level)
        {
            batteryLevel = level;
        }

        public bool isLowOnBattery()
        {
            return batteryLevel < 20;
        }

        public bool smallerThen11()
        {
            return batteryLevel < 11;
        }

        public void BatterMinus10()
        {
            batteryLevel -= 10;
            if (batteryLevel < 0) batteryLevel = 0;
        }
    }
}
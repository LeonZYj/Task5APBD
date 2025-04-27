using DeviceManager.Entries.Exceptions;
using DeviceManager.Entries.Interfaces;

namespace DeviceManager.Entries.Devices
{
    public class SmartWatch : Device, IPowerNotifier, BatteryPowerInterfacesmartwatchca
    {
        private BatteryPercentageFunctionality batteryBattery { get; set; }
        public BatteryPercentageFunctionality BatteryPercentageFunctionality { get; set; }
        
        public SmartWatch() : base()
        {
            batteryBattery = new BatteryPercentageFunctionality(100);
        }
        public SmartWatch(string Id, string name, bool isTurnedOn, int batteryPercentage)
            : base(Id, name, isTurnedOn)
        {
            batteryBattery = new BatteryPercentageFunctionality(batteryPercentage);
            if (batteryBattery.isLowOnBattery())
                NotifyLowPower();
        }

        public override void TurnOn()
        {
            if (batteryBattery.smallerThen11())
            {
                throw new EmptyBatteryException($"Cant turn on {Name} with battery lower then 11%");
            }

            IsTurnedOn = true;
            batteryBattery.BatterMinus10();

            if (batteryBattery.isLowOnBattery())
            {
                NotifyLowPower();
            }

            Console.WriteLine($"{Name} is turrned on with battery {batteryBattery.GetBatteryLevel()}%");
        }


        public void NotifyLowPower()
        {
            Console.WriteLine($"Warning: {Name} has low battery with battery {batteryBattery.GetBatteryLevel()}%");
        }


        public override string ToString()
        {
            return
                $"[SmartWatch] ID: {Id}, Name: {Name}, IsTurnedOn: {IsTurnedOn}, Battery: {batteryBattery.GetBatteryLevel()}%";
        }

        public void SetBatteryLevel(int value)
        {
            batteryBattery.SetBatteryLevel(value);
        }

        public int getBatteryLevel()
        {
            return batteryBattery.GetBatteryLevel();
        }

        public void setBatteryLevel(int level)
        {
            batteryBattery.SetBatteryLevel(level);
        }
    }
}
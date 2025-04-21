namespace DeviceManager.Entries.Exceptions
{

    public class EmptyBatteryException : Exception
    {
        public EmptyBatteryException(string message) : base(message)
        {
        }
    }
}
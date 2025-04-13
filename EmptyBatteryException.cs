namespace ConsoleApp1;

public class EmptyBatteryException : Exception
{
    public EmptyBatteryException(string message) : base(message) {}
}
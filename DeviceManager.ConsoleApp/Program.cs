// See https://aka.ms/new-console-template for more information
namespace DeviceManager.ConsoleApp;


class Program
{
    static void Main()
    {
        string filePath = "input.txt";
        FactoryDeviceManagerInterface factory = new DeviceManagerMainFac();
        IDeviceManagerMainOperations operationmanager = factory.Create(filePath);

        Console.WriteLine("\nAll Devices:");
        foreach (var device in operationmanager.getDevices())
        {
            Console.WriteLine(device);
        }

        Console.WriteLine("\nTurning On Devices:");
        foreach (var device in operationmanager.getDevices())
        {
            operationmanager.turnOnDevice(device);
        }
    }
}
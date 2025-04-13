namespace ConsoleApp1;

public interface FactoryDeviceManagerInterface
{
    IDeviceManagerMainOperations Create(string filepath);
}
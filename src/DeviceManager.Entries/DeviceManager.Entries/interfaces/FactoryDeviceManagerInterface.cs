namespace DeviceManager.Entries.Interfaces
{


    public interface FactoryDeviceManagerInterface
    {
        IDeviceManagerMainOperations Create(string filepath);
    }
}
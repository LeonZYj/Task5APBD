namespace ConsoleApp1;

public interface IDeviceManagerMainOperations
{
    List<Device> getDevices();
    void addDevice(Device device);
    void removeDevice(Device device);
    void turnOnDevice(Device device);
    void turnOffDevice(Device device);
    void editDevice(long id, object newDevice);
}
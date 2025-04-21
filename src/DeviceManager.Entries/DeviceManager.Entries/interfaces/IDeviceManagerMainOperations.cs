namespace DeviceManager.Entries.Interfaces
{


    public interface IDeviceManagerMainOperations
    {
        List<Devices.Device> getDevices();
        void addDevice(Devices.Device device);
        void removeDevice(Devices.Device device);
        void turnOnDevice(Devices.Device device);
        void turnOffDevice(Devices.Device device);
        void editDevice(long id, object newDevice);
    }
}
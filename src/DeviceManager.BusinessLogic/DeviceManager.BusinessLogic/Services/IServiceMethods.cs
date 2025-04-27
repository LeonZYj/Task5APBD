using DeviceManager.Entries.Devices;
using DeviceClass = DeviceManager.Entries.Devices.Device;



namespace Device.BusinessLogic.Services;

public interface IServiceMethods
{
    IEnumerable<PersonalComputer> getAllPersonalComputers();
    PersonalComputer getPersonalComputerID(String id);
    bool addPersonalComputer(PersonalComputer computer);
    bool deletePersonalComputer(String id);
    bool updatePersonalComputer(PersonalComputer computer);
    
    IEnumerable<SmartWatch> getAllSmartWatches();
    SmartWatch getSmartWatchID(String id);
    bool addSmartWatch(SmartWatch watch);
    bool deleteSmartWatch(String id);
    bool updateSmartWatch(SmartWatch watch);
    
    IEnumerable<EmbeddedDevice> getAllEmbeddedDevices();
    EmbeddedDevice getEmbeddedDeviceID(String id);
    bool addEmbeddedDevice(EmbeddedDevice device);
    bool deleteEmbeddedDevice(String id);
    bool updateEmbeddedDevice(EmbeddedDevice device);

    IEnumerable<DeviceClass> getAllDevices();
    DeviceClass getDeviceID(String id);
    bool addDevice(DeviceClass device);
    bool deleteDevice(String id);
    bool updateDevice(DeviceClass device);
    
    
    

}
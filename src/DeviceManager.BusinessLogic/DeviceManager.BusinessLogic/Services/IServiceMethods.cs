using DeviceManager.Entries.Devices;

namespace Device.BusinessLogic.Services;

public interface IServiceMethods
{
    IEnumerable<PersonalComputer> getAllPersonalComputers();
    PersonalComputer getPersonalComputerID(String id);
    bool addPersonalComputer(PersonalComputer computer);
    bool deletePersonalComputer(String id);
    
    IEnumerable<SmartWatch> getAllSmartWatches();
    SmartWatch getSmartWatchID(String id);
    bool addSmartWatch(SmartWatch watch);
    bool deleteSmartWatch(String id);
    
    IEnumerable<EmbeddedDevice> getAllEmbeddedDevices();
    EmbeddedDevice getEmbeddedDeviceID(String id);
    bool addEmbeddedDevice(EmbeddedDevice device);
    bool deleteEmbeddedDevice(String id);
}
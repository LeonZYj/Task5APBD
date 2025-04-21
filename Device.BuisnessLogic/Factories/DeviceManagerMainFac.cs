namespace Device.BusinessLogic.Factories;


public class DeviceManagerMainFac : FactoryDeviceManagerInterface
{
    public IDeviceManagerMainOperations Create(string filepath)
    {
        IFileService fileService = new FileService();
        ISplittData splitdata = new DeviceSplitter();
        return new DeviceManagerMainOperations(fileService, splitdata, filepath);

    }
}
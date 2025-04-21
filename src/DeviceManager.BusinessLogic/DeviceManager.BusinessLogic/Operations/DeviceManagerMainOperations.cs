using DeviceManager.Entries.Devices;
using DeviceManager.Entries.Interfaces;
using DeviceClass = DeviceManager.Entries.Devices.Device;

namespace Device.BusinessLogic.Operations
{
    public class DeviceManagerMainOperations : IDeviceManagerMainOperations
    {
        private readonly IFileService _fileService;
        private readonly ISplittData _dataSplitter;
        private readonly string _filePath;
        private readonly List<DeviceClass> _devices;

        public DeviceManagerMainOperations(IFileService fileService, ISplittData dataSplitter, string filePath)
        {
            _fileService = fileService;
            _dataSplitter = dataSplitter;
            _filePath = filePath;
            _devices = new List<DeviceClass>();

            LoadDevices();
        }

        public void LoadDevices()
        {
            List<string> lines = _fileService.ReadFile(_filePath);

            foreach (string line in lines)
            {
                try
                {
                    DeviceClass device = _dataSplitter.parseData(line);
                    _devices.Add(device);
                }
                catch (Exception e)
                {
                    Console.WriteLine("skipo " + e.Message);
                }
            }
        }

        public List<DeviceClass> getDevices() => _devices;

        public void addDevice(DeviceClass device)
        {
            _devices.Add(device);
            SaveDevices();
        }

        public void removeDevice(DeviceClass device)
        {
            _devices.Remove(device);
            SaveDevices();
        }

        public void turnOnDevice(DeviceClass device)
        {
            try
            {
                device.TurnOn();
            }
            catch (Exception e)
            {
                Console.WriteLine("cant turn on device " + e.Message);
            }
        }

        public void turnOffDevice(DeviceClass device)
        {
            device.TurnOff();
        }

        public void editDevice(long id, object newDevice)
        {
            string isString = id.ToString();
            foreach (DeviceClass device in _devices)
            {
                if (device.Id == isString)
                {
                    if (device is PersonalComputer && newDevice is string os)
                    {
                        ((PersonalComputer)device).OperatingSystem = os;
                    }
                    else if (device is BatteryPowerInterfacesmartwatchca batteryDevice && newDevice is int batteryLevel)
                    {
                        batteryDevice.setBatteryLevel(batteryLevel);
                    }
                    else if (device is EmbeddedDevice && newDevice is string network)
                    {
                        ((EmbeddedDevice)device).SetNetworkName(network);
                    }

                    SaveDevices();
                    return;
                }
            }

            Console.WriteLine("not id found " + id);
        }

        private void SaveDevices()
        {
            string allLines = string.Join(Environment.NewLine, _devices.Select(d => d.ToString()));
            _fileService.writeLineToFile(_filePath, allLines);
        }
    }
}

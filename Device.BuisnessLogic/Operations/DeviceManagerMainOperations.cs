using Device.Entries.Interfaces;
using Device.Entries.Devices;
using Device.Entries.Exceptions;
using Device.BusinessLogic.Services;


namespace Device.BusinessLogic.Operations;



    public class DeviceManagerMainOperations : IDeviceManagerMainOperations
    {
        private IFileService _fileService;
        private ISplittData _dataSplitter;
        private string _filePath;
        private List<Device> _devices;

        public DeviceManagerMainOperations(IFileService fileService, ISplittData dataSplitter, string filePath)
        {
            _fileService = fileService;
            _dataSplitter = dataSplitter;
            _filePath = filePath;
            _devices = new List<Device>();

            LoadDevices();
        }

        public void LoadDevices()
        {
            List<string> lines = _fileService.ReadFile(_filePath);

            foreach (string line in lines)
            {
                try
                {
                    Device device = _dataSplitter.parseData(line);
                    _devices.Add(device);
                }
                catch (Exception e)
                {
                    Console.WriteLine("skipo " + e.Message);
                }
            }
        }

        public List<Device> getDevices()
        {
            return _devices;
        }

        public void addDevice(Device device)
        {
            _devices.Add(device);
            SaveDevices();
        }

        public void removeDevice(Device device)
        {
            _devices.Remove(device);
            SaveDevices();
        }

        public void turnOnDevice(Device device)
        {
            try
            {
                device.TurnOn();
            }
            catch (Exception e)
            {
                Console.WriteLine("cant turn om device " + e.Message);
            }
        }

        public void turnOffDevice(Device device)
        {
            device.TurnOff();
        }

        public void editDevice(long id, object newDevice)
        {
            string isString = id.ToString();
            foreach (Device device in _devices)
            {
                if (device.Id == isString)
                {
                    if (device is PersonalComputer && newDevice is string)
                    {
                        ((PersonalComputer)device).OperatingSystem = (string)newDevice;
                    }
                    else if (device is BatteryPowerInterfacesmartwatchca batteryDevice && newDevice is int batteryLevel)
                    {
                        batteryDevice.setBatteryLevel(batteryLevel);
                    }
                    else if (device is EmbeddedDevice && newDevice is string)
                    {
                        ((EmbeddedDevice)device).SetNetworkName((string)newDevice);
                    }

                    SaveDevices();
                    return;
                }
            }

            Console.WriteLine("not id found " + id);
        }

        private void SaveDevices()
        {
            string allLines = "";
            foreach (Device device in _devices)
            {
                allLines += device.ToString() + Environment.NewLine;
            }

            _fileService.writeLineToFile(_filePath, allLines);
        }
    }

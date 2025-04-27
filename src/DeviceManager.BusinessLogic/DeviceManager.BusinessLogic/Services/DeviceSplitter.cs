

using DeviceManager.Entries.Devices;
using DeviceManager.Entries.Interfaces;

namespace Device.BusinessLogic.Services
{

    public class DeviceSplitter : ISplittData
    {
        public DeviceManager.Entries.Devices.Device parseData(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new ArgumentException("empty");
            string[] parts = line.Split(',');

            if (parts.Length < 3)
                throw new FormatException($"Invalid {line}");

            string id = parts[0];
            string name = parts[1];

            try
            {
                if (id.StartsWith("P"))
                {
                    bool isTurnedOn = bool.Parse(parts[2]);
                    string os = parts.Length > 3 ? parts[3] : "Unknown";
                    return new PersonalComputer(id, name, isTurnedOn, os);
                }
                else if (id.StartsWith("SW"))
                {
                    bool isTurnedOn = bool.Parse(parts[2]);
                    int battery = int.Parse(parts[3].Replace("%", ""));
                    return new SmartWatch(id, name, isTurnedOn, battery);
                }
                else if (id.StartsWith("ED"))
                {
                    if (parts.Length < 4)
                        throw new FormatException($"No IP; {line}");

                    string ip = parts[2];
                    string network = parts[3];
                    return new EmbeddedDevice( name, false, ip, network);
                }

                throw new NotSupportedException($"Unknown: {id}");
            }
            catch (Exception)
            {
                throw new Exception($"Error when splitting line {line}.");
            }
        }
    }
}
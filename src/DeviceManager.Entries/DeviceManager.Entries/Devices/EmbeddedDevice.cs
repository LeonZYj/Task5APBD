using DeviceManager.Entries.Interfaces;
using DeviceManager.Entries.Devices;
using DeviceManager.Entries.Exceptions;
using System.Text.RegularExpressions;



namespace DeviceManager.Entries.Devices
{

    public class EmbeddedDevice : Device
    {
        private string _ipAddress;
        private string _networkName;

        public EmbeddedDevice(string id, string name, bool isTurnedOn, string ipAddress, string networkName)
            : base(id, name, isTurnedOn)
        {
            if (!IsValidIpAddress(ipAddress))
                throw new ArgumentException("IP doesn't match the format.");

            _ipAddress = ipAddress;
            _networkName = string.IsNullOrEmpty(networkName) ? "Unknown" : networkName;
        }

        public string GetIpAddress() => _ipAddress;
        public string GetNetworkName() => _networkName;

        public static bool IsValidIpAddress(string ipAddress)
        {
            string pattern = @"^(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]?)\."
                             + @"(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]?)\."
                             + @"(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]?)\."
                             + @"(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]?)$";

            return Regex.IsMatch(ipAddress, pattern);
        }


        public void Connect()
        {
            if (!_networkName.Contains("MD Ltd."))
                throw new ConnectionException($"Network '{_networkName}' is not authorized. Must contain 'MD Ltd.'");
        }

        public override void TurnOn()
        {
            Connect();
            IsTurnedOn = true;
            Console.WriteLine($"{Name} is turned on and connected to {_networkName} with IP Address: {_ipAddress}");
        }

        public void SetNetworkName(string newNetwork)
        {
            if (string.IsNullOrEmpty(newNetwork))
            {
                Console.WriteLine("Error: Network name cannot be empty.");
                return;
            }

            _networkName = newNetwork;
        }

        public override string ToString()
        {
            return
                $"[EmbeddedDevice] Name: {Name}, ID: {Id}, IsTurnedOn: {IsTurnedOn}, Network: {_networkName}, IP: {_ipAddress}";
        }
    }

    public class ConnectionException : Exception
    {
        public ConnectionException(string message) : base(message)
        {
        }
    }
}
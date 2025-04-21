namespace DeviceManager.Entries.Devices
{

    public class PersonalComputer : Device
    {
        public string OperatingSystem { get; set; }

        public PersonalComputer(string id, string name, bool isTurnedOn, string operatingSystem) : base(id, name,
            isTurnedOn)
        {
            OperatingSystem = string.IsNullOrEmpty(operatingSystem) ? "Unknown" : operatingSystem;
        }

        public override void TurnOn()
        {
            if (OperatingSystem == "Unknown")
                throw new EmptySystemException("cant turn on no operating systen");

            IsTurnedOn = true;
            Console.WriteLine($"{Name} turned on on {OperatingSystem}");
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, IsTurnedOn: {IsTurnedOn}, OperatingSystem: {OperatingSystem}";

        }

        public class EmptySystemException : Exception
        {
            public EmptySystemException(string message) : base(message)
            {
            }
        }
    }
}
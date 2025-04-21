namespace DeviceManager.Database;

public class EmbeddedDevice
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsTurnedOn { get; set; }
    public string IpAddress { get; set; }
    public string NetworkName { get; set; }
}

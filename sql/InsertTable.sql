-- Insert devices into base Device table
INSERT INTO Device (Id, Name, IsEnabled) VALUES
                                             ('D-100', 'Apple Watch Series 8', 1),
                                             ('D-200', 'Dell XPS 13', 0),
                                             ('D-300', 'Raspberry Pi Zero', 1);

-- Insert into Smartwatch (Id auto-generated)
INSERT INTO Smartwatch (BatteryPercentage, DeviceId) VALUES
    (78, 'D-100');

-- Insert into PersonalComputer (Id auto-generated)
INSERT INTO PersonalComputer (OperatingSystem, DeviceId) VALUES
    ('Windows 11 Pro', 'D-200');

-- Insert into Embedded (Id auto-generated)
INSERT INTO Embedded (IpAddress, NetworkName, DeviceId) VALUES
    ('192.168.0.42', 'IoT_Network', 'D-300');

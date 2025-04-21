INSERT INTO SmartWatch (Id, Name, IsTurnedOn, BatteryLevel)
VALUES ('SW-1', 'Apple Watch SE2', 1, 27);

INSERT INTO PersonalComputer (Id, Name, IsTurnedOn, OperatingSystem)
VALUES
    ('P-1', 'LinuxPC', 0, 'Linux Mint'),
    ('P-2', 'ThinkPad T440', 0, NULL),
    ('Capital33', 'BestPC_Ever', 0, '456217865891789');

INSERT INTO EmbeddedDevice (Id, Name, IsTurnedOn, IpAddress, NetworkName)
VALUES
    ('ED-1', 'Pi3', 1, '192.168.1.44', 'MD Ltd.Wifi-1'),
    ('ED-2', 'Pi4', 1, '192.168.1.45', 'eduroam'),
    ('ED-3', 'Pi4', 1, 'whatisIP', 'MyWifiName');

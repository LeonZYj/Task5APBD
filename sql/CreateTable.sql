CREATE TABLE PersonalComputer (
                                  Id NVARCHAR(50) PRIMARY KEY,
                                  Name NVARCHAR(100) NOT NULL,
                                  IsTurnedOn BIT NOT NULL,
                                  OperatingSystem NVARCHAR(100)
);

CREATE TABLE EmbeddedDevice (
                                Id NVARCHAR(50) PRIMARY KEY,
                                Name NVARCHAR(100) NOT NULL,
                                IsTurnedOn BIT NOT NULL,
                                IpAddress NVARCHAR(50),
                                NetworkName NVARCHAR(100)
);

CREATE TABLE SmartWatch (
                            Id NVARCHAR(50) PRIMARY KEY,
                            Name NVARCHAR(100) NOT NULL,
                            IsTurnedOn BIT NOT NULL,
                            BatteryLevel INT
);

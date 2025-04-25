CREATE TABLE Device (
                        Id VARCHAR(50) PRIMARY KEY,
                        Name NVARCHAR(100) NOT NULL,
                        IsEnabled BIT NOT NULL
);

CREATE TABLE Smartwatch (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            BatteryPercentage INT,
                            DeviceId VARCHAR(50),
                            FOREIGN KEY (DeviceId) REFERENCES Device(Id)
);

CREATE TABLE PersonalComputer (
                                  Id INT IDENTITY(1,1) PRIMARY KEY,
                                  OperatingSystem VARCHAR(100),
                                  DeviceId VARCHAR(50),
                                  FOREIGN KEY (DeviceId) REFERENCES Device(Id)
);

CREATE TABLE Embedded (
                          Id INT IDENTITY(1,1) PRIMARY KEY,
                          IpAddress VARCHAR(50),
                          NetworkName VARCHAR(100),
                          DeviceId VARCHAR(50),
                          FOREIGN KEY (DeviceId) REFERENCES Device(Id)
);

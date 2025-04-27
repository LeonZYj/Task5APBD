using System.Collections;
using System.ComponentModel.Design;
using DeviceManager.Entries.Devices;
using Microsoft.Data.SqlClient;
using DeviceClass = DeviceManager.Entries.Devices.Device;


namespace Device.BusinessLogic.Services;

public class ServiceMethod : IServiceMethods
{
    private readonly string connectionString;

    public ServiceMethod(String connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<PersonalComputer>? getAllPersonalComputers()
    {
        List<PersonalComputer> personalComputers = [];

        string query = @"SELECT d.ID, d.Name , d.IsEnabled, pc.OperatingSystem
           FROM Device d
           INNER JOIN PersonalComputer pc ON d.Id = pc.DeviceId";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var personalComputer = new PersonalComputer
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            IsTurnedOn = reader.GetBoolean(2),
                            OperatingSystem = reader.GetString(3)
                        };
                        personalComputers.Add(personalComputer);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }

        return personalComputers;
    }

    public PersonalComputer getPersonalComputerID(string id)
    {
        PersonalComputer personalComputer = null;
        String query = @"SELECT d.ID, d.Name , d.IsEnabled, pc.OperatingSystem
           FROM Device d
           INNER JOIN PersonalComputer pc ON d.Id = pc.DeviceId
           WHERE d.Id = @id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            var reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        personalComputer = new PersonalComputer
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            IsTurnedOn = reader.GetBoolean(2),
                            OperatingSystem = reader.GetString(3)
                        };
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }

        return personalComputer;
    }

    public bool addPersonalComputer(PersonalComputer personalComputer)
    {
        string InsertQueryDevice = @"
        INSERT INTO Device (Name, IsEnabled)
        OUTPUT INSERTED.Id
        VALUES (@Name, @IsEnabled);";

        string InsertQueryPC = @"
        INSERT INTO PersonalComputer (OperatingSystem, DeviceId)
        VALUES (@OperatingSystem, @DeviceId);";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            if (string.IsNullOrWhiteSpace(personalComputer.Id))
            {
                personalComputer.Id = Guid.NewGuid().ToString();
            }

            using (SqlCommand command1 = new SqlCommand(InsertQueryDevice, connection))
            {
                command1.Parameters.AddWithValue("@Id", personalComputer.Id);
                command1.Parameters.AddWithValue("@Name", personalComputer.Name);
                command1.Parameters.AddWithValue("@IsEnabled", personalComputer.IsTurnedOn);

                command1.ExecuteNonQuery();
            }


            using (SqlCommand command2 = new SqlCommand(InsertQueryPC, connection))
            {
                command2.Parameters.AddWithValue("@OperatingSystem", personalComputer.OperatingSystem);
                command2.Parameters.AddWithValue("@DeviceId", personalComputer.Id);

                command2.ExecuteNonQuery();
            }
        }

        return true;
    }


    public bool deletePersonalComputer(string id)
    {
        string query = @"DELETE FROM PersonalComputer WHERE Id = @Id;
                        DELETE FROM Device WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        return true;
    }
    public bool updatePersonalComputer(PersonalComputer personalComputer)
    {
        string query = @"UPDATE PersonalComputer SET OperatingSystem = @OperatingSystem WHERE Id = @Id;
                        UPDATE Device SET Name = @Name, IsEnabled = @IsEnabled WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", personalComputer.Id);
                command.Parameters.AddWithValue("@Name", personalComputer.Name);
                command.Parameters.AddWithValue("@IsEnabled", personalComputer.IsTurnedOn);
                command.Parameters.AddWithValue("@OperatingSystem", personalComputer.OperatingSystem);

                command.ExecuteNonQuery();
            }
        }

        return true;
    }

    public IEnumerable<SmartWatch> getAllSmartWatches()
    {
        List<SmartWatch> smartWatches = [];

        string query = @"SELECT d.ID, d.Name, d.IsEnabled, sw.Batterylife
                        FROM Device d 
                        INNER JOIN SmartWatch sw on d.ID = sw.DeviceID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var Smartwatch = new SmartWatch
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            IsTurnedOn = reader.GetBoolean(2),
                            BatteryPercentageFunctionality = new BatteryPercentageFunctionality(reader.GetInt32(3))
                        };

                        smartWatches.Add(Smartwatch);
                    }
                }
            }
            finally
            {
                reader.Close();
            }

        }

        return smartWatches;
    }

   
    public SmartWatch getSmartWatchID(string id)
    {
        SmartWatch smartWatch = null;
        string query = @"SELECT d.ID, d.Name, d.IsEnable, sw.Batterylif
                        FROM Device d 
                        INNER JOIN SmartWatch sw on d.ID = sw.DeviceID
                        WHERE d.ID = @Id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            var reader = command.ExecuteReader();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        smartWatch = new SmartWatch
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            IsTurnedOn = reader.GetBoolean(2),
                            BatteryPercentageFunctionality = new BatteryPercentageFunctionality(reader.GetInt32(3))
                        };
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }

        return smartWatch;
    }

    public bool addSmartWatch(SmartWatch smartWatch)
    {
        string InsertQueryDevice = @"
        INSERT INTO Device (Id, Name, IsEnabled)
        VALUES (@Id, @Name, @IsEnabled)";

        string InsertQueryPC = @"
        INSERT INTO SmartWatch (Id, BatteryLife)
        VALUES (@Id, @BatteryLife)";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            {
                var command1 = new SqlCommand(InsertQueryDevice, connection);
                var command2 = new SqlCommand(InsertQueryPC, connection);

                command1.Parameters.AddWithValue("@Id", smartWatch.Id);
                command1.Parameters.AddWithValue("@Name", smartWatch.Name);
                command1.Parameters.AddWithValue("@IsEnabled", smartWatch.IsTurnedOn);

                command1.ExecuteNonQuery();

                command2.Parameters.AddWithValue("@Id", smartWatch.Id);
                command2.Parameters.AddWithValue("@BatteryLife",
                    smartWatch.BatteryPercentageFunctionality.GetBatteryLevel());

                command2.ExecuteNonQuery();
            }
        }

        return true;
    }

    public bool deleteSmartWatch(string id)
    {
        string query = @"DELETE FROM SmartWatch WHERE Id = @Id;
                        DELETE FROM Device WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        return true;
    }
    public bool updateSmartWatch(SmartWatch smartWatch)
    {
        string query = @"UPDATE SmartWatch SET BatteryLife = @BatteryLife WHERE Id = @Id;
                        UPDATE Device SET Name = @Name, IsEnabled = @IsEnabled WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", smartWatch.Id);
                command.Parameters.AddWithValue("@Name", smartWatch.Name);
                command.Parameters.AddWithValue("@IsEnabled", smartWatch.IsTurnedOn);
                command.Parameters.AddWithValue("@BatteryLife",
                    smartWatch.BatteryPercentageFunctionality.GetBatteryLevel());

                command.ExecuteNonQuery();
            }
        }

        return true;
    }

    public IEnumerable<EmbeddedDevice> getAllEmbeddedDevices()
    {
        List<EmbeddedDevice> embeddedDevices = [];

        string query = @"SELECT d.ID, d.Name, d.IsEnabled, ed.NetworkName, ed.IPAddress
                        FROM Device d 
                        INNER JOIN EmbeddedDevice ed on d.ID = ed.DeviceID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var embeddedDevice = new EmbeddedDevice
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            IsTurnedOn = reader.GetBoolean(2),
                            _networkName = reader.GetString(3),
                            _ipAddress = reader.GetString(4)
                        };
                        embeddedDevices.Add(embeddedDevice);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }
        return embeddedDevices;
    }
    public EmbeddedDevice getEmbeddedDeviceID(string id)
    {
        EmbeddedDevice embeddedDevice = null;
        string query = @"SELECT d.ID, d.Name, d.IsEnabled, ed.NetworkName, ed.IPAddress
                        FROM Device d 
                        INNER JOIN EmbeddedDevice ed on d.ID = ed.DeviceID
                        WHERE d.ID = @Id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            var reader = command.ExecuteReader();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        embeddedDevice = new EmbeddedDevice
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            IsTurnedOn = reader.GetBoolean(2),
                            _networkName = reader.GetString(3),
                            _ipAddress = reader.GetString(4)
                        };
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }

        return embeddedDevice;
    }
    public bool addEmbeddedDevice(EmbeddedDevice embeddedDevice)
    {
        string InsertQueryDevice = @"
        INSERT INTO Device (Id, Name, IsEnabled)
        VALUES (@Id, @Name, @IsEnabled)";

        string InsertQueryPC = @"
        INSERT INTO EmbeddedDevice (Id, NetworkName, IPAddress)
        VALUES (@Id, @NetworkName, @IPAddress)";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            {
                var command1 = new SqlCommand(InsertQueryDevice, connection);
                var command2 = new SqlCommand(InsertQueryPC, connection);

                command1.Parameters.AddWithValue("@Id", embeddedDevice.Id);
                command1.Parameters.AddWithValue("@Name", embeddedDevice.Name);
                command1.Parameters.AddWithValue("@IsEnabled", embeddedDevice.IsTurnedOn);

                command1.ExecuteNonQuery();

                command2.Parameters.AddWithValue("@Id", embeddedDevice.Id);
                command2.Parameters.AddWithValue("@NetworkName", embeddedDevice.GetNetworkName());
                command2.Parameters.AddWithValue("@IPAddress", embeddedDevice.GetIpAddress());

                command2.ExecuteNonQuery();
            }
        }

        return true;
    }
    public bool deleteEmbeddedDevice(string id)
    {
        string query = @"DELETE FROM EmbeddedDevice WHERE Id = @Id;
                        DELETE FROM Device WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        return true;
    }
    public bool updateEmbeddedDevice(EmbeddedDevice embeddedDevice)
    {
        string query = @"UPDATE EmbeddedDevice SET NetworkName = @NetworkName, IPAddress = @IPAddress WHERE Id = @Id;
                        UPDATE Device SET Name = @Name, IsEnabled = @IsEnabled WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", embeddedDevice.Id);
                command.Parameters.AddWithValue("@Name", embeddedDevice.Name);
                command.Parameters.AddWithValue("@IsEnabled", embeddedDevice.IsTurnedOn);
                command.Parameters.AddWithValue("@NetworkName", embeddedDevice.GetNetworkName());
                command.Parameters.AddWithValue("@IPAddress", embeddedDevice.GetIpAddress());

                command.ExecuteNonQuery();
            }
        }

        return true;
    }

    public IEnumerable<DeviceNonAbstractClass> getAllDevices()
    {
        List<DeviceNonAbstractClass> devices = [];

        string query1111 = @"SELECT * FROM Device";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query1111, connection);
            connection.Open();
            
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var deviceAB = new DeviceNonAbstractClass()
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            IsTurnedOn = reader.GetBoolean(2)
                        };
                        devices.Add(deviceAB);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }
        return devices;
    }
    
    
    public DeviceNonAbstractClass getDeviceID(string id)
    {
        DeviceNonAbstractClass device = null;
        string query = @"SELECT * FROM Device WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            var reader = command.ExecuteReader();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        device = new DeviceNonAbstractClass
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            IsTurnedOn = reader.GetBoolean(2)
                        };
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }

        return device;
    }
    public bool addDevice(DeviceNonAbstractClass device)
    {
        string InsertQueryDevice = @"
        INSERT INTO Device (Id, Name, IsEnabled)
        VALUES (@Id, @Name, @IsEnabled)";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            {
                var command1 = new SqlCommand(InsertQueryDevice, connection);

                command1.Parameters.AddWithValue("@Id", device.Id);
                command1.Parameters.AddWithValue("@Name", device.Name);
                command1.Parameters.AddWithValue("@IsEnabled", device.IsTurnedOn);

                command1.ExecuteNonQuery();
            }
        }

        return true;
    }
    public bool deleteDevice(string id)
    {
        string query = @"DELETE FROM Device WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        return true;
    }
    public bool updateDevice(DeviceNonAbstractClass device)
    {
        string query = @"UPDATE Device SET Name = @Name, IsEnabled = @IsEnabled WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", device.Id);
                command.Parameters.AddWithValue("@Name", device.Name);
                command.Parameters.AddWithValue("@IsEnabled", device.IsTurnedOn);

                command.ExecuteNonQuery();
            }
        }

        return true;
    }
}

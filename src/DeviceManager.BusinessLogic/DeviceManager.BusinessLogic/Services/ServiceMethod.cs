using System.ComponentModel.Design;
using DeviceManager.Entries.Devices;
using Microsoft.Data.SqlClient;

namespace Device.BusinessLogic.Services;

public class ServiceMethod : IServiceMethods
{
    private readonly string connectionString;

    public ServiceMethod(String connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<PersonalComputer> GetAllPersonalComputers()
    {
        List<PersonalComputer> PersonalComputers = [];

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
                    
                        var personalComputer = new PersonalComputer
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            IsTurnedOn = reader.GetBoolean(2),
                            OperatingSystem = reader.GetString(3)
                        };
                        PersonalComputers.Add(personalComputer);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }

        return PersonalComputers;
    }

    public PersonalComputer GetPersonalComputerID(string id)
    {
        PersonalComputer personalComputer = null;
        String query = @"SELECT d.ID, d.Name , d.IsEnabled, pc.OperatingSystem
           FROM Device d
           INNER JOIN PersonalComputer pc ON d.Id = pc.DeviceId
           WHERE d.Id = @id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue(@id, id);
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
                            Id = reader.GetInt32(0),
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

    public bool AddPersonalComputer(PersonalComputer personalComputer)
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

            using (SqlCommand command1 = new SqlCommand(InsertQueryDevice, connection))
            {
                command1.Parameters.AddWithValue("@Name", personalComputer.Name);
                command1.Parameters.AddWithValue("@IsEnabled", personalComputer.IsTurnedOn);

                int deviceId = (int)command1.ExecuteScalar();
                personalComputer.Id = deviceId;

                using (SqlCommand command2 = new SqlCommand(InsertQueryPC, connection))
                {
                    command2.Parameters.AddWithValue("@OperatingSystem", personalComputer.OperatingSystem);
                    command2.Parameters.AddWithValue("@DeviceId", deviceId);

                    command2.ExecuteNonQuery();
                }
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

    public IEnumerable<SmartWatch> GetSmartWatches(string id)
    {
        List<SmartWatch> smartWatches = [];

        string query = @"SELECT d.ID, d.Name, d.IsEnable, sw.Batterylif
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
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IsTurnedOn = reader.GetBoolean(2),
                            BatteryPercentageFunctionality = new BatteryPercentageFunctionality(reader.GetInt32(3))
                        }
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

    public SmartWatch GetSmartWatch(string id)
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
                            Id = reader.GetInt32(0),
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

                command2.Parameters.AddWithValue("@Id", smartWatch.Id);
                command2.Parameters.AddWithValue("@BatteryLife", smartWatch.BatteryPercentageFunctionality.GetBatteryLevel());

                command1.ExecuteNonQuery();
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

    public IEnumerable<EmbeddedDevice> GetEmbeddedDevices()
    {
        List<EmbeddedDevice> embeddedDevices = [];
        
        string query =@"SELECT d.ID, d.Name, d.IsEnabled, ed.NetworkName, ed.IPAddress
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
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IsTurnedOn = reader.GetBoolean(2),
                            NetworkName = reader.GetString(3),
                            IPAddress = reader.GetString(4)
                        };
                    }
                }
            }
        }
        
    }




}
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
                    {
                        var personalComputer = new PersonalComputer
                        {
                            Id = reader.GetInt32(0),
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

    public PersonalComputer GetPersonalComputerID(String id)
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
        INSERT INTO Device (Id, Name, IsEnabled)
        VALUES (@Id, @Name, @IsEnabled)";

        string InsertQueryPC = @"
        INSERT INTO PersonakComputer (Id, OperatingSystem)
        VALUES (@Id, @OperatingSystem)";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            {
                var command1 = new SqlCommand(InsertQueryDevice, connection);
                var command2 = new SqlCommand(InsertQueryPC, connection);

                command1.Parameters.AddWithValue("@Id", personalComputer.Id);
                command1.Parameters.AddWithValue("@Name", personalComputer.Name);
                command1.Parameters.AddWithValue("@IsEnabled", personalComputer.IsTurnedOn);

                command2.Parameters.AddWithValue("@Id", personalComputer.Id);
                command2.Parameters.AddWithValue("@OperatingSystem", personalComputer.OperatingSystem);

                command1.ExecuteNonQuery();
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

    public IEnumerable<SmartWatch> GetSmartWatches(string id)
    {
        List<SmartWatch> smaertWatches = [];

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
                    }
                }
            }
            
        }


}




}
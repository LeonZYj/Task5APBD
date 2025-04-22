using DeviceManager.Entries.Devices;

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

        string query = "SELECT * FROM PersonalComputers";

        using (Sqlconnection connection = new Sqlconnection(connectionString))
        {
            Sqlcommand command = new Sqlcommand(query, connection);

            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var personalComputer = new PersonalComputer
                        (
                            Id = reader.GetInt32(0),
                            Name = reader.GetInt32(1),
                            IsTurnedOn = reader.GetBoolean(2),
                            OperatingSystem = reader.getString(3)
                        );
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
        String query = "SELECT * FROM PersonalComputers WHERE ID = @ID";

        using (Sqlconnection connection = new Sqlconnection(connectionString))
        {
            var command = new Sqlcommand(query, connection);
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
                        (
                            Id = reader.GetInt32(0),
                            Name = reader.GetInt32(1),
                            IsTurnedOn = reader.GetBoolean(2),
                            OperatingSystem = reader.getString(3)
                        );
                    }
                }
            }
            finally
            {
                reader.close();
            }
        }
        return personalComputer;
    }
    
    
    
}
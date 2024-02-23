using System;
using System.Data.SqlClient;
using Toasted.Logic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Toasted.Data
{
    public class SqlRepository : IRepository
    {
        // Fields

        private readonly string _connectionString;
        private readonly ILogger<SqlRepository> _logger;

        // Constructors
        public SqlRepository(string connectionString, ILogger<SqlRepository> logger)
        {
            this._connectionString = connectionString;
            this._logger = logger;
        }

        // Methods

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);

            await connection.OpenAsync();
            string cmdText = "SELECT * FROM [dbo].[User];";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            Console.WriteLine("Reader Executed...");

            List<User> users = new List<User>;

            while (await reader.ReadAsync())
            {
                int userId = (int)reader["userID"];
                string username = reader["username"].ToString() ?? "";
                string email = reader["Email"].ToString() ?? "";
                int location = (int)reader["location"];
                string firstName = reader.["firstName"].ToString ?? "";
                string lastName = reader.["lastName"].ToString ?? "";
                string password = reader.["password"].ToString ?? "";
                char tempUnit = reader.["tempUnit"].ToString()[0];
                string countryCode = reader.["countryCode"].ToString ?? "";

                users.Add(new User(userId, username, email, location, firstName, lastName, password, tempUnit, countryCode));
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllUsersAsync, returned {0} results.", users.Count);
            return users;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            connection.OpenAsync();

            string cmdText = "SELECT * FROM [dbo].[User] WHERE username = @username;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@username", username);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            User tmpUser = new User();

            while (await reader.ReadAsync())
            {
                int userId = (int)reader["userID"];
                string dbUsername = reader["username"].ToString() ?? "";
                string email = reader["Email"].ToString() ?? "";
                int location = (int)reader["location"];
                string firstName = reader.["firstName"].ToString ?? "";
                string lastName = reader.["lastName"].ToString ?? "";
                string password = reader.["password"].ToString ?? "";
                char tempUnit = reader.["tempUnit"].ToString()[0];
                string countryCode = reader.["countryCode"].ToString ?? "";

                tmpUser = new User(userId, dbUsername, email, location, firstName, lastName, password, tempUnit, countryCode);
            }
            connection.CloseAsync();
            return tmpUser;
        }

        public async Task AddUserAsync(User user)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            string cmdText = "INSERT INTO [dbo].[User] (username, email, location, firstName, lastName, password, tempUnit, countryCode) " +
                             "VALUES (@username, @email, @location, @firstName, @lastName, @password, @tempUnit, @countryCode);";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@username", user.username);
            cmd.Parameters.AddWithValue("@email", user.email);
            cmd.Parameters.AddWithValue("@location", user.location);
            cmd.Parameters.AddWithValue("@firstName", user.firstName);
            cmd.Parameters.AddWithValue("@lastName", user.lastName);
            cmd.Parameters.AddWithValue("@password", user.password);
            cmd.Parameters.AddWithValue("@tempUnit", user.tempUnit);
            cmd.Parameters.AddWithValue("@countryCode", user.countryCode);

            await cmd.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

            List<User> users = new List<User>();

            while (await reader.ReadAsync())
            {
                int userId = (int)reader["userID"];
                string username = reader["username"].ToString() ?? "";
                string email = reader["Email"].ToString() ?? "";
                string location = (string)reader["location"];
                string firstName = reader["firstName"].ToString() ?? "";
                string lastName = reader["lastName"].ToString() ?? "";
                string password = reader["password"].ToString() ?? "";
                char tempUnit = reader["tempUnit"].ToString()[0];
                string countryCode = reader["countryCode"].ToString() ?? "";

                users.Add(new User(userId, username, email, JsonConvert.DeserializeObject<Location>(location), firstName, lastName, password, tempUnit, countryCode));
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllUsersAsync, returned {0} results.", users.Count);
            return users;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
           await connection.OpenAsync();

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
                string location = (string)reader["location"];
                string firstName = reader["firstName"].ToString() ?? "";
                string lastName = reader["lastName"].ToString() ?? "";
                string password = reader["password"].ToString() ?? "";
                char tempUnit = reader["tempUnit"].ToString()[0];
                string countryCode = reader["countryCode"].ToString() ?? "";

                tmpUser = new User(userId, dbUsername, email, JsonConvert.DeserializeObject<Location>(location), firstName, lastName, password, tempUnit, countryCode); //de-jsonify LOCATION 
            }
           await connection.CloseAsync();
            return tmpUser;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            bool success = false;


            using SqlConnection connection = new SqlConnection(this._connectionString);
            {
                await connection.OpenAsync();

                string cmdText = "INSERT INTO [dbo].[User] (username, email, location, firstName, lastName, password, tempUnit, countryCode) " +
                                 "VALUES (@username, @email, @location, @firstName, @lastName, @password, @tempUnit, @countryCode);";

                using SqlCommand cmd = new SqlCommand(cmdText, connection);
                {
                    cmd.Parameters.AddWithValue("@username", user.username);
                    cmd.Parameters.AddWithValue("@email", user.email);
                    cmd.Parameters.AddWithValue("@location", user.location.JsonThis()); //always send location as JSON to database
                    cmd.Parameters.AddWithValue("@firstName", user.firstName);
                    cmd.Parameters.AddWithValue("@lastName", user.lastName);
                    cmd.Parameters.AddWithValue("@password", user.password);
                     cmd.Parameters.AddWithValue("@tempUnit", user.tempUnit);
                    cmd.Parameters.AddWithValue("@countryCode", user.countryCode);

                  //  await cmd.ExecuteNonQueryAsync();
                    // If rowsAffected is greater than 0, the insertion was successful
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    success = rowsAffected > 0; /*if >0, this expression will be valid and this success will be true.
                                                **Pretty cool way to avoid having to type out a whole 'if statement'.
                                                **Although, this comment is so long that it would have been quicker for me
                                                **to just type out the if statement instead of using this trick then writing out
                                                **a long comment to explain it. On top of that, explaining the fact that writing
                                                **this comment took a long time further increases the amount of time it has taken
                                                **to write this expression. I should probably just stop commenting here.*/
                }
                await connection.CloseAsync();
            }

            return success;
        }




        public async Task<bool> UpdatePasswordAsync(string username, string encryptedPassword) //pass username and new password to update password
        {
            bool success = false;
            using SqlConnection connection = new SqlConnection(this._connectionString);
            {
                await connection.OpenAsync();
                string sql = "Update [dbo].[User] " +
                             "SET password = @password " +
                              "WHERE Username = @username";
                using SqlCommand cmd = new SqlCommand(sql, connection);
                {
                    cmd.Parameters.AddWithValue("@username",username);
                    cmd.Parameters.AddWithValue("@password", encryptedPassword);
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    success = rowsAffected > 0;
                }
            }
            return success;
        }

        public async Task<bool> UpdateTempUnitAsync(string username, char tempUnit)
        {

            bool success = false;
            using SqlConnection connection = new SqlConnection(this._connectionString);
            {
                await connection.OpenAsync();
                string sql = "Update [dbo].[User] " +
                             "SET tempUnit = @tempUnit " +
                              "WHERE Username = @username";
                using SqlCommand cmd = new SqlCommand(sql, connection);
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@tempUnit", tempUnit);
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    success = rowsAffected > 0;
                }
            }
            return success;
        }

        public async Task<bool> UpdateLocationAsync(string username, string locationJSON)
        {
            bool success = false;
            using SqlConnection connection = new SqlConnection(this._connectionString);
            {
                await connection.OpenAsync();
                string sql = "Update [dbo].[User] " +
                             "SET location = @locationJSON " +
                              "WHERE Username = @username";
                using SqlCommand cmd = new SqlCommand(sql, connection);
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@locationJSON", locationJSON);
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    success = rowsAffected > 0;
                }
            }
            return success;
        }


        public async Task<bool> checkEmailExistsAsync(string email)
        {
            bool exists = false;
            using SqlConnection connection= new SqlConnection(this._connectionString);
            {
                await connection.OpenAsync();
                string sql = "Select * FROM [dbo].[User] " +
                    "WHERE email = @email";
                using SqlCommand cmd = new SqlCommand(sql, connection);
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    if (await cmd.ExecuteScalarAsync() != null)
                    {
                        exists = true;
                    }
                }
            }
            return exists;
        }







        /*  Task<bool> IRepository.AddUserAsync(User user)
          {
              throw new NotImplementedException();
          }

          */
    }
}

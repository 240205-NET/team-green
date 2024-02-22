﻿using System;
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

            Console.WriteLine("Reader Executed...")

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

                users.Add(new User(userId, username, email, location, firstName, lastName, password, tempUnit));
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllUsersAsync, returned {0} results.", users.Count);
            return users;
        }
    }
}
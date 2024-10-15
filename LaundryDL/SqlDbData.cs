using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using LaundryModel;


namespace LaundryDL
{
    public class SqlDbData
    {
        static string connectionString = "Data Source=DESKTOP-TC94IK2\\SQLEXPRESS01; Initial Catalog=Laundry; Integrated Security=True;";
        SqlConnection sqlConnection;

        public SqlDbData()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public List<User> GetUsers()
        {
            string selectStatement = "SELECT * FROM users";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            List<User> users = new List<User>();

            while (reader.Read())
            {
                string name = reader["name"].ToString();
                string clWeight = reader["clWeight"].ToString();
                string status = reader["status"].ToString();

                User readUser = new User();
                readUser.name = name;
                readUser.clWeight = clWeight;
                readUser.status = status;

                users.Add(readUser);
            }

            sqlConnection.Close();

            return users;
        }

        public int AddUser(string name, string clWeight, string status)
        {
            int success;

            string insertStatement = "INSERT INTO users VALUES (@name, @clWeight, @status)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@name", name);
            insertCommand.Parameters.AddWithValue("@clWeight", clWeight);
            insertCommand.Parameters.AddWithValue("@status", status);
            sqlConnection.Open();

            success = insertCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return success;

        }

        public int UpdateUser(string name, string clWeight, string status)
        {
            int success;

            string updateStatement = $"UPDATE users SET clWeight = @clWeight WHERE name = @name";
            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            sqlConnection.Open();

            updateCommand.Parameters.AddWithValue("@name", name);
            updateCommand.Parameters.AddWithValue("@clWeight", clWeight);

            success = updateCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return success;

        }

        public int DeleteUser(string name)
        {
            int success;

            string deleteStatement = $"DELETE FROM users WHERE name = @name";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
            sqlConnection.Open();

            deleteCommand.Parameters.AddWithValue("@name", name);

            success = deleteCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return success;
        }

    }
}
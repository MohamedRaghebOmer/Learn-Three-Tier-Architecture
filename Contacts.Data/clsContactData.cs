using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using Contacts.Data.Settings;
using Microsoft.SqlServer.Server;

namespace Contacts.Data
{
    public class clsContactData
    {
        public static bool GetContactInfoByID(int ID, ref string firstName,
            ref string lastName, ref string email, ref string phone,
            ref string address, ref DateTime dateOfBirth, ref int countryID,
            ref string imagePath)
        {
            bool isFound = false; // to be able to close connction before returning the value
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(clsDataSettings.connectionString);
                SqlCommand command = new SqlCommand("SELECT * FROM Contacts WHERE ContactID = @ID", connection);
                command.Parameters.AddWithValue("@ID", ID);

                connection.Open();
                reader = command.ExecuteReader();

                // (FirstName, LastName, Email, Phone, Address, DateOfBirth and CountryId),
                // all of them don't allow NULL in database, so the validation is only on 'ImagePath'.
                if (reader.Read())
                {
                    firstName = (string)reader["FirstName"];
                    lastName = (string)reader["LastName"];
                    email = (string)reader["Email"];
                    phone = (string)reader["Phone"];
                    address = (string)reader["Address"];
                    dateOfBirth = (DateTime)reader["DateOfBirth"];
                    countryID = (int)reader["CountryID"];
                    // null validation
                    imagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";

                    isFound = true;
                }
            }
            catch (Exception)
            {
                // We can log the error or do something else...
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return isFound;
        }

        public static int AddNewContact(string firstName, string lastName,
            string email, string phone, string address,
            DateTime dateOfBirth, int countryID, string imagePath)
        {
            int id = -1;
            SqlConnection connection = null;
            SqlCommand command = null;
            string query = @"INSERT INTO Contacts(FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath)
                            VALUES
                            (@firstName, @lastName, @email, @phone, @address, @dateOfBirth, @countryID, @imagePath);
                            SELECT SCOPE_IDENTITY();";

            try
            {
                connection = new SqlConnection(clsDataSettings.connectionString);
                connection.Open();
                command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                command.Parameters.AddWithValue("@countryID", countryID);

                if (imagePath != "")
                    command.Parameters.AddWithValue("@imagePath", imagePath);
                else
                    command.Parameters.AddWithValue("imagePath", System.DBNull.Value);

                    object result = command.ExecuteScalar();
                if (result != null)
                    id = Convert.ToInt32(result);
            }
            catch (Exception) { } 
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            
            return id;
        }


    }
}

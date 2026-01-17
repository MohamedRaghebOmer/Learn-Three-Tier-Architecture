using System;
using System.Data;
using System.Data.SqlClient;
using Contacts.Data.Settings;

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
                if (!string.IsNullOrEmpty(imagePath)) // nullable value
                    command.Parameters.AddWithValue("@imagePath", imagePath);
                else
                    command.Parameters.AddWithValue("@imagePath", DBNull.Value);

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

        public static bool UpdateContact(int id, string firstName, string lastName,
            string email, string phone, string address,
            DateTime dateOfBirth, int countryID, string imagePath)
        {
            int rowsAffected = 0;
            SqlConnection connection = null;
            SqlCommand command = null;
            string query = @"UPDATE Contacts
                            SET
                            FirstName = @firstName,
                            LastName = @lastName,
                            Email = @email,
                            Phone = @phone,
                            Address = @address,
                            DateOfBirth = @dateOfBirth,
                            CountryID = @countryID,
                            ImagePath = @imagePath
                            WHERE ContactID = @id";

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
                command.Parameters.AddWithValue("@id", id);
                if (!string.IsNullOrEmpty(imagePath)) // nullable value
                    command.Parameters.AddWithValue("@imagePath", imagePath);
                else
                    command.Parameters.AddWithValue("@imagePath", DBNull.Value);

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return rowsAffected > 0;
        }

        public static bool DeleteContact(int id)
        {
            int rowsAffected = 0;
            SqlConnection connection = null;
            SqlCommand command = null;
            string query = @"DELETE FROM Contacts
                            WHERE ContactID = @id";

            try
            {
                connection = new SqlConnection(clsDataSettings.connectionString);
                connection.Open();

                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return rowsAffected > 0;
        }

        public static DataTable GetAllContacts()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM Contacts;";

            try
            {
                connection = new SqlConnection(clsDataSettings.connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
            }
            catch (Exception) { }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return dataTable;
        }

        public static bool IsContactExist(int id)
        {
            bool exist = false;
            SqlConnection connection = null;
            string query = @"SELECT 1
                            FROM Contacts
                            WHERE Contacts.ContactID = @id;";
            try
            {
                connection = new SqlConnection(clsDataSettings.connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                
                object result = command.ExecuteScalar();

                if (result != null)
                    exist = true;
            }
            catch (Exception) { }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return exist;
        }
    }

    public class clsCountriesData
    {
        public static bool GetCountryInfoByID(int id, ref string countryName, ref string code, ref string phoneCode)
        {
            bool isFound = false;
            SqlConnection connection = null;
            SqlDataReader reader = null;
            string query = @"SELECT *
                            FROM Countries
                            WHERE CountryID = @id;";
            try
            {
                connection = new SqlConnection(clsDataSettings.connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    countryName = reader["CountryName"] == DBNull.Value ? "" : reader["CountryName"].ToString();
                    code = reader["Code"] == DBNull.Value ? "" : reader["Code"].ToString();
                    phoneCode = reader["PhoneCode"] == DBNull.Value ? "" : reader["PhoneCode"].ToString();
                    isFound = true;
                }
            }
            catch (Exception) { }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return isFound;
        }
        
        public static int AddNewCountry(string countryName, string code, string phoneCode)
        {
            int newCountryId = -1;
            SqlConnection connection = null;
            string query = @"INSERT INTO Countries (CountryName, Code, PhoneCode)
                VALUES (@countryName, @code, @phoneCode);
                SELECT SCOPE_IDENTITY();";

            try
            {
                connection = new SqlConnection(clsDataSettings.connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@countryName", countryName);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@phoneCode", phoneCode);

                object result = command.ExecuteScalar();
                newCountryId = result != null ? Convert.ToInt32(result) : -1;
            }
            catch (Exception) { }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return newCountryId;
        }

        public static bool UpdateCountry(int id, string countryName, string code, string phoneCode)
        {
            int rowsAffected = 0;
            SqlConnection connection = null;
            string query = @"UPDATE Countries
                            SET
                            CountryName = @countryName, Code = @code, PhoneCode = @phoneCode
                            WHERE Countries.CountryID = @id;";

            try
            {
                connection = new SqlConnection(clsDataSettings.connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@countryName", countryName);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@phoneCode", phoneCode);
                command.Parameters.AddWithValue("@id", id);

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}
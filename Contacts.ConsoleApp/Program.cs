using System;
using System.Data;
using Contacts.Business;

namespace Contacts.ConsoleApp
{
    /*
    internal class clsTestContact
    {
        static void TestFind(int id)
        {
            clsContact contact = clsContact.Find(id);

            if (contact != null)
            {
                Console.WriteLine("FullName   : " + contact.FirstName + " " + contact.LastName);
                Console.WriteLine("Email      : " + contact.Email);
                Console.WriteLine("Phone      : " + contact.Phone);
                Console.WriteLine("Address    : " + contact.Address);
                Console.WriteLine("DateOfBirth: " + contact.DateOfBirth.ToString());
                Console.WriteLine("CountryID  : " + contact.CountryID);
                Console.WriteLine("ImagePath  : " + contact.ImagePath);
            }
            else
            {
                Console.WriteLine("Contact [" + id + "] Not Found!");
            }
        }

        static void TestAddNew()
        {
            clsContact contact = new clsContact();

            contact.FirstName = "Mohamed";
            contact.LastName = "Ragheb";
            contact.Email = "mohamedragheb@gmail.com";
            contact.Phone = "+201001451925";
            contact.Address = "kafer-saad";
            contact.DateOfBirth = new DateTime(2008, 1, 1);
            contact.ImagePath = @"C:\Images\MohamedRagheb";
            contact.CountryID = 1;

            if (contact.Save())
            {
                Console.WriteLine($"Contact add successfully with id = {contact.ID}");
            }
            else
            {
                Console.WriteLine("Saving is faild");
            }
        }
        
        static void TestUpdate(int id)
        {
            clsContact contact = clsContact.Find(id);

            contact.FirstName = "Mazen";
            contact.LastName = "Ragheb";
            contact.Email = "mazen@example.com";
            contact.Address = "kafer-saad";
            contact.DateOfBirth = new DateTime(2017, 7, 10);
            contact.ImagePath = @"C:\Photos\MazenRagheb";

            if (contact.Save())
                Console.WriteLine("Contact updated successflly");
            else
                Console.WriteLine("update faild");
        }

        static void TestDelete(int id)
        {
            if (clsContact.DeleteContact(id))
                Console.WriteLine("Contact deleted successflly");
            else
                Console.WriteLine("Delete faild");
        }

        static void TestGetAllContacts()
        {
            DataTable dataTable = clsContact.GetAllContacts();

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine($"{row["ContactID"]}. {row["FirstName"]} {row["LastName"]}");
                }
            }
        }

        static void TestContactExist(int id)
        {
            if (clsContact.IsContactExist(id))
                Console.WriteLine("Contact Existed");
            else
                Console.WriteLine("Contact NOT Existed");
        }


        static void Main(string[] args)
        {
            //TestFind(4);
            //TestAddNew();
            //TestUpdate(2);
            //TestDelete(5);
            //TestGetAllContacts();
            //TestContactExist(3);
        }
    }
    */

    internal class clsTestCountries
    {
        static void TestFind(int id)
        {
            clsCountry country = clsCountry.Find(id);

            if (country != null)
            {
                Console.WriteLine($"Country ID: {country.CountryID}");
                Console.WriteLine($"Country Name: {country.CountryName}");
                Console.WriteLine($"Country Code: {country.Code}");
                Console.WriteLine($"Country Phone Code: {country.PhoneCode}");
            }
            else
                Console.WriteLine($"Country with id = {id} is NOT found");
        }

        static void TestAddNew()
        {
            clsCountry country = new clsCountry();

            country.CountryName = "Egypt";
            country.Code = "EGY";
            country.PhoneCode = "+20";

            if (country.Save())
                Console.WriteLine("Country add successflly");
            else
                Console.WriteLine("adding failed");
        }

        static void TestUpdate()
        {
            clsCountry country = clsCountry.Find(1);

            if (country != null)
            {
                country.Code = "USA";
                country.PhoneCode = "+1";

                if (country.Save())
                    Console.WriteLine("Country updated successfully");
                else
                    Console.WriteLine("Update failed");
            }
            else
            {
                Console.WriteLine("Country with ID 1 not found");
            }

        }

        static void Main(string[] arg)
        {
            //TestFind(3);
            //TestAddNew();
            TestUpdate();
        }
    }
}

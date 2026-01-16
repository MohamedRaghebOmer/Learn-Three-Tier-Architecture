using System;
using Contacts.Business;

namespace Contacts.ConsoleApp
{
    internal class Program
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

        static void Main(string[] args)
        {
            //TestFind(4);
            TestAddNew();
        }
    }
}

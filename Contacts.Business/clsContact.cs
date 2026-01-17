using System;
using System.Data;
using Contacts.Data;

namespace Contacts.Business
{
    public class clsContact
    {
        private enum enMode { AddNew, Update };
        private enMode _Mode = enMode.AddNew;

        public int ID { get; set; } // ID is read only property, because it's an (outo number) emplemented.
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set;}
        public int CountryID { get; set; }

        private bool _AddNewContact()
        {
            this.ID = Contacts.Data.clsContactData.AddNewContact(this.FirstName, this.LastName, this.Email, this.Phone, this.Address,
                this.DateOfBirth, this.CountryID, this.ImagePath);

            return (this.ID != -1);
        }

        private bool _UpdateContact()
        {
            return Contacts.Data.clsContactData.UpdateContact(this.ID, this.FirstName, this.LastName, this.Email, this.Phone, this.Address,
                this.DateOfBirth, this.CountryID, this.ImagePath);
        }

        public clsContact()
        {
            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.ImagePath = "";
            this.CountryID = -1;
            this._Mode = enMode.AddNew;
        }

        private clsContact(int id, string firstName, string lastName, string email, string phone,
            string address, DateTime dateOfBirth, int countryId, string imagePath)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
            this.Address = address;
            this.DateOfBirth = dateOfBirth;
            this.CountryID = countryId;
            this.ImagePath = imagePath;
            this._Mode = enMode.Update;
        }

        public static clsContact Find(int id)
        {
            string firstName = "", lastName = "", email = "", phone = "", address = "", imagePath = "";
            DateTime dateOfBirth = default;
            int countryId = -1;

            if (clsContactData.GetContactInfoByID(id, ref firstName, ref lastName, ref email, ref phone,
                ref address, ref dateOfBirth, ref countryId, ref imagePath))
                return new clsContact(id, firstName, lastName, email, phone, address, dateOfBirth, countryId, imagePath);
            else
                return null;
        }

        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                 case enMode.Update:
                    return _UpdateContact();
            }

            return false;
        }

        public static bool DeleteContact(int id)
        {
            return Contacts.Data.clsContactData.DeleteContact(id);
        }

        public static DataTable GetAllContacts()
        {
            return clsContactData.GetAllContacts();
        }

        public static bool IsContactExist(int id)
        {
            return clsContactData.IsContactExist(id);
        }
    }

    public class clsCountry
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode = enMode.AddNew;

        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }

        private bool _AddNewCountry()
        {
            this.CountryID = clsCountriesData.AddNewCountry(this.CountryName, this.Code, this.PhoneCode);
            return this.CountryID != -1;
        }

        private bool _Update()
        {
            return clsCountriesData.UpdateCountry(this.CountryID, this.CountryName, this.Code, this.PhoneCode);
        }

        private clsCountry(int id, string countryName, string code, string phoneCode)
        {
            this.CountryID = id;
            this.CountryName = countryName;
            this.Code = code;
            this.PhoneCode = phoneCode;
            this._Mode = enMode.Update;
        }

        public clsCountry()
        {
            this.CountryID = -1;
            this.CountryName = string.Empty;
            this.Code = string.Empty;
            this.PhoneCode = string.Empty;
            this._Mode = enMode.AddNew;
        }

        public static clsCountry Find(int id)
        {
            string countryName = "", code = "", phoneCode = "";

            if (clsCountriesData.GetCountryInfoByID(id, ref countryName, ref code, ref phoneCode))
                return new clsCountry(id, countryName, code, phoneCode);
            else
                return null;
        }

        public bool Save()
        {
            switch(this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                
                case enMode.Update:
                   return _Update();
            }
            
            return false;
        }
    }

}

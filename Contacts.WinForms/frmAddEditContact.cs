using Contacts.Business;
using Contacts.WinForms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Contacts.WinForms
{
    public partial class frmAddEditContact : Form
    {
        private enum enMode { AddNewMode, UpdateMode };
        private enMode _Mode;
        
        private int _contactID;
        private clsContact _contact;

        public frmAddEditContact(int contactID)
        {
            InitializeComponent();
            this._contactID = contactID;

            if (this._contactID == -1)
                this._Mode = enMode.AddNewMode;
            else
                this._Mode = enMode.UpdateMode;
        }

        private void frmAddNewContact_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _LoadData()
        {
            _FillCountriesInComboBox();
            cbCountries.SelectedIndex = 0;

            if (pictureBox1.ImageLocation == null)
                lblRemove.Visible = false;

            if (this._Mode == enMode.AddNewMode)
            {
                // lblMode.Text = "Add New Contact";
                _contact = new clsContact();
                return;
            }

            _contact = clsContact.Find(_contactID);

            if (_contact == null)
            {
                MessageBox.Show("This form will be closed because there is no contact id with ID = " + _contact);
                this.Close();
                return;
            }

            lblMode.Text = "Edit Contact ID With ID = " + _contactID;
            lblID.Text = _contact.ID.ToString();
            txtFirstName .Text = _contact.FirstName;
            txtLastName.Text = _contact.LastName;
            txtEmail.Text = _contact.Email;
            txtPhone.Text = _contact.Phone;
            txtAddress.Text = _contact.Address;
            dtpDateOfBirth.Value = _contact.DateOfBirth;
            cbCountries.Text = clsCountry.Find(_contact.CountryID).CountryName;
            
            if (!string.IsNullOrEmpty(_contact.ImagePath))
                pictureBox1.ImageLocation = _contact.ImagePath;
        }

        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog image = new OpenFileDialog();

            image.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
            image.Title = "Select a Contact Image";

            if (image.ShowDialog() == DialogResult.OK)
                pictureBox1.ImageLocation = image.FileName;
        }

        private void lblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.Image = Resources.person_man;
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            frmListContacts listContacts = new frmListContacts();
            listContacts.Refresh();
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            int countryID = clsCountry.Find(cbCountries.Text).CountryID;
            string imagePath = this.pictureBox1.ImageLocation;

            _contact.FirstName = this.txtFirstName.Text.ToString();
            _contact.LastName = this.txtLastName.Text.ToString();
            _contact.Email = this.txtEmail.Text.ToString();
            _contact.Phone = this.txtPhone.Text.ToString();
            _contact.Address = this.txtPhone.Text.ToString();
            _contact.DateOfBirth = this.dtpDateOfBirth.Value;
            _contact.CountryID = countryID;
            _contact.ImagePath = string.IsNullOrEmpty(imagePath) ? string.Empty : this.pictureBox1.ImageLocation.ToString();

            if (_contact.Save())
                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Can't be Saved.");

            _Mode = enMode.UpdateMode;
            lblMode.Text = "Edit Contact ID With ID = " + _contactID;
            lblID.Text = _contact.ID.ToString();
        }       
    }
}

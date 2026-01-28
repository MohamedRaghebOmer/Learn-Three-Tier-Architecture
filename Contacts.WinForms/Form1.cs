using Contacts.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts.WinForms
{
    public partial class frmListContacts : Form
    {
        public frmListContacts()
        {
            InitializeComponent();
        }

        private void frmListContacts_Load(object sender, EventArgs e)
        {
            _RefreshContactList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddEditContact frm = new frmAddEditContact(-1);
            frm.ShowDialog();
            _RefreshContactList();
        }

        private void _RefreshContactList()
        {
            this.dgvAllContacts.DataSource = Contacts.Business.clsContact.GetAllContacts();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void dgvAllContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditContact frm = new frmAddEditContact(Convert.ToInt32(dgvAllContacts.CurrentRow.Cells[0].Value));
            frm.ShowDialog();

            _RefreshContactList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int contactID = Convert.ToInt32(dgvAllContacts.CurrentRow.Cells[0].Value);

            if (MessageBox.Show("Are you sure you want to delete contact with ID = " + contactID + " ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsContact.DeleteContact(contactID))
                {
                    MessageBox.Show("Contact deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshContactList();
                }
                else
                {
                    MessageBox.Show("Failed to delete contact.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

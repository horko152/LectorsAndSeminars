using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{
    public partial class Form2 : Form
    {
        Project1Entities project1 = new Project1Entities();
        public Form2()
        {
            InitializeComponent();
           
        }
        public void ListSeminarItems(string LektorName)
        {
            var lek = project1.Lektors;
            listView1.Items.Clear();
            foreach (var item in lek)
            {
                if (txtLektorName.Text == item.LektorName)
                {
                    var semlek = project1.SeminarsAndLektors;
                    var sem = project1.Seminars;
                    foreach (var itemn in semlek)
                    {
                        foreach (var itm in sem)
                        {
                            if (item.LektorName == itemn.LektorsName && itemn.SeminarsId == itm.Id)
                            {
                                listView1.Items.Add(itm.SeminarName);
                            }
                        }

                    }
                }
            }
        }
        public Form2(string yourselecteditem)
        {
            InitializeComponent();
            var lek = project1.Lektors;
            foreach (var item in lek)
            {
                if (yourselecteditem == item.LektorName)
                {
                    txtLektorName.Text = item.LektorName;
                    txtAge.Text = item.Age.ToString();
                    txtSex.Text = item.Sex;
                    txtPhone.Text = item.Phone;
                    txtAddress.Text = item.Address;
                }
            }
            ListSeminarItems(txtLektorName.Text);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                txtLektorName.Focus();
                Lektors l = new Lektors
                {
                    LektorName = txtLektorName.Text,
                    Age = Convert.ToInt32(txtAge.Text),
                    Sex = txtSex.Text,
                    Address = txtAddress.Text,
                    Phone = txtPhone.Text
                };
                project1.Lektors.Add(l);
                lektorsBindingSource.Add(l);
                lektorsBindingSource.MoveLast();
                txtLektorName.Clear();
                txtAge.Clear();
                txtSex.Clear();
                txtAddress.Clear();
                txtPhone.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            txtLektorName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lektorsBindingSource.EndEdit();
                project1.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lektorsBindingSource.ResetBindings(false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            lektorsBindingSource.ResetBindings(false);
            foreach(DbEntityEntry entry in project1.ChangeTracker.Entries())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }

        }

        private void btnAddS_Click(object sender, EventArgs e)
        {

        }
        private void btnRemoveS_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            project1 = new Project1Entities();
            lektorsBindingSource.DataSource = project1.Lektors.ToList();
            seminarsBindingSource.DataSource = project1.Seminars.ToList();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(string.IsNullOrEmpty(txtSearch.Text))
                {
                    dataGridView1.DataSource = lektorsBindingSource;
                }
                else
                {
                    var query = from o in lektorsBindingSource.DataSource as List<Lektors>
                                where o.LektorName.Contains(txtSearch.Text) || o.Age.ToString() == txtSearch.Text || o.Sex == txtSearch.Text || o.Address.Contains(txtSearch.Text) || o.Phone == txtSearch.Text
                                select o;
                    dataGridView1.DataSource = query.ToList();
                }
            }

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if(MessageBox.Show("Ви дійсно хочете видалити запис?","Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    project1.Lektors.Remove(lektorsBindingSource.Current as Lektors);
                    lektorsBindingSource.RemoveCurrent();
                }
            }
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            txtLektorName.Text = dr.Cells[0].Value.ToString();
            txtAge.Text = dr.Cells[1].Value.ToString();
            txtSex.Text = dr.Cells[2].Value.ToString();
            txtAddress.Text = dr.Cells[3].Value.ToString();
            txtPhone.Text = dr.Cells[4].Value.ToString();
            ListSeminarItems(txtLektorName.Text);
        }
    }
}

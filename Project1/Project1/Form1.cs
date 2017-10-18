using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{
    public partial class Form1 : Form
    {
        Project1Entities data1 = new Project1Entities();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bindtreeview();
        }
        public void bindtreeview()
        {
            var kor = "Seminars";
            TreeNode rootnode = new TreeNode(kor);
            Project1Entities data1 = new Project1Entities();
            var child = data1.Seminars;
            foreach (var item in child)
            {
                TreeNode childNode = new TreeNode(item.SeminarName);
                var sal = data1.SeminarsAndLektors;
                foreach (var itemn in sal)
                {
                    TreeNode childno = new TreeNode(itemn.LektorsName);
                    if (itemn.SeminarsId == item.Id)
                    {
                        childNode.Nodes.Add(childno);
                    }

                }
                rootnode.Nodes.Add(childNode);
            }
            treeView1.Nodes.Add(rootnode);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView treeView = sender as TreeView;
            ListView list = new ListView();
            Project1Entities entities = new Project1Entities();
            var sem = entities.Seminars;
            var lek = entities.Lektors;
            listView1.Items.Clear();
            if (treeView1.SelectedNode.Text == "Seminars")
            {
                foreach (var item in lek)
                    listView1.Items.Add(item.LektorName);
            }
            foreach (var item in sem)
                {
                var semlek = entities.SeminarsAndLektors;
                if (treeView1.SelectedNode.Text == item.SeminarName)
                foreach (var itemn in semlek)
                    {
                        if (itemn.SeminarsId == item.Id)
                        {
                        listView1.Items.Add(itemn.LektorsName);
                        }
                    }
                }
        }
        public void delete()
        {
            var lek = data1.Lektors;
            if (listView1.SelectedItems.Count != 0)
            {
                foreach (ListViewItem LItem in listView1.SelectedItems)
                {
                    foreach (var item in lek)
                    {
                        if (LItem.Text == item.LektorName)
                        {
                            var confirmation = MessageBox.Show("Дійсно видалити цього лектора?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (confirmation == DialogResult.Yes)
                            {
                                data1.Lektors.Remove(item);
                            }
                        }
                    }
                }
            }
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.ShowDialog();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                foreach (ListViewItem LItem in listView1.SelectedItems)
                {
                    string selecteditem = LItem.Text;
                    Form2 frm2 = new Form2(selecteditem);
                    frm2.ShowDialog(this);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete();
        }

        public void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            var lek = data1.Lektors;
            if (listView1.SelectedItems.Count != 0)
            {
                foreach (ListViewItem LItem in listView1.SelectedItems)
                {
                    if (Keys.Delete == e.KeyCode)
                    {
                        foreach (var item in lek)
                        {
                            if (LItem.Text == item.LektorName)
                            {
                                var confirmation = MessageBox.Show("Дійсно видалити цього лектора?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (confirmation == DialogResult.Yes)
                                {
                                    data1.Lektors.Remove(item);
                                }
                            }
                        }
                        //var confirmation = MessageBox.Show("Дійсно видалити цього лектора?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //if (confirmation == DialogResult.Yes)
                        //{
                        //    data1.Lektors.Remove(item);

                        //}
                    }
                    if (Keys.Enter == e.KeyCode)
                    {
                        string selecteditem = LItem.Text;
                        Form2 frm2 = new Form2(selecteditem);
                        frm2.ShowDialog(this);
                    }
                    if (Keys.Oemplus == e.KeyCode)
                    {
                        Form2 frm = new Form2();
                        frm.ShowDialog();
                    }
                }
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                foreach (ListViewItem LItem in listView1.SelectedItems)
                {
                    string selecteditem = LItem.Text;
                    Form2 frm2 = new Form2(selecteditem);
                    frm2.ShowDialog(this);
                }
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Розроблено Волошенюком І.В.","Про програму");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}


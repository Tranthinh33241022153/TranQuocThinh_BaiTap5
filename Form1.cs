using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TranQuocThinh_BaiTap5.Model;

namespace TranQuocThinh_BaiTap5
{
    public partial class Form1 : Form
    {
        private List<Khoa> khoas = new List<Khoa>();
        public Form1()
        {
            InitializeComponent();
            InitializeData();
            PopulateTreeView();
            listView1.SelectedIndexChanged += ListViewStudents_SelectedIndexChanged;
            treeView1.AfterSelect += TreeViewFaculties_AfterSelect;
        }
        //private void InitializeComponent()
        //{
        //    // Initialize TreeView
        //    treeViewFaculties = new TreeView
        //    {
        //        Location = new System.Drawing.Point(12, 12),
        //        Size = new System.Drawing.Size(200, 400),
        //        Name = "treeViewFaculties"
        //    };
        //    treeViewFaculties.AfterSelect += TreeViewFaculties_AfterSelect;

        //    // Initialize DataGridView
        //    dataGridViewStudents = new DataGridView
        //    {
        //        Location = new System.Drawing.Point(220, 12),
        //        Size = new System.Drawing.Size(400, 200),
        //        Name = "dataGridViewStudents",
        //        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        //        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        //        MultiSelect = false
        //    };
        //    dataGridViewStudents.SelectionChanged += DataGridViewStudents_SelectionChanged;

        //    // Initialize TextBoxes for student details
        //    txtId = new TextBox
        //    {
        //        Location = new System.Drawing.Point(220, 220),
        //        Size = new System.Drawing.Size(100, 20),
        //        ReadOnly = true
        //    };
        //    txtName = new TextBox
        //    {
        //        Location = new System.Drawing.Point(220, 250),
        //        Size = new System.Drawing.Size(200, 20),
        //        ReadOnly = true
        //    };
        //    txtEmail = new TextBox
        //    {
        //        Location = new System.Drawing.Point(220, 280),
        //        Size = new System.Drawing.Size(200, 20),
        //        ReadOnly = true
        //    };

        //    // Add Labels
        //    var lblId = new Label
        //    {
        //        Text = "ID",
        //        Location = new System.Drawing.Point(180, 220),
        //        Size = new System.Drawing.Size(30, 20)
        //    };
        //    var lblName = new Label
        //    {
        //        Text = "Name",
        //        Location = new System.Drawing.Point(180, 250),
        //        Size = new System.Drawing.Size(40, 20)
        //    };
        //    var lblEmail = new Label
        //    {
        //        Text = "Email",
        //        Location = new System.Drawing.Point(180, 280),
        //        Size = new System.Drawing.Size(40, 20)
        //    };

        //    // Add controls to form
        //    Controls.Add(treeViewFaculties);
        //    Controls.Add(dataGridViewStudents);
        //    Controls.Add(txtId);
        //    Controls.Add(txtName);
        //    Controls.Add(txtEmail);
        //    Controls.Add(lblId);
        //    Controls.Add(lblName);
        //    Controls.Add(lblEmail);

        //    // Form properties
        //    Text = "University Management";
        //    Size = new System.Drawing.Size(650, 450);
        //    StartPosition = FormStartPosition.CenterScreen;
        //}

        private void InitializeData()
        {
            khoas = new List<Khoa>();

            // Create Faculty: Khoa CNTT Kinh doanh
            var faculty1 = new Khoa( "Khoa CNTT Kinh doanh");
            faculty1.Lops.Add(new Lop(111, "Class 111-22-2111"));
            faculty1.Lops.Add(new Lop(112, "Class 111-22-2222"));

            // Create Faculty: Khoa Kế toán
            var faculty2 = new Khoa( "Khoa Kế toán");
            faculty2.Lops.Add(new Lop(221, "class 222-33-111"));
            faculty2.Lops.Add(new Lop(222, "class 222-33-222"));
            faculty2.Lops.Add(new Lop(223, "class 222-33-333"));

            // Add students to Class 111-22-2222
            faculty1.Lops[0].Students.AddRange(new[]
            {
                new Student(105, "Full name #10", "email10@ueh.edu.vn"),
                new Student(106, "Full name #11", "email11@ueh.edu.vn"),
                new Student(107, "Full name #12", "email12@ueh.edu.vn"),
                new Student(108, "Full name #13", "email13@ueh.edu.vn"),
                new Student(109, "Full name #14", "email15@ueh.edu.vn")
            });
            // Add students to Class 111-22-2111
            faculty1.Lops[1].Students.AddRange(new[]
            {
                new Student(105, "Full name #5", "email5@ueh.edu.vn"),
                new Student(106, "Full name #6", "email6@ueh.edu.vn"),
                new Student(107, "Full name #7", "email7@ueh.edu.vn"),
                new Student(108, "Full name #8", "email8@ueh.edu.vn"),
                new Student(109, "Full name #9", "email9@ueh.edu.vn")
            });

            // Add khoas to list
            khoas.Add(faculty1);
            khoas.Add(faculty2);
        }
        //phương thức hiện Khoa và lớp lên TreeView
        private void PopulateTreeView()  
        {
            treeView1.Nodes.Clear();
            foreach (var khoa in khoas)
            {
                var khoaNode = treeView1.Nodes.Add(khoa.Name);
                foreach (var cls in khoa.Lops)
                {
                    khoaNode.Nodes.Add(cls.Name).Tag = cls;
                }
            }
            treeView1.ExpandAll();
        }

        private void TreeViewFaculties_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is Lop selectedClass)
            {
                // Clear existing items
                listView1.Items.Clear();

                // Populate ListView with students
                foreach (var student in selectedClass.Students)
                {
                    var item = new ListViewItem(student.Id.ToString());
                    item.SubItems.Add(student.Name);
                    item.SubItems.Add(student.Email);
                    item.Tag = student; // Store student object in Tag
                    listView1.Items.Add(item);
                }

                // Clear student details
                ClearStudentDetails();
            }
        }

        private void ListViewStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var selectedStudent = (Student)listView1.SelectedItems[0].Tag;
                txtID.Text = selectedStudent.Id.ToString();
                txtName.Text = selectedStudent.Name;
                txtEmail.Text = selectedStudent.Email;
            }
            else
            {
                ClearStudentDetails();
            }
        }

        private void ClearStudentDetails()
        {
            txtID.Clear();
            txtName.Clear();
            txtEmail.Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //txtID.Enabled = true;
            txtName.Enabled = true;
            txtEmail.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Student student = new Student(
                txtID.Text == "" ? 0 : int.Parse(txtID.Text),
                txtName.Text,
                txtEmail.Text
                );
            if (listView1.SelectedItems.Count > 0)
            {
                var selectedStudent = (Student)listView1.SelectedItems[0].Tag;
                selectedStudent.Name = student.Name;
                selectedStudent.Email = student.Email;
                // Update ListView
                listView1.SelectedItems[0].SubItems[1].Text = student.Name;
                listView1.SelectedItems[0].SubItems[2].Text = student.Email;
                //refresh the ListView
                listView1.Refresh();

            }
            else
            {
                MessageBox.Show("Please select a student to edit.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var selectedStudent = (Student)listView1.SelectedItems[0].Tag;
                var selectedClass = (Lop)treeView1.SelectedNode.Tag;
                selectedClass.Students.Remove(selectedStudent);
                listView1.Items.Remove(listView1.SelectedItems[0]);
                ClearStudentDetails();
            }
            else
            {
                MessageBox.Show("Please select a student to delete.");
            }
        }
    }
}

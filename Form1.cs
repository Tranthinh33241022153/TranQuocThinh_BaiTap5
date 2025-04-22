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
            
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            if (e.Node.Tag is Lop selectedClass)
            {
                btnAdd.Enabled = true;
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
            else
            {
                btnAdd.Enabled = false;
                ClearStudentDetails();
                listView1.Items.Clear();
            }
        }

        private void ListViewStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
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

      

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
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
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có lớp nào được chọn không
            if (treeView1.SelectedNode?.Tag is Lop selectedClass)
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtID.Text) ||
                    string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin sinh viên.");
                    return;
                }

                // Kiểm tra ID có phải là số và là duy nhất
                if (!int.TryParse(txtID.Text, out int newId))
                {
                    MessageBox.Show("Vui lòng nhập ID hợp lệ.");
                    return;
                }

                // Kiểm tra ID đã tồn tại chưa
                if (selectedClass.Students.Any(s => s.Id == newId))
                {
                    MessageBox.Show("ID sinh viên đã tồn tại.");
                    return;
                }

                // Tạo sinh viên mới
                var newStudent = new Student(newId, txtName.Text, txtEmail.Text);

                // Thêm vào danh sách sinh viên của lớp
                selectedClass.Students.Add(newStudent);

                // Thêm vào ListView
                var item = new ListViewItem(newStudent.Id.ToString());
                item.SubItems.Add(newStudent.Name);
                item.SubItems.Add(newStudent.Email);
                item.Tag = newStudent;
                listView1.Items.Add(item);

                // Xóa các trường nhập liệu
                ClearStudentDetails();

                MessageBox.Show("Thêm sinh viên thành công!");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một lớp trước.");
            }
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
        }
    }
}

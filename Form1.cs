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
        public List<Khoa> khoas;
        public List<Lop> lops;
        public List<Student> students;
        public CreateKhoa createKhoa;

        public Form1()
        {
            InitializeComponent();
            
            
            listView1.SelectedIndexChanged += ListViewStudents_SelectedIndexChanged;
            treeView1.AfterSelect += TreeViewFaculties_AfterSelect;
            khoas = new List<Khoa>();
            lops = new List<Lop>();
            students = new List<Student>();
            createKhoa = new CreateKhoa();
            InitializeData();
            PopulateTreeView();

        }
        

        private void InitializeData()
        {
            khoas = createKhoa.khoas;
            lops = createKhoa.lops;
            students = createKhoa.students;

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
                selectedStudent.Id = student.Id;
                // Update ListView
                listView1.SelectedItems[0].SubItems[1].Text = student.Name;
                listView1.SelectedItems[0].SubItems[2].Text = student.Email;
                listView1.SelectedItems[0].SubItems[0].Text = student.Id.ToString();
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
                try
                {
                    var regex = new System.Text.RegularExpressions.Regex(
                        @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                    if (!regex.IsMatch(txtEmail.Text))
                    {
                        MessageBox.Show("Vui lòng nhập email hợp lệ.");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Vui lòng nhập email hợp lệ.");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranQuocThinh_BaiTap5.Model;

namespace TranQuocThinh_BaiTap5
{
    public class CreateKhoa
    {
        public  List<Lop> lops {  get; set; }
        public List<Student> students {  get; set; }
        public List<Khoa> khoas { get; set; }

        public CreateKhoa()
        {
            lops = new List<Lop>();
            students = new List<Student>();
            khoas = new List<Khoa>();

            Khoa faculty1 = new Khoa("Khoa CNTT Kinh doanh");
            faculty1.Lops.Add(new Lop(111, "Class 111-22-2111"));
            faculty1.Lops.Add(new Lop(112, "Class 111-22-2222"));

            // Create Faculty: Khoa Kế toán
            Khoa faculty2 = new Khoa("Khoa Kế toán");
            faculty2.Lops.Add(new Lop(221, "class 222-33-111"));
            faculty2.Lops.Add(new Lop(222, "class 222-33-111"));
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

            // Add students to class 222-33-111
            faculty2.Lops[0].Students.AddRange(new[]
            {
                new Student(110, "Full name #15", "email15@ueh.edu.vn"),
                new Student(111, "Full name #16", "email16@ueh.edu.vn"),
                new Student(112, "Full name #17", "email17@ueh.edu.vn"),
                new Student(113, "Full name #18", "email18@ueh.edu.vn"),
                new Student(114, "Full name #19", "email19@ueh.edu.vn")
            });
            // Add students to class 222-33-111
            faculty2.Lops[1].Students.AddRange(new[]
            {
                new Student(122, "Full name #22", "emai22@ueh.edu.vn"),
                new Student(133, "Full name #22", "emai2233@ueh.edu.vn"),
                new Student(133, "Full name #744", "emai334@ueh.edu.vn"),
                new Student(155, "Full name #44", "emai433@ueh.edu.vn"),
                new Student(166, "Full name #855", "emai232@ueh.edu.vn")
            });

            // Add students to class 222-33-333
            faculty2.Lops[2].Students.AddRange(new[]
            {
                new Student(122, "Full name #8888", "emai8882@ueh.edu.vn"),
                new Student(133, "Full name #9999", "emai2555@ueh.edu.vn"),
                new Student(133, "Full name #1515", "emai5584@ueh.edu.vn"),
                new Student(155, "Full name #2255", "emai5515@ueh.edu.vn"),
                new Student(166, "Full name #8845", "emai5518@ueh.edu.vn")
            });


            // Add khoas to list
            this.khoas.Add(faculty1);
            this.khoas.Add(faculty2);
        }
       
       
    }
}

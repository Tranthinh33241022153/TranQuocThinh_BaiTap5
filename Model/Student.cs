using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranQuocThinh_BaiTap5
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Student(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
       public Student()
        {
            Id = 0;
            Name = string.Empty;
            Email = string.Empty;
        }
        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Email: {Email}";
        }
    }
}

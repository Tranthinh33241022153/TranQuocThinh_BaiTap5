using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranQuocThinh_BaiTap5.Model
{
    public class Lop
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; }

        public Lop(int id, string name)
        {
            Name = name;
            Students = new List<Student>();
        }
    }
}

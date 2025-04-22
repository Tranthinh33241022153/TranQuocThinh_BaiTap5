using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranQuocThinh_BaiTap5.Model
{
    public class Khoa
    {
        public String Name { get; set; }
        public List<Lop> Lops { get; set; }
        public Khoa(string name)
        {
            Name = name;
            Lops = new List<Lop>();
        }
    }
}

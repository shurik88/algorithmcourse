using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8.Task8_4
{
    public class Task8_4
    {
        public static void Main(string[] args)
        {

            var h1 = Hash(2, "hello");
            var h2 = Hash(3, "hello");
        }
        public static int Hash(int multiple, String s)
        {
            int rv = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                rv = multiple * rv + s[i];
            }
            return rv;
        }
    }
}

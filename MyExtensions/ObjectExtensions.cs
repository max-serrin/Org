using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExtensions
{
    public static class ObjectExtensions
    {
        public static Boolean Exists(this object o)
        {
            return o != null;
        }
    }
}

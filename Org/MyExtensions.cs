using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org
{
    public static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list, Random rnd)
        {
            for (var i = 0; i < list.Count; i++)
                Swap(list, i, rnd.Next(list.Count));
        }

        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}

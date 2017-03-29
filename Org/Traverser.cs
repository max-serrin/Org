using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org
{
    public class Traverser<T> : ITraverser<T>
    {
        private LinkedList<T> traversableList = new LinkedList<T>();
        private LinkedListNode<T> current;

        public Traverser(IEnumerable<T> enumerableList, int size = int.MaxValue)
        {
            foreach (T listItem in enumerableList)
            {
                if (size-- <= 0)
                    break;
                traversableList.AddLast(listItem);
            }
            current = traversableList.First;
        }

        public Traverser(IList<T> list, List<int> indexes, int size = int.MaxValue)
        {
            foreach(int index in indexes)
            {
                if (size-- <= 0)
                    break;
                traversableList.AddLast(list[index]);
            }
            current = traversableList.First;
        }

        public int Count()
        {
            return traversableList.Count;
        }

        public T GetCurrent()
        {
            return current.Value;
        }

        public T MoveNext()
        {
            return (current == traversableList.Last ? current = traversableList.First : current = current.Next).Value;
        }

        public T MovePrevious()
        {
            return (current == traversableList.First ? current = traversableList.Last : current = current.Previous).Value;
        }

        public void Reset()
        {
            current = traversableList.First;
        }
    }
}

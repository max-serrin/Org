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
        private int index;

        public Traverser(IEnumerable<T> enumerableList, int size = int.MaxValue)
        {
            foreach (T listItem in enumerableList)
            {
                if (size-- <= 0)
                    break;
                traversableList.AddLast(listItem);
            }
            if (traversableList.Count == 0)
            {
                index = -1;
            }
            else
            {
                current = traversableList.First;
                index = 0;
            }
        }

        public Traverser(IList<T> list, List<int> indexes, int size = int.MaxValue)
        {
            foreach(int index in indexes)
            {
                if (size-- <= 0)
                    break;
                traversableList.AddLast(list[index]);
            }
            if (traversableList.Count == 0)
            {
                index = -1;
            }
            else
            {
                current = traversableList.First;
                index = 0;
            }
        }

        public int Count { get { return traversableList.Count; } }

        public int Index { get; }

        public T GetCurrent()
        {
            if (traversableList.Count == 0)
                return default(T);
            return current.Value;
        }

        public T GetAtIndex(int i)
        {
            if (traversableList.Count == 0)
                return default(T);
            return traversableList.ElementAt(i);
        }

        public T MoveNext()
        {
            if (traversableList.Count == 0)
                return default(T);
            index++;
            if (index == traversableList.Count)
                index = 0;
            return (current == traversableList.Last ? current = traversableList.First : current = current.Next).Value;
        }

        public T MovePrevious()
        {
            if (traversableList.Count == 0)
                return default(T);
            index--;
            if (index == -1)
                index = traversableList.Count - 1;
            return (current == traversableList.First ? current = traversableList.Last : current = current.Previous).Value;
        }

        public T MoveToIndex(int i)
        {
            if (traversableList.Count == 0 || i >= traversableList.Count || i < 0)
                return default(T);
            while (index != i)
            {
                if (index < i)
                {
                    index++;
                    current = current.Next;
                }
                else
                {
                    index--;
                    current = current.Previous;
                }
            }
            return current.Value;
        }

        public void Reset()
        {
            if (traversableList.Count == 0)
            {
                index = -1;
            }
            else
            {
                index = 0;
                current = traversableList.First;
            }
        }

        public T RemoveCurrent()
        {
            if (traversableList.Count == 0)
                return default(T);
            LinkedListNode<T> old = current;
            if (traversableList.Count == 1)
            {
                index = -1;
                current = null;
                return default(T);
            }
            else if (index == traversableList.Count - 1)
            {
                index--;
                current = current.Previous;
            }
            else
            {
                current = current.Next;
            }
            traversableList.Remove(old);
            return current.Value;
        }

        public T RemoveAt(int i)
        {
            if (i >= traversableList.Count || i < 0)
                return GetCurrent();
            else if (i == index)
                return RemoveCurrent();
            else if (i < index)
                index--;
            traversableList.Remove(traversableList.ElementAt(i));
            return GetCurrent();
        }

        public void UpdateCurrent(T update)
        {
            if (traversableList.Count == 0)
                return;
            current.Value = update;
        }

        public void InsertFirst(T insert)
        {
            index++;
            traversableList.AddFirst(insert);
        }

        public void InsertLast (T insert)
        {
            traversableList.AddLast(insert);
        }

        public void InsertBeforeCurrent(T insert)
        {
            if (traversableList.Count == 0)
            {
                index = 0;
                traversableList.AddFirst(insert);
            }
            index++;
            traversableList.AddBefore(current, insert);
        }

        public void InsertAfterCurrent(T insert)
        {
            if (traversableList.Count == 0)
            {
                index = 0;
                traversableList.AddFirst(insert);
            }
            traversableList.AddAfter(current, insert);
        }

        public void InsertAt(int i, T insert)
        {
            if (i > traversableList.Count || i < 0)
                throw new IndexOutOfRangeException();
            if (i == traversableList.Count)
            {
                traversableList.AddLast(insert);
            }
            else
            {
                int insertIndex = index;
                LinkedListNode<T> insertNode = current;
                while (insertIndex != i)
                {
                    if (insertIndex < i)
                    {
                        insertIndex++;
                        insertNode = insertNode.Next;
                    }
                    else
                    {
                        insertIndex--;
                        insertNode = insertNode.Previous;
                    }
                }
                if (i <= index)
                    index++;
                traversableList.AddBefore(insertNode, insert);
            }
        }
    }
}

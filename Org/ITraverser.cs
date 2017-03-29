using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org
{
    public interface ITraverser<T>
    {
        T GetCurrent();
        T MoveNext();
        T MovePrevious();
        void Reset();
    }
}
